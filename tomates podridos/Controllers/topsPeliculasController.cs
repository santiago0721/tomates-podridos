using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using tomates_podridos.Data;
using tomates_podridos.Models;

namespace tomates_podridos.Controllers
{
    public class topsPeliculasController : Controller
    {
        private readonly tomates_podridosContext _context;

        public topsPeliculasController(tomates_podridosContext context)
        {
            _context = context;
        }

        // GET: topsPeliculas
        public async Task<IActionResult> Index()
        {
            var tomates_podridosContext = _context.topsPelicula.Include(t => t.Pelicula);
            return View(await tomates_podridosContext.ToListAsync());
        }

        // GET: topsPeliculas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.topsPelicula == null)
            {
                return NotFound();
            }

            var topsPelicula = await _context.topsPelicula
                .Include(t => t.Pelicula)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (topsPelicula == null)
            {
                return NotFound();
            }

            return View(topsPelicula);
        }

        // GET: topsPeliculas/Create
        public IActionResult Create()
        {
            ViewData["PeliculaId"] = new SelectList(_context.Pelicula, "Id", "Id");
            return View();
        }

        // POST: topsPeliculas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PeliculaId")] topsPelicula topsPelicula)
        {
            if (ModelState.IsValid)
            {
                _context.Add(topsPelicula);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PeliculaId"] = new SelectList(_context.Pelicula, "Id", "Id", topsPelicula.PeliculaId);
            return View(topsPelicula);
        }

        // GET: topsPeliculas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.topsPelicula == null)
            {
                return NotFound();
            }

            var topsPelicula = await _context.topsPelicula.FindAsync(id);
            if (topsPelicula == null)
            {
                return NotFound();
            }
            ViewData["PeliculaId"] = new SelectList(_context.Pelicula, "Id", "Id", topsPelicula.PeliculaId);
            return View(topsPelicula);
        }

        // POST: topsPeliculas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PeliculaId")] topsPelicula topsPelicula)
        {
            if (id != topsPelicula.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(topsPelicula);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!topsPeliculaExists(topsPelicula.Id))
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
            ViewData["PeliculaId"] = new SelectList(_context.Pelicula, "Id", "Id", topsPelicula.PeliculaId);
            return View(topsPelicula);
        }

        // GET: topsPeliculas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.topsPelicula == null)
            {
                return NotFound();
            }

            var topsPelicula = await _context.topsPelicula
                .Include(t => t.Pelicula)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (topsPelicula == null)
            {
                return NotFound();
            }

            return View(topsPelicula);
        }

        // POST: topsPeliculas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.topsPelicula == null)
            {
                return Problem("Entity set 'tomates_podridosContext.topsPelicula'  is null.");
            }
            var topsPelicula = await _context.topsPelicula.FindAsync(id);
            if (topsPelicula != null)
            {
                _context.topsPelicula.Remove(topsPelicula);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool topsPeliculaExists(int id)
        {
          return (_context.topsPelicula?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
