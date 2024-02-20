using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HealthAdviceGroup.Data;
using HealthAdviceGroup.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HealthAdviceGroup.Controllers
{
    public class HealthController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HealthController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Health/Index
        public async Task<IActionResult> Index()
        {
            // Retrieve today's health data for the logged-in user
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var date = DateTime.Now.ToString("dd/MM/yyyy");
            var dailyEntry = await _context.Health
                .Where(u => u.UserId == userId)
                .Where(u => u.Date == date)
                .ToListAsync();

            // Return the data to the view
            return View(dailyEntry);
        }

        // GET: Health/MyDiary
        public async Task<IActionResult> MyDiary()
        {
            // Retrieve all health entries for the logged-in user
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var entries = await _context.Health
                .Where(u => u.UserId == userId)
                .ToListAsync();

            // Return the data to the view
            return View(entries);
        }

        // GET: Health/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Retrieve health details for the specified ID
            var health = await _context.Health
                .FirstOrDefaultAsync(m => m.Id == id);

            // If no health object found, return NotFound
            if (health == null)
            {
                return NotFound();
            }

            // Return the health object to the view
            return View(health);
        }

        // GET: Health/Create
        public IActionResult Create()
        {
            // Passes the User's ID and Date to the view for creating a new entry
            ViewBag.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.Date = DateTime.Now.ToString("dd/MM/yyyy");
            return View();
        }

        // POST: Health/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Health health)
        {
            // Validate and save a new health entry
            if (ModelState.IsValid)
            {
                _context.Add(health);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // If validation fails, return to the Create view with the userID and date values for making a new entry
            ViewBag.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.Date = DateTime.Now.ToString("dd/MM/yyyy");
            return View(health);
        }

        // GET: Health/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Retrieve health details for editing
            var health = await _context.Health.FindAsync(id);

            // If no health object found, return NotFound
            if (health == null)
            {
                return NotFound();
            }

            // Return the health object to the view for editing
            return View(health);
        }

        // POST: Health/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,Steps,Calories,Water")] Health health)
        {
            // Validate and save edited health entry
            if (id != health.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(health);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HealthExists(health.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            // If validation fails, return to the Edit view with current values
            return View(health);
        }

        // GET: Health/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Retrieve health details for deletion
            var health = await _context.Health
                .FirstOrDefaultAsync(m => m.Id == id);

            // If no health object found, return NotFound
            if (health == null)
            {
                return NotFound();
            }

            // Return the health object to the view for confirmation of deletion
            return View(health);
        }

        // POST: Health/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Delete the specified health entry
            var health = await _context.Health.FindAsync(id);
            if (health != null)
            {
                _context.Health.Remove(health);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Helper method to check if a health entry exists
        private bool HealthExists(int id)
        {
            return _context.Health.Any(e => e.Id == id);
        }
    }
}
