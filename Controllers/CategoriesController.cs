using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventManagementSystem.Context;
using EventManagementSystem.Models;

namespace EventManagementSystem.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly CategoriesDbContext _context;

        public CategoriesController(CategoriesDbContext context)
        {
            _context = context;
        }

        // GET: Categories
        public async Task<IActionResult> Index(string searchString = null)
        {
            var categories = from c in _context.EventCategories
                             select c;

            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                categories = categories.Where(c =>
                    c.CategoryName.ToLower().Contains(searchString) ||
                    (c.Description != null && c.Description.ToLower().Contains(searchString)) ||
                    c.PricePerDay.ToString().Contains(searchString) ||
                    c.PricePerHour.ToString().Contains(searchString) ||
                    c.IsActive.ToString().ToLower().Contains(searchString) ||
                    c.MaxCapacity.ToString().Contains(searchString)
                );
            }

            return View(await categories.ToListAsync());
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var category = await _context.EventCategories
                .FirstOrDefaultAsync(m => m.CategoryId == id);

            if (category == null) return NotFound();

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,CategoryName,Description,PricePerHour,PricePerDay,IsActive,MaxCapacity")] EventCategory category)
        {
            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var category = await _context.EventCategories.FindAsync(id);
            if (category == null) return NotFound();

            return View(category);
        }

        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,CategoryName,Description,PricePerHour,PricePerDay,IsActive,MaxCapacity")] EventCategory category)
        {
            if (id != category.CategoryId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventCategoryExists(category.CategoryId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var category = await _context.EventCategories
                .FirstOrDefaultAsync(m => m.CategoryId == id);

            if (category == null) return NotFound();

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.EventCategories.FindAsync(id);
            if (category != null)
            {
                _context.EventCategories.Remove(category);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool EventCategoryExists(int id)
        {
            return _context.EventCategories.Any(e => e.CategoryId == id);
        }
    }
}
