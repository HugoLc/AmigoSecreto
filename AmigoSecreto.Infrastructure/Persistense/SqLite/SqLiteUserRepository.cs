using AmigoSecreto.Application.Common.Interfaces.Persistense;
using AmigoSecreto.Domain.Entity;
using AmigoSecreto.Domain.ValueObjects;
using AmigoSecreto.Infrastructure.Persistense.Common;
using Dapper;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace AmigoSecreto.Infrastructure.Persistense.SqLite;
public class SqLiteUserRepository : IUserRepository
{
    private readonly string _connectionString = "Data Source=../AmigoSecreto.Infrastructure/AmigoSecreto.db";
    public async Task AddGroup(Guid userId, Guid groupId)
    {
        await using var connection = new SqliteConnection(_connectionString);
        var sql = @"UPDATE [user] 
                    SET [group_id] = @GroupId
                    WHERE [id] = @UserId";
        var rowsAffected = await connection.ExecuteAsync(sql, new { UserId = userId.ToString(), GroupId = groupId.ToString() });
        Console.WriteLine($"{rowsAffected} linhas afetadas");

    }

    public async Task AddUser(User user)
    {
        await using (var connection = new SqliteConnection(_connectionString))
        {
            var rowsAffected = await connection.ExecuteAsync(
                @"INSERT INTO
                    [user] (id, name, phone, password, group_id)
                  VALUES
                    (@Id, @Name, @Phone, @Password, @GroupId)  
                ",
                new
                {
                    Id = user.Id.ToString(),
                    user.Name,
                    user.Phone,
                    user.Password,
                    GroupId = user.GroupId.ToString()
                }
            );
            if (
                rowsAffected == 0 ||
                user.Gifts == null ||
                user.Gifts.Count == 0) return;
            foreach (var gift in user.Gifts)
            {

                rowsAffected += await connection.ExecuteAsync(
                    @"INSERT INTO
                        [gift] (id, user_id, description, link)
                    VALUES 
                        (@Id, @UserId, @Description, @Link)
                ",
                new
                {
                    gift.Id,
                    UserId = gift.UserId.ToString(),
                    gift.Description,
                    gift.Link
                }
                );
            }
            Console.WriteLine($"{rowsAffected} linhas afetadas");
        }
    }

    public async Task<Player?> GetPlayer(Guid userId)
    {
        await using var connection = new SqliteConnection(_connectionString);
        var sqlPlayer = @"SELECT 
                                id as Id, 
                                name as Name, 
                                phone as Phone, 
                                group_id as GroupId
                              FROM [user] 
                              WHERE id = @UserId";
        var players = await connection.QueryAsync<PlayerSqliteResponse>(
                sqlPlayer, new { UserId = userId.ToString() }
            );
        var sqlGifts = @"SELECT 
                            id as Id, 
                            user_id as UserId,
                            description as Description, 
                            link as Link
                        FROM [gift] 
                        WHERE user_id = @UserId";
        var giftsSqlResponse = await connection.QueryAsync<GiftsSqliteResponse>(sqlGifts, new { UserId = userId.ToString() });

        var playerSqlResponse = players.FirstOrDefault();
        if (playerSqlResponse is null)
        {
            return null;
        }
        var gifts = giftsSqlResponse.Select(g => new Gift()
        {
            Id = Guid.Parse(g.GiftId),
            UserId = Guid.Parse(g.UserId),
            Description = g.Description,
            Link = g.Link
        }).ToList();
        var player = new Player
        {
            Id = Guid.Parse(playerSqlResponse.Id),
            Name = playerSqlResponse.Name,
            Phone = playerSqlResponse.Phone,
            GroupId = !string.IsNullOrWhiteSpace(playerSqlResponse.GroupId) ? Guid.Parse(playerSqlResponse.GroupId) : null,
            Gifts = gifts
        };
        return player;
    }

    public List<Player> GetPlayers()
    {
        throw new NotImplementedException();
    }

    public async Task<List<Player>> GetPlayersByGroup(Guid groupId)
    {
        await using var connection = new SqliteConnection(_connectionString);
        var sqlPlayer = @"SELECT 
                            u.id as Id, 
                            u.name as Name, 
                            u.phone as Phone, 
                            u.group_id as GroupId,
                            g.id as GiftId,
                            g.description as Description,
                            g.link as Link
                        FROM [user] u
                        LEFT JOIN gift g ON g.user_id = u.id
                        WHERE group_id = @GroupId";

        var playerDictionary = new Dictionary<Guid, Player>();

        var playersResponse = await connection.QueryAsync<PlayerSqliteResponse, GiftsSqliteResponse, Player>(
            sqlPlayer,
            (playerResp, giftResp) =>
            {
                if (!playerDictionary.TryGetValue(Guid.Parse(playerResp.Id), out var currentPlayer))
                {
                    currentPlayer = new Player()
                    {
                        Id = Guid.Parse(playerResp.Id),
                        Name = playerResp.Name,
                        Phone = playerResp.Phone
                    };
                    playerDictionary.Add(currentPlayer.Id, currentPlayer);
                }

                if (playerResp != null && giftResp != null)
                {
                    var gift = new Gift()
                    {
                        Id = Guid.Parse(giftResp.GiftId),
                        Description = giftResp.Description,
                        Link = giftResp.Link,
                        UserId = Guid.Parse(playerResp.Id)
                    };
                    currentPlayer.Gifts.Add(gift);
                }

                return currentPlayer;
            },
            new { GroupId = groupId.ToString() },
            splitOn: "GiftId"
        );
        return playersResponse.Distinct().ToList();
    }
}
