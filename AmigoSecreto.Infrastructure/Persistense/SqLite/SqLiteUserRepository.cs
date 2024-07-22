using AmigoSecreto.Application.Common.Interfaces.Persistense;
using AmigoSecreto.Domain.Entity;
using Dapper;
using Microsoft.Data.Sqlite;

namespace AmigoSecreto.Infrastructure.Persistense.SqLite;
public class SqLiteUserRepository : IUserRepository
{
    private readonly string _connectionString = "Data Source=../AmigoSecreto.Infrastructure/AmigoSecreto.db";
    public void AddGroup(Guid userId, Guid groupId)
    {
        throw new NotImplementedException();
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

    public async Task<User?> GetUser(Guid userId)
    {
        await using (var connection = new SqliteConnection(_connectionString))
        {
            var sql = $"SELECT * FROM [user] WHERE id = @UserId";
            var user = await connection.QueryAsync<User>(
                    sql, new { UserId = userId.ToString() }
                );
                //TODO: mudar tudo para player. get player...
                //TODO
                // criar tipo de resposta da query
                // pegar dados dos gifts
                // mapear para user
                //
            return user.FirstOrDefault();
        }
    }

    public List<User> GetUsers()
    {
        throw new NotImplementedException();
    }
}
