using BulletinBoard.Core.Entities;
using BulletinBoard.Core.Interfaces;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BulletinBoard.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AdsController : ControllerBase
    {
        private readonly IAdRepository _adRepository;

        public AdsController(IAdRepository adRepository)
        {
            _adRepository = adRepository;
        }

        // GET: api/ads
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ad>>> GetAds()
        {
            var ads = await _adRepository.GetAllAsync();
            return Ok(ads);
        }

        // GET: api/ads/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ad>> GetAd(int id)
        {
            var ad = await _adRepository.GetByIdAsync(id);

            if (ad == null)
            {
                return NotFound();
            }

            return Ok(ad);
        }

        // POST: api/ads
        [HttpPost]
        public async Task<ActionResult<Ad>> PostAd(Ad ad)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            ad.UserId = userId;
            ad.CreatedDate = DateTime.UtcNow;
            ad.Status = true;

            var createdId = await _adRepository.CreateAsync(ad);
            ad.Id = createdId;

            return CreatedAtAction(nameof(GetAd), new { id = ad.Id }, ad);
        }

        // PUT: api/ads/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAd(int id, Ad ad)
        {
            if (id != ad.Id)
            {
                return BadRequest();
            }

            try
            {
                await _adRepository.UpdateAsync(ad);
            }
            catch (Exception)
            {
                if (await _adRepository.GetByIdAsync(id) == null)
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/ads/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAd(int id)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            await _adRepository.DeleteAsync(id, userId);
            return NoContent();
        }
    }
}