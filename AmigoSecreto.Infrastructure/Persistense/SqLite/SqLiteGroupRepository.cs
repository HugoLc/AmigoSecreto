using AmigoSecreto.Application.Common.Interfaces.Persistense;
using AmigoSecreto.Domain.Entity;
using AmigoSecreto.Domain.ValueObjects;
using AmigoSecreto.Infrastructure.Persistense.Common;
using Dapper;
using Microsoft.Data.Sqlite;

namespace AmigoSecreto.Infrastructure.Persistense.SqLite;
public class SqLiteGroupRepository : IGroupRepository
{
    //TODO: mudar para variavel de ambiente
    private readonly string _connectionString = "Data Source=../AmigoSecreto.Infrastructure/AmigoSecreto.db";
    private readonly IUserRepository _userRepository;

    public SqLiteGroupRepository(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task AddGroup(Group group)
    {
        await using var connection = new SqliteConnection(_connectionString);
        await using var transaction = await connection.BeginTransactionAsync();
        try
        {
            var sql = @"INSERT INTO 
                        [group] (id, draw_date, gifts_date, local, are_friends_drawn, admin_id)
                    VALUES
                        (@Id, @DrawDate, @GiftsDate, @Local, @AreFriendsDrawn, @AdminId)
                    ";
            await connection.ExecuteAsync(sql, new
            {
                Id = group.Id.ToString(),
                group.DrawDate,
                group.GiftsDate,
                group.Local,
                group.AreFriendsDrawn,
                AdminId = group.AdminId.ToString()
            });
            await _userRepository.AddGroup(group.AdminId, group.Id);
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
        }
    }

    public async Task<List<Player>> AddPlayers(Guid groupId, List<Player> players)
    {
        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();
        var sqlSelect = @"SELECT 
                    [id] as ID
                    FROM [group]
                    WHERE [id] = @Id";
        var groupResponse = await connection.QueryAsync(
            sqlSelect,
            new { Id = groupId }) ?? throw new Exception("grupo não encontrado");
        await connection.CloseAsync();
        foreach (var player in players)
        {
            await _userRepository.AddUser(player.ToUser());
        }
        return players;

    }

    public Group DrawDriends(Guid groupId)
    {
        throw new NotImplementedException();
    }

    public async Task<Group?> GetGroup(Guid id)
    {
        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();
        var sql = @"SELECT 
                    g.id as GroupId, 
                    g.draw_date as DrawDate,
                    g.gifts_date as GiftsDate,
                    g.local as Local, 
                    g.are_friends_drawn as AreFriendsDrawn,
                    g.admin_id as AdminId,
                    u.id as Id,
                    u.name as Name,
                    u.phone as Phone,
                    gift.id as GiftId, 
                    gift.description as Description, 
                    gift.link as Link
                FROM 
                    [group] g
                LEFT JOIN 
                    [user]u ON g.id = u.group_id
                LEFT JOIN 
                    [gift] gift ON u.id = gift.user_id
                WHERE g.id = @GroupId";
        Group? group = null;
        await connection.QueryAsync<GroupSqliteResponse, PlayerSqliteResponse, GiftsSqliteResponse, Group>(
            sql,
            (groupResp, playerResp, giftResp) =>
            {
                if (group == null)
                {

                    group = new Group()
                    {
                        Id = Guid.Parse(groupResp.GroupId),
                        AdminId = Guid.Parse(groupResp.AdminId),
                        //TODO verificar a string vazia
                        DrawDate = DateTime.Parse(groupResp.DrawDate),
                        GiftsDate = DateTime.Parse(groupResp.GiftsDate),
                        Local = groupResp.Local,
                        AreFriendsDrawn = groupResp.AreFriendsDrawn,
                        Players = []
                    };
                }

                var currentPlayer = group.Players.FirstOrDefault(p => p.Id.ToString() == playerResp.Id);
                if (currentPlayer == null)
                {

                    currentPlayer = new Player()
                    {
                        Id = Guid.Parse(playerResp.Id),
                        Name = playerResp.Name,
                        Phone = playerResp.Phone,
                        GroupId = Guid.Parse(groupResp.GroupId),
                        Gifts = []
                    };
                    currentPlayer.Gifts = [];
                    group.Players.Add(currentPlayer);
                }

                if (giftResp != null && giftResp.GiftId != string.Empty)
                {

                    currentPlayer.Gifts.Add(new Gift()
                    {
                        Id = Guid.Parse(giftResp.GiftId),
                        Description = giftResp.Description,
                        Link = giftResp.Link,
                        UserId = currentPlayer.Id,
                    });
                }

                return group;
            },
            new { GroupId = id.ToString() },

            splitOn: "Id,GiftId"
        );
        return group;
    }

    public List<Group> GetGroups()
    {
        throw new NotImplementedException();
    }
}
