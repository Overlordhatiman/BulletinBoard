using BulletinBoard.Mvc.Models;
using Microsoft.AspNetCore.Authentication;
using System.Net.Http.Headers;

namespace BulletinBoard.Mvc.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApiService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        private async Task AddAuthorizationHeaderAsync()
        {
            var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");

            if (!string.IsNullOrEmpty(accessToken))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }
        }

        public async Task<List<Ad>> GetAdsAsync()
        {
            await AddAuthorizationHeaderAsync();
            var allAds = await _httpClient.GetFromJsonAsync<List<Ad>>("api/ads");
            return allAds?.Where(ad => ad.Status).ToList() ?? new List<Ad>();
        }

        public async Task<Ad> GetAdAsync(int id)
        {
            await AddAuthorizationHeaderAsync();
            return await _httpClient.GetFromJsonAsync<Ad>($"api/ads/{id}");
        }

        public async Task CreateAdAsync(AdCreateDto dto)
        {
            await AddAuthorizationHeaderAsync();
            await _httpClient.PostAsJsonAsync("api/ads", dto);
        }

        public async Task UpdateAdAsync(int id, AdUpdateDto dto)
        {
            await AddAuthorizationHeaderAsync();
            await _httpClient.PutAsJsonAsync($"api/ads/{id}", dto);
        }

        public async Task DeleteAdAsync(int id)
        {
            await AddAuthorizationHeaderAsync();
            await _httpClient.DeleteAsync($"api/ads/{id}");
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            await AddAuthorizationHeaderAsync();
            return await _httpClient.GetFromJsonAsync<List<Category>>("api/categories");
        }

        public async Task<List<Subcategory>> GetSubcategoriesAsync(int categoryId)
        {
            await AddAuthorizationHeaderAsync();
            return await _httpClient.GetFromJsonAsync<List<Subcategory>>($"api/categories/{categoryId}/subcategories");
        }
    }
}