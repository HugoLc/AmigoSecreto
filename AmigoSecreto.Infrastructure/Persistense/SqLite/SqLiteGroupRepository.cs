using AmigoSecreto.Application.Common.Interfaces.Persistense;
using AmigoSecreto.Domain.Entity;
using Dapper;
using Microsoft.Data.Sqlite;

namespace AmigoSecreto.Infrastructure.Persistense.SqLite;
public class SqLiteGroupRepository : IGroupRepository
{
    //TODO: mudar para variavel de ambiente
    private readonly string _connectionString = "Data Source=../AmigoSecreto.Infrastructure/AmigoSecreto.db";
    public async Task AddGroup(Group group)
    {
        await using var connection = new SqliteConnection(_connectionString);
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
            group.AdminId
        });
    }

    public List<Player> AddPlayers(Guid groupId, List<Player> players)
    {
        throw new NotImplementedException();
    }

    public Group DrawDriends(Guid groupId)
    {
        throw new NotImplementedException();
    }

    public Group? GetGroup(Guid id)
    {
        throw new NotImplementedException();
    }

    public List<Group> GetGroups()
    {
        throw new NotImplementedException();
    }
}
