using BulletinBoard.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Core.Interfaces
{
    public interface IAdRepository
    {
        // Read
        public Task<IEnumerable<Ad>> GetAllAsync();
        public Task<Ad> GetByIdAsync(int id);
        public Task<IEnumerable<Ad>> GetByUserAsync(string userId);

        // Write
        public Task<int> CreateAsync(Ad ad);
        public Task UpdateAsync(Ad ad);
        public Task DeleteAsync(int id, string userId);
    }
}
