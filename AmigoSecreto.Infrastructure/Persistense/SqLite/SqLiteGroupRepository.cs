using System.Linq.Expressions;
using AmigoSecreto.Application.Common.Interfaces.Persistense;
using AmigoSecreto.Domain.Entity;
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
        var sqlSelect = @"SELECT 
                    [id] as ID
                    FROM [group]
                    WHERE [id] = @Id";
        var groupResponse = await connection.QueryAsync<GroupSqliteResponse>(
            sqlSelect,
            new { Id = id }) ?? throw new Exception("grupo não encontrado");
        //pegar playersresponse por grupo
        //criar lista de players
        //criar metodo no repositorio
        //criar group com base no groupresponse
        //adicionar players no group
        //retornar group


    }

    public List<Group> GetGroups()
    {
        throw new NotImplementedException();
    }
}
