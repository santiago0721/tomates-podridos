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
    public class topsShowsController : Controller
    {
        private readonly tomates_podridosContext _context;

        public topsShowsController(tomates_podridosContext context)
        {
            _context = context;
        }

        // GET: topsShows
        public async Task<IActionResult> Index()
        {
            var tomates_podridosContext = _context.topsShows.Include(t => t.Show);
            return View(await tomates_podridosContext.ToListAsync());
        }

        // GET: topsShows/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.topsShows == null)
            {
                return NotFound();
            }

            var topsShows = await _context.topsShows
                .Include(t => t.Show)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (topsShows == null)
            {
                return NotFound();
            }

            return View(topsShows);
        }

        // GET: topsShows/Create
        public IActionResult Create()
        {
            ViewData["ShowId"] = new SelectList(_context.Show, "Id", "Id");
            return View();
        }

        // POST: topsShows/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ShowId")] topsShows topsShows)
        {
            if (ModelState.IsValid)
            {
                _context.Add(topsShows);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ShowId"] = new SelectList(_context.Show, "Id", "Id", topsShows.ShowId);
            return View(topsShows);
        }

        // GET: topsShows/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.topsShows == null)
            {
                return NotFound();
            }

            var topsShows = await _context.topsShows.FindAsync(id);
            if (topsShows == null)
            {
                return NotFound();
            }
            ViewData["ShowId"] = new SelectList(_context.Show, "Id", "Id", topsShows.ShowId);
            return View(topsShows);
        }

        // POST: topsShows/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ShowId")] topsShows topsShows)
        {
            if (id != topsShows.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(topsShows);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!topsShowsExists(topsShows.Id))
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
            ViewData["ShowId"] = new SelectList(_context.Show, "Id", "Id", topsShows.ShowId);
            return View(topsShows);
        }

        // GET: topsShows/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.topsShows == null)
            {
                return NotFound();
            }

            var topsShows = await _context.topsShows
                .Include(t => t.Show)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (topsShows == null)
            {
                return NotFound();
            }

            return View(topsShows);
        }

        // POST: topsShows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.topsShows == null)
            {
                return Problem("Entity set 'tomates_podridosContext.topsShows'  is null.");
            }
            var topsShows = await _context.topsShows.FindAsync(id);
            if (topsShows != null)
            {
                _context.topsShows.Remove(topsShows);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool topsShowsExists(int id)
        {
          return (_context.topsShows?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
