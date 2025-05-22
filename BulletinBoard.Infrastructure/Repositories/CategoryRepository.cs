using BulletinBoard.Core.Entities;
using BulletinBoard.Core.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly string _connectionString;

        public CategoryRepository(string conString)
        {
            _connectionString = conString;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<Category>(
                "SELECT * FROM Categories",
                commandType: CommandType.Text);
        }

        public async Task<IEnumerable<Subcategory>> GetSubcategoriesAsync(int categoryId)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<Subcategory>(
                "SELECT * FROM Subcategories WHERE CategoryId = @CategoryId",
                new { CategoryId = categoryId },
                commandType: CommandType.Text);
        }
    }
}