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
    public class AdviceController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ApplicationDbContext _context;

        public AdviceController(IWebHostEnvironment webHostEnvironment, ApplicationDbContext context)
        {
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }

        // GET: Advice/Index
        public async Task<IActionResult> Index()
        {
            // Display all advice entries
            return View(await _context.Advice.ToListAsync());
        }

        // GET: Advice/MyAdvice
        public async Task<IActionResult> MyAdvice()
        {
            // Retrieve advice saved by the logged-in user
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var adviceIds = await _context.Save
                .Where(s => s.UserId == userId)
                .Select(s => s.AdviceId)
                .ToListAsync();

            var advice = await _context.Advice
                .Where(a => adviceIds.Contains(a.Id))
                .ToListAsync();

            return View(advice);
        }

        // GET: Advice/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Retrieve details of a specific advice entry
            var advice = await _context.Advice
                .FirstOrDefaultAsync(m => m.Id == id);
            if (advice == null)
            {
                return NotFound();
            }

            return View(advice);
        }

        // GET: Advice/Create
        public IActionResult Create()
        {
            // Display the form for creating new advice
            return View();
        }

        // POST: Advice/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Advice advice)
        {
            if (ModelState.IsValid)
            {
                // If the form is valid, handle image upload and save the advice
                if (advice.ImageFile != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "uploaded");
                    advice.ImagePath = Guid.NewGuid().ToString() + "_" + advice.ImageFile.FileName;
                    string filePath = Path.Combine(uploadsFolder, advice.ImagePath);
                    advice.ImageFile.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                _context.Add(advice);
                await _context.SaveChangesAsync();
                // Redirect to the dashboard after successful advice creation
                return RedirectToAction("Index", "Home");
            }
            return View(advice);
        }

        // GET: Advice/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            Console.WriteLine("----------------------1");
            Console.WriteLine("----------------------1");
            Console.WriteLine("----------------------1");
            Console.WriteLine("----------------------1");
            Console.WriteLine("----------------------1");
            Console.WriteLine("----------------------1");
            if (id == null)
            {
                return NotFound();
            }

            // Display the form for editing a specific advice entry
            Advice advice = await _context.Advice.FindAsync(id);
            if (advice == null)
            {
                return NotFound();
            }
            return View(advice);
        }

        // POST: Advice/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Advice advice)
        {
            if (id != advice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Update and save the edited advice entry
                    _context.Update(advice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdviceExists(advice.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                // Redirect to the advice index after successful edit
                return RedirectToAction(nameof(Index));
            }
            return View(advice);
        }

        // GET: Advice/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Display the confirmation page for deleting a specific advice entry
            var advice = await _context.Advice
                .FirstOrDefaultAsync(m => m.Id == id);
            if (advice == null)
            {
                return NotFound();
            }

            return View(advice);
        }

        // POST: Advice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Delete the specified advice entry
            var advice = await _context.Advice.FindAsync(id);
            if (advice != null)
            {
                _context.Advice.Remove(advice);
            }

            await _context.SaveChangesAsync();
            // Redirect to the advice index after successful deletion
            return RedirectToAction(nameof(Index));
        }

        // Helper method to check if an advice entry exists
        private bool AdviceExists(int id)
        {
            return _context.Advice.Any(e => e.Id == id);
        }

        // POST: Advice/SaveAdvice
        public async Task<IActionResult> SaveAdvice(IFormCollection collection)
        {
            // Get user's ID so it is saved specifically to their account
            // Get the value from the hidden input named "id"
            int adviceId = int.Parse(collection["id"].ToString());
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var existingSave = await _context.Save
                .FirstOrDefaultAsync(s => s.UserId == userId && s.AdviceId == adviceId);

            if (existingSave != null)
            {
                // If already saved, redirect to MyAdvice
                return RedirectToAction("Index", "Advice");
            }
            else
            {
                // Create a new advice entry and save it to the user's saved advice list
                var save = new Save
                {
                    AdviceId = adviceId,
                    UserId = userId,
                    Date = DateTime.Now.ToString("dd/MM/yyyy"),
                };
                // Add the new entry to "Save" table
                _context.Save.Add(save);
                await _context.SaveChangesAsync();
                Console.WriteLine("Successful save");
                // Redirect to MyAdvice after successful save
                return RedirectToAction("Index", "Advice");
            }
        }

        // POST: Advice/DeleteSave
        [HttpPost, ActionName("DeleteSave")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSave(IFormCollection collection)
        {
            // Remove advice from the user's saved advice list
            var id = int.Parse(collection["id"].ToString());
            var save = await _context.Save.Where(s => s.AdviceId == id)
                .Where(s => s.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier))
                .FirstOrDefaultAsync();

            if (save != null)
            {
                _context.Save.Remove(save);
            }

            await _context.SaveChangesAsync();
            // Redirect to MyAdvice after successful removal
            return RedirectToAction(nameof(MyAdvice));
        }
    }
}
