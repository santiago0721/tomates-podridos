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
    public class ComentarioCriticasController : Controller
    {
        private readonly tomates_podridosContext _context;

        public ComentarioCriticasController(tomates_podridosContext context)
        {
            _context = context;
        }

        // GET: ComentarioCriticas
        public async Task<IActionResult> Index()
        {
              return _context.ComentarioCritica != null ? 
                          View(await _context.ComentarioCritica.ToListAsync()) :
                          Problem("Entity set 'tomates_podridosContext.ComentarioCritica'  is null.");
        }

        // GET: ComentarioCriticas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ComentarioCritica == null)
            {
                return NotFound();
            }

            var comentarioCritica = await _context.ComentarioCritica
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comentarioCritica == null)
            {
                return NotFound();
            }

            return View(comentarioCritica);
        }

        // GET: ComentarioCriticas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ComentarioCriticas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] ComentarioCritica comentarioCritica)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comentarioCritica);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(comentarioCritica);
        }

        // GET: ComentarioCriticas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ComentarioCritica == null)
            {
                return NotFound();
            }

            var comentarioCritica = await _context.ComentarioCritica.FindAsync(id);
            if (comentarioCritica == null)
            {
                return NotFound();
            }
            return View(comentarioCritica);
        }

        // POST: ComentarioCriticas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] ComentarioCritica comentarioCritica)
        {
            if (id != comentarioCritica.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comentarioCritica);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComentarioCriticaExists(comentarioCritica.Id))
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
            return View(comentarioCritica);
        }

        // GET: ComentarioCriticas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ComentarioCritica == null)
            {
                return NotFound();
            }

            var comentarioCritica = await _context.ComentarioCritica
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comentarioCritica == null)
            {
                return NotFound();
            }

            return View(comentarioCritica);
        }

        // POST: ComentarioCriticas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ComentarioCritica == null)
            {
                return Problem("Entity set 'tomates_podridosContext.ComentarioCritica'  is null.");
            }
            var comentarioCritica = await _context.ComentarioCritica.FindAsync(id);
            if (comentarioCritica != null)
            {
                _context.ComentarioCritica.Remove(comentarioCritica);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComentarioCriticaExists(int id)
        {
          return (_context.ComentarioCritica?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
