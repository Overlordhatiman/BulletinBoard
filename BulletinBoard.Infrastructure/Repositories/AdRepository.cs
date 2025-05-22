using BulletinBoard.Core.Entities;
using BulletinBoard.Core.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BulletinBoard.Infrastructure.Repositories
{
    public class AdRepository : IAdRepository
    {
        private readonly string _connectionString;

        public AdRepository(string conString)
        {
            _connectionString = conString;
        }

        // Read Operations
        public async Task<IEnumerable<Ad>> GetAllAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<Ad, Category, Subcategory, Ad>(
                "spAd_GetAll",
                (ad, category, subcategory) =>
                {
                    ad.Category = category;
                    ad.Subcategory = subcategory;
                    return ad;
                },
                splitOn: "CategoryId,SubcategoryId",
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<Ad> GetByIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var result = await connection.QueryAsync<Ad, Category, Subcategory, Ad>(
                "spAd_GetById",
                (ad, category, subcategory) =>
                {
                    ad.Category = category;
                    ad.Subcategory = subcategory;
                    return ad;
                },
                new { Id = id },
                splitOn: "CategoryId,SubcategoryId",
                commandType: CommandType.StoredProcedure
            );
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<Ad>> GetByUserAsync(string userId)
        {
            using var connection = new SqlConnection(_connectionString);
            var result = await connection.QueryAsync<Ad, Category, Subcategory, Ad>(
                "spAd_GetByUser",
                (ad, category, subcategory) =>
                {
                    ad.Category = category;
                    ad.Subcategory = subcategory;
                    return ad;
                },
                new { UserId = userId },
                splitOn: "CategoryId,SubcategoryId",
                commandType: CommandType.StoredProcedure
            );
            return result;
        }

        // Write Operations
        public async Task<int> CreateAsync(Ad ad)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.ExecuteScalarAsync<int>(
                "spAd_Create",
                new
                {
                    ad.Title,
                    ad.Description,
                    ad.UserId,
                    ad.CategoryId,
                    ad.SubcategoryId
                },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task UpdateAsync(Ad ad)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync(
                "spAd_Update",
                new
                {
                    ad.Id,
                    ad.Title,
                    ad.Description,
                    ad.CategoryId,
                    ad.SubcategoryId
                },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task DeleteAsync(int id, string userId)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync(
                "spAd_Delete",
                new { Id = id },
                commandType: CommandType.StoredProcedure
            );
        }

        // Categories
        public async Task<IEnumerable<Category>> GetCategoriesWithSubcategoriesAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            var categories = await connection.QueryAsync<Category>(
                "SELECT * FROM Categories",
                commandType: CommandType.Text
            );

            var subcategories = await connection.QueryAsync<Subcategory>(
                "SELECT * FROM Subcategories",
                commandType: CommandType.Text
            );

            // Map subcategories to categories
            foreach (var category in categories)
            {
                category.Subcategories = subcategories
                    .Where(s => s.CategoryId == category.CategoryId)
                    .ToList();
            }

            return categories;
        }
    }
}