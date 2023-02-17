using Dapper;
using repository_dapper.Interfaces;
using repository_dapper.Models;
using System.Data.SqlClient;

namespace repository_dapper.Repository
{

    public class ProductRepositoryDapper : IRepository<Product>
    {
        private readonly string _connectionString;

        public ProductRepositoryDapper(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var sql = "SELECT * FROM Products WHERE Id = @Id";
            var product = await connection.QuerySingleOrDefaultAsync<Product>(sql, new { Id = id });

            return product;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var sql = "SELECT * FROM Products";
            var products = await connection.QueryAsync<Product>(sql);

            return products;
        }

        public async Task<Product> AddAsync(Product product)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var sql = "INSERT INTO Products (Name, Description, Price) VALUES (@Name, @Description, @Price); SELECT CAST(SCOPE_IDENTITY() as int)";
            var id = await connection.ExecuteScalarAsync<int>(sql, product);

            product.Id = id;

            return product;
        }

        public async Task<bool> UpdateAsync(Product product)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var sql = "UPDATE Products SET Name = @Name, Description = @Description, Price = @Price WHERE Id = @Id";
            var affectedRows = await connection.ExecuteAsync(sql, product);

            return affectedRows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var sql = "DELETE FROM Products WHERE Id = @Id";
            var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });

            return affectedRows > 0;
        }
    }
}
