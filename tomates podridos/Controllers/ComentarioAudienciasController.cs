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
    public class ComentarioAudienciasController : Controller
    {
        private readonly tomates_podridosContext _context;

        public ComentarioAudienciasController(tomates_podridosContext context)
        {
            _context = context;
        }

        // GET: ComentarioAudiencias
        public async Task<IActionResult> Index()
        {
              return _context.ComentarioAudiencia != null ? 
                          View(await _context.ComentarioAudiencia.ToListAsync()) :
                          Problem("Entity set 'tomates_podridosContext.ComentarioAudiencia'  is null.");
        }

        // GET: ComentarioAudiencias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ComentarioAudiencia == null)
            {
                return NotFound();
            }

            var comentarioAudiencia = await _context.ComentarioAudiencia
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comentarioAudiencia == null)
            {
                return NotFound();
            }

            return View(comentarioAudiencia);
        }

        // GET: ComentarioAudiencias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ComentarioAudiencias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] ComentarioAudiencia comentarioAudiencia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comentarioAudiencia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(comentarioAudiencia);
        }

        // GET: ComentarioAudiencias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ComentarioAudiencia == null)
            {
                return NotFound();
            }

            var comentarioAudiencia = await _context.ComentarioAudiencia.FindAsync(id);
            if (comentarioAudiencia == null)
            {
                return NotFound();
            }
            return View(comentarioAudiencia);
        }

        // POST: ComentarioAudiencias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] ComentarioAudiencia comentarioAudiencia)
        {
            if (id != comentarioAudiencia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comentarioAudiencia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComentarioAudienciaExists(comentarioAudiencia.Id))
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
            return View(comentarioAudiencia);
        }

        // GET: ComentarioAudiencias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ComentarioAudiencia == null)
            {
                return NotFound();
            }

            var comentarioAudiencia = await _context.ComentarioAudiencia
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comentarioAudiencia == null)
            {
                return NotFound();
            }

            return View(comentarioAudiencia);
        }

        // POST: ComentarioAudiencias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ComentarioAudiencia == null)
            {
                return Problem("Entity set 'tomates_podridosContext.ComentarioAudiencia'  is null.");
            }
            var comentarioAudiencia = await _context.ComentarioAudiencia.FindAsync(id);
            if (comentarioAudiencia != null)
            {
                _context.ComentarioAudiencia.Remove(comentarioAudiencia);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComentarioAudienciaExists(int id)
        {
          return (_context.ComentarioAudiencia?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
