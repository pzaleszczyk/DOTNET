using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using pzaleszczyk.Models;
using pzaleszczyk.Data;
using Microsoft.AspNetCore.Authorization;

namespace pzaleszczyk.Controllers
{
    [Authorize]
    public class MangasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MangasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Mangas
        //public async Task<IActionResult> Index()
        //{
        //    var applicationDbContext = _context.Manga.Include(m => m.Author);
        //    return View(await applicationDbContext.ToListAsync());
        //}

        public async Task<IActionResult> Index(string mangaGenre, string searchString)
        {
            //System.Diagnostics.Debug.WriteLine(System.Reflection.Assembly.GetExecutingAssembly());
            //LINQ
            IQueryable<string> genreQuery = from m in _context.Manga
                                            orderby m.Genre
                                            select m.Genre;

            var mangas = from m in _context.Manga
                         select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                mangas = mangas.Where(s => s.Title.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(mangaGenre))
            {
                mangas = mangas.Where(x => x.Genre == mangaGenre);
            }

            var movieGenreVM = new GenreViewModel
            {
                Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),

                Mangas = await mangas.Include(m => m.Author).ToListAsync()
            };

            return View(movieGenreVM);
        }

        // GET: Mangas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manga = await _context.Manga
                .Include(m => m.Author)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (manga == null)
            {
                return NotFound();
            }

            return View(manga);
        }

        // GET: Mangas/Create
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_context.Set<Author>(), "Id", "Name");
            return View();
        }

        // POST: Mangas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,AuthorId,Genre,Rating,Price")] Manga manga)
        {
            if (ModelState.IsValid)
            {
                _context.Add(manga);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.Set<Author>(), "Id", "Name", manga.AuthorId);
            return View(manga);
        }

        // GET: Mangas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manga = await _context.Manga.FindAsync(id);
            if (manga == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_context.Set<Author>(), "Id", "Name", manga.AuthorId);
            return View(manga);
        }

        // POST: Mangas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,AuthorId,Genre,Rating,Price")] Manga manga)
        {
            if (id != manga.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(manga);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MangaExists(manga.Id))
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
            ViewData["AuthorId"] = new SelectList(_context.Set<Author>(), "Id", "Name", manga.AuthorId);
            return View(manga);
        }

        // GET: Mangas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manga = await _context.Manga
                .Include(m => m.Author)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (manga == null)
            {
                return NotFound();
            }

            return View(manga);
        }

        // POST: Mangas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var manga = await _context.Manga.FindAsync(id);
            _context.Manga.Remove(manga);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MangaExists(int id)
        {
            return _context.Manga.Any(e => e.Id == id);
        }
    }
}
