using Dapper;
using Microsoft.Data.Sqlite;
using Models;
using Repositories.Models;

namespace Repositories;

public class UsersRepository
{
    private readonly DatabaseConfig databaseConfig;
    public UsersRepository(DatabaseConfig databaseConfig)
    {
        this.databaseConfig = databaseConfig;
    }
    public async Task<User?> GetUser()
    {
        var sql = "SELECT Culture FROM users LIMIT 1";
       
        using (var connection = new SqliteConnection(databaseConfig.Name))
        {
           return await connection.QuerySingleAsync<User?>(sql);
        }
    }
}
