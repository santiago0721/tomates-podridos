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
    public class PeliculageneroesController : Controller
    {
        private readonly tomates_podridosContext _context;

        public PeliculageneroesController(tomates_podridosContext context)
        {
            _context = context;
        }

        // GET: Peliculageneroes
        public async Task<IActionResult> Index()
        {
            var tomates_podridosContext = _context.Peliculagenero.Include(p => p.Pelicula).Include(p => p.genero);
            return View(await tomates_podridosContext.ToListAsync());
        }

        // GET: Peliculageneroes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Peliculagenero == null)
            {
                return NotFound();
            }

            var peliculagenero = await _context.Peliculagenero
                .Include(p => p.Pelicula)
                .Include(p => p.genero)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (peliculagenero == null)
            {
                return NotFound();
            }

            return View(peliculagenero);
        }

        // GET: Peliculageneroes/Create
        public IActionResult Create()
        {
            ViewData["PeliculaId"] = new SelectList(_context.Pelicula, "Id", "Id");
            ViewData["generoId"] = new SelectList(_context.genero, "Id", "Id");
            return View();
        }

        // POST: Peliculageneroes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,generoId,PeliculaId")] Peliculagenero peliculagenero)
        {
            if (ModelState.IsValid)
            {
                _context.Add(peliculagenero);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PeliculaId"] = new SelectList(_context.Pelicula, "Id", "Id", peliculagenero.PeliculaId);
            ViewData["generoId"] = new SelectList(_context.genero, "Id", "Id", peliculagenero.generoId);
            return View(peliculagenero);
        }

        // GET: Peliculageneroes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Peliculagenero == null)
            {
                return NotFound();
            }

            var peliculagenero = await _context.Peliculagenero.FindAsync(id);
            if (peliculagenero == null)
            {
                return NotFound();
            }
            ViewData["PeliculaId"] = new SelectList(_context.Pelicula, "Id", "Id", peliculagenero.PeliculaId);
            ViewData["generoId"] = new SelectList(_context.genero, "Id", "Id", peliculagenero.generoId);
            return View(peliculagenero);
        }

        // POST: Peliculageneroes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,generoId,PeliculaId")] Peliculagenero peliculagenero)
        {
            if (id != peliculagenero.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(peliculagenero);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PeliculageneroExists(peliculagenero.Id))
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
            ViewData["PeliculaId"] = new SelectList(_context.Pelicula, "Id", "Id", peliculagenero.PeliculaId);
            ViewData["generoId"] = new SelectList(_context.genero, "Id", "Id", peliculagenero.generoId);
            return View(peliculagenero);
        }

        // GET: Peliculageneroes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Peliculagenero == null)
            {
                return NotFound();
            }

            var peliculagenero = await _context.Peliculagenero
                .Include(p => p.Pelicula)
                .Include(p => p.genero)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (peliculagenero == null)
            {
                return NotFound();
            }

            return View(peliculagenero);
        }

        // POST: Peliculageneroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Peliculagenero == null)
            {
                return Problem("Entity set 'tomates_podridosContext.Peliculagenero'  is null.");
            }
            var peliculagenero = await _context.Peliculagenero.FindAsync(id);
            if (peliculagenero != null)
            {
                _context.Peliculagenero.Remove(peliculagenero);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PeliculageneroExists(int id)
        {
          return (_context.Peliculagenero?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
