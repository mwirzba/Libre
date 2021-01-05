using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Libre.Data;
using Libre.Models;
using Libre.Utility;
using Microsoft.AspNetCore.Authorization;


namespace Libre.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = Strings.Admin)]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Book.Include(b => b.Genre);
            return View(await applicationDbContext.ToListAsync());
        }

        [Authorize(Roles = Strings.Admin + "," + Strings.Moderator + "," + Strings.User)]
        public async Task<IActionResult> BooksList(string bookGenre, string searchString)
        {
            IQueryable<string> genreQuery = (from m in _context.Genre
                                             orderby m.Name
                                            select m.Name);

            var books = from m in _context.Book.Include(b => b.Genre) select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(s => s.Title.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(bookGenre))
            {
                books = books.Where(x => x.Genre.Name == bookGenre);
            }

            var bookGenreVM = new BookGenreViewModel
            {
                Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Books = await books.ToListAsync()
            };

            return View(bookGenreVM);
        }

        [Authorize(Roles = Strings.Admin)]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [Authorize(Roles = Strings.Admin + "," + Strings.Moderator + "," + Strings.User)]
        public async Task<IActionResult> DetailsForUser(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [Authorize(Roles = Strings.Admin)]
        public IActionResult Create()
        {
            ViewData["GenreId"] = new SelectList(_context.Set<Genre>(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Strings.Admin)]
        public async Task<IActionResult> Create([Bind("Id,Title,Publisher,RealeaseDate,Language,Pages,CoverType,Info,GenreId")] Book book)
        {
            if (ModelState.IsValid)
            {
                book.Id = Guid.NewGuid();
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenreId"] = new SelectList(_context.Set<Genre>(), "Id", "Name", book.GenreId);
            return View(book);
        }

        // GET: Books/Edit/5
        [Authorize(Roles = Strings.Admin)]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["GenreId"] = new SelectList(_context.Set<Genre>(), "Id", "Name", book.GenreId);
            return View(book);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Strings.Admin)]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Title,Publisher,RealeaseDate,Language,Pages,CoverType,Info,GenreId")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
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
            ViewData["GenreId"] = new SelectList(_context.Set<Genre>(), "Id", "Name", book.GenreId);
            return View(book);
        }

        // GET: Books/Delete/5
        [Authorize(Roles = Strings.Admin)]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [Authorize(Roles = Strings.Admin)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var book = await _context.Book.FindAsync(id);
            _context.Book.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(Guid id)
        {
            return _context.Book.Any(e => e.Id == id);
        }
    }
}
