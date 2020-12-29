using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Libre.Data;
using Libre.Models;

namespace Libre.Controllers
{
    public class GendresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GendresController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Gendre.ToListAsync());
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gendre = await _context.Gendre
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gendre == null)
            {
                return NotFound();
            }

            return View(gendre);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Gendre gendre)
        {
            if (ModelState.IsValid)
            {
                gendre.Id = Guid.NewGuid();
                _context.Add(gendre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gendre);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gendre = await _context.Gendre.FindAsync(id);
            if (gendre == null)
            {
                return NotFound();
            }
            return View(gendre);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name")] Gendre gendre)
        {
            if (id != gendre.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gendre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GendreExists(gendre.Id))
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
            return View(gendre);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gendre = await _context.Gendre
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gendre == null)
            {
                return NotFound();
            }

            return View(gendre);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var gendre = await _context.Gendre.FindAsync(id);
            _context.Gendre.Remove(gendre);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GendreExists(Guid id)
        {
            return _context.Gendre.Any(e => e.Id == id);
        }
    }
}
