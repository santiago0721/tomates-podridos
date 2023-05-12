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
    public class ComentarioAudiencia_showController : Controller
    {
        private readonly tomates_podridosContext _context;

        public ComentarioAudiencia_showController(tomates_podridosContext context)
        {
            _context = context;
        }

        // GET: ComentarioAudiencia_show
        public async Task<IActionResult> Index()
        {
            var tomates_podridosContext = _context.ComentarioAudiencia_show.Include(c => c.Show);
            return View(await tomates_podridosContext.ToListAsync());
        }

        // GET: ComentarioAudiencia_show/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ComentarioAudiencia_show == null)
            {
                return NotFound();
            }

            var comentarioAudiencia_show = await _context.ComentarioAudiencia_show
                .Include(c => c.Show)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comentarioAudiencia_show == null)
            {
                return NotFound();
            }

            return View(comentarioAudiencia_show);
        }

        // GET: ComentarioAudiencia_show/Create
        public IActionResult Create()
        {
            ViewData["ShowId"] = new SelectList(_context.Show, "Id", "Id");
            return View();
        }

        // POST: ComentarioAudiencia_show/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,ShowId")] ComentarioAudiencia_show comentarioAudiencia_show)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comentarioAudiencia_show);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ShowId"] = new SelectList(_context.Show, "Id", "Id", comentarioAudiencia_show.ShowId);
            return View(comentarioAudiencia_show);
        }

        // GET: ComentarioAudiencia_show/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ComentarioAudiencia_show == null)
            {
                return NotFound();
            }

            var comentarioAudiencia_show = await _context.ComentarioAudiencia_show.FindAsync(id);
            if (comentarioAudiencia_show == null)
            {
                return NotFound();
            }
            ViewData["ShowId"] = new SelectList(_context.Show, "Id", "Id", comentarioAudiencia_show.ShowId);
            return View(comentarioAudiencia_show);
        }

        // POST: ComentarioAudiencia_show/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,ShowId")] ComentarioAudiencia_show comentarioAudiencia_show)
        {
            if (id != comentarioAudiencia_show.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comentarioAudiencia_show);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComentarioAudiencia_showExists(comentarioAudiencia_show.Id))
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
            ViewData["ShowId"] = new SelectList(_context.Show, "Id", "Id", comentarioAudiencia_show.ShowId);
            return View(comentarioAudiencia_show);
        }

        // GET: ComentarioAudiencia_show/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ComentarioAudiencia_show == null)
            {
                return NotFound();
            }

            var comentarioAudiencia_show = await _context.ComentarioAudiencia_show
                .Include(c => c.Show)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comentarioAudiencia_show == null)
            {
                return NotFound();
            }

            return View(comentarioAudiencia_show);
        }

        // POST: ComentarioAudiencia_show/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ComentarioAudiencia_show == null)
            {
                return Problem("Entity set 'tomates_podridosContext.ComentarioAudiencia_show'  is null.");
            }
            var comentarioAudiencia_show = await _context.ComentarioAudiencia_show.FindAsync(id);
            if (comentarioAudiencia_show != null)
            {
                _context.ComentarioAudiencia_show.Remove(comentarioAudiencia_show);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComentarioAudiencia_showExists(int id)
        {
          return (_context.ComentarioAudiencia_show?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
