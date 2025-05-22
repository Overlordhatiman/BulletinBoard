using BulletinBoard.Mvc.Models;
using BulletinBoard.Mvc.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulletinBoard.Mvc.Controllers
{
    [Authorize]
    public class AdsController : Controller
    {
        private readonly ApiService _apiService;

        public AdsController(ApiService apiService)
        {
            _apiService = apiService;
        }

        // GET: Ads
        public async Task<IActionResult> Index()
        {
            var ads = await _apiService.GetAdsAsync();
            return View(ads);
        }

        // GET: Ads/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var ad = await _apiService.GetAdAsync(id);
            if (ad == null)
            {
                return NotFound();
            }
            return View(ad);
        }

        // GET: Ads/Create
        public async Task<IActionResult> Create()
        {
            await PopulateCategoriesViewBag();
            return View();
        }

        // POST: Ads/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdCreateDto dto)
        {
            if (ModelState.IsValid)
            {
                await _apiService.CreateAdAsync(dto);
                return RedirectToAction(nameof(Index));
            }
            await PopulateCategoriesViewBag();
            return View(dto);
        }

        // GET: Ads/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var ad = await _apiService.GetAdAsync(id);
            if (ad == null)
            {
                return NotFound();
            }

            await PopulateCategoriesViewBag();
            var editDto = new AdUpdateDto
            {
                Id = ad.Id,
                Title = ad.Title,
                Description = ad.Description,
                CategoryId = ad.CategoryId,
                SubcategoryId = ad.SubcategoryId
            };
            return View(editDto);
        }

        // POST: Ads/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AdUpdateDto dto)
        {
            if (id != dto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _apiService.UpdateAdAsync(id, dto);
                return RedirectToAction(nameof(Index));
            }
            await PopulateCategoriesViewBag();
            return View(dto);
        }

        // GET: Ads/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var ad = await _apiService.GetAdAsync(id);
            if (ad == null)
            {
                return NotFound();
            }
            return View(ad);
        }

        // POST: Ads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _apiService.DeleteAdAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // AJAX endpoint for subcategories
        public async Task<JsonResult> GetSubcategories(int categoryId)
        {
            var subcategories = await _apiService.GetSubcategoriesAsync(categoryId);
            return Json(subcategories);
        }

        private async Task PopulateCategoriesViewBag()
        {
            var categories = await _apiService.GetCategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
        }
    }
}