using EconomicManagementAPP.Models;
using EconomicManagementAPP.Repositories;
using Microsoft.Data.SqlClient;
using Dapper;

namespace EconomicManagementAPP.Services
{
    public class ServicesAccounts : IRepositorieAccounts
    {
        private readonly string connectionString;
        public ServicesAccounts(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task Create(Accounts accounts)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>
                ($@"INSERT INTO Accounts
                           (Name, AccountTypeId, Balance, Description)
                           VALUES(@Name, @AccountTypeId, @Balance, @Description);
                            SELECT SCOPE_IDENTITY();",
                            accounts);
            accounts.Id = id;
        }

        public async Task<bool> Exist(string Name, int Id)
        {
            using var connection = new SqlConnection(connectionString);
            var exist = await connection.QueryFirstOrDefaultAsync<int>
                ($@"SELECT 1
                        FROM Accounts WHERE Name = @Name AND Id=@Id;", 
                        new { Name, Id });
            return exist == 1;
        }

        public async Task<IEnumerable<Accounts>> GetAccounts()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Accounts>(
                @"SELECT 
                    Id, Name, AccountTypeId, Balance, Description
                    FROM Accounts;");
        }
        public async Task ModifyAccount(Accounts accounts)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"Update Accounts 
                    set Name=@Name, AccountTypeIdBalance=@AccountTypeIdBalance, Description=@AccountTypeIdBalance
                        WHERE Id = @Id;", accounts);
        }
        public async Task Delete(int id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync("DELETE Users WHERE Id = @Id", new { id });
        }
    }
}
