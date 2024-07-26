using AmigoSecreto.Application.Common.Interfaces.Persistense;
using AmigoSecreto.Domain.Entity;
using AmigoSecreto.Domain.ValueObjects;
using AmigoSecreto.Infrastructure.Persistense.Common;
using Dapper;
using Microsoft.Data.Sqlite;

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
                    user.GroupId
                }
            );
            if (rowsAffected == 0) return;
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
        await using (var connection = new SqliteConnection(_connectionString))
        {
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
                Id = Guid.Parse(g.Id),
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
    }

    public List<Player> GetPlayers()
    {
        throw new NotImplementedException();
    }
}
