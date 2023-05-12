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
    public class ComentarioCritica_showController : Controller
    {
        private readonly tomates_podridosContext _context;

        public ComentarioCritica_showController(tomates_podridosContext context)
        {
            _context = context;
        }

        // GET: ComentarioCritica_show
        public async Task<IActionResult> Index()
        {
            var tomates_podridosContext = _context.ComentarioCritica_show.Include(c => c.Show);
            return View(await tomates_podridosContext.ToListAsync());
        }

        // GET: ComentarioCritica_show/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ComentarioCritica_show == null)
            {
                return NotFound();
            }

            var comentarioCritica_show = await _context.ComentarioCritica_show
                .Include(c => c.Show)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comentarioCritica_show == null)
            {
                return NotFound();
            }

            return View(comentarioCritica_show);
        }

        // GET: ComentarioCritica_show/Create
        public IActionResult Create()
        {
            ViewData["ShowId"] = new SelectList(_context.Show, "Id", "Id");
            return View();
        }

        // POST: ComentarioCritica_show/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,ShowId")] ComentarioCritica_show comentarioCritica_show)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comentarioCritica_show);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ShowId"] = new SelectList(_context.Show, "Id", "Id", comentarioCritica_show.ShowId);
            return View(comentarioCritica_show);
        }

        // GET: ComentarioCritica_show/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ComentarioCritica_show == null)
            {
                return NotFound();
            }

            var comentarioCritica_show = await _context.ComentarioCritica_show.FindAsync(id);
            if (comentarioCritica_show == null)
            {
                return NotFound();
            }
            ViewData["ShowId"] = new SelectList(_context.Show, "Id", "Id", comentarioCritica_show.ShowId);
            return View(comentarioCritica_show);
        }

        // POST: ComentarioCritica_show/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,ShowId")] ComentarioCritica_show comentarioCritica_show)
        {
            if (id != comentarioCritica_show.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comentarioCritica_show);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComentarioCritica_showExists(comentarioCritica_show.Id))
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
            ViewData["ShowId"] = new SelectList(_context.Show, "Id", "Id", comentarioCritica_show.ShowId);
            return View(comentarioCritica_show);
        }

        // GET: ComentarioCritica_show/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ComentarioCritica_show == null)
            {
                return NotFound();
            }

            var comentarioCritica_show = await _context.ComentarioCritica_show
                .Include(c => c.Show)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comentarioCritica_show == null)
            {
                return NotFound();
            }

            return View(comentarioCritica_show);
        }

        // POST: ComentarioCritica_show/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ComentarioCritica_show == null)
            {
                return Problem("Entity set 'tomates_podridosContext.ComentarioCritica_show'  is null.");
            }
            var comentarioCritica_show = await _context.ComentarioCritica_show.FindAsync(id);
            if (comentarioCritica_show != null)
            {
                _context.ComentarioCritica_show.Remove(comentarioCritica_show);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComentarioCritica_showExists(int id)
        {
          return (_context.ComentarioCritica_show?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
