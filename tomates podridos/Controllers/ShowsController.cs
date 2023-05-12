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
    public class ShowsController : Controller
    {
        private readonly tomates_podridosContext _context;

        public ShowsController(tomates_podridosContext context)
        {
            _context = context;
        }

        // GET: Shows
        public async Task<IActionResult> Index()
        {
              return _context.Show != null ? 
                          View(await _context.Show.ToListAsync()) :
                          Problem("Entity set 'tomates_podridosContext.Show'  is null.");
        }

        // GET: Shows/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Show == null)
            {
                return NotFound();
            }

            var show = await _context.Show
                .FirstOrDefaultAsync(m => m.Id == id);
            if (show == null)
            {
                return NotFound();
            }
            ViewBag.audiencia = this.mandar_audiencia(id);
            ViewBag.critica = this.mandar_crtitica(id);
            return View(show);
        }



        public List<List<string>> mandar_crtitica(int? id)
        {
            var consulta = (from p in _context.ComentarioCritica_show where p.ShowId == id select p);

            if (consulta.Count() == 0) { return null; }

            List<List<string>> critica = new List<List<string>>();

            foreach (ComentarioCritica_show critica_ in consulta)
            {
                List<string> aux = new List<string>();
                aux.Add(critica_.Name);
                aux.Add(critica_.Description);
                critica.Add(aux);

            }

            return critica;


        }

        public List<List<string>> mandar_audiencia(int? id)
        {

            var consulta = (from p in _context.ComentarioAudiencia_show where p.ShowId == id select p);

            if (consulta.Count() == 0) { return null; }

            List<List<string>> audiencia = new List<List<string>>();

            foreach (ComentarioAudiencia_show critica_ in consulta)
            {
                List<string> aux = new List<string>();
                aux.Add(critica_.Name);
                aux.Add(critica_.Description);
                audiencia.Add(aux);

            }

            return audiencia;


        }





        // GET: Shows/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Shows/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,nombre,img,calCritica,calAudiencia,plataformas,produccion,fecha,actores,genero")] Show show)
        {
            if (ModelState.IsValid)
            {
                _context.Add(show);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(show);
        }

        // GET: Shows/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Show == null)
            {
                return NotFound();
            }

            var show = await _context.Show.FindAsync(id);
            if (show == null)
            {
                return NotFound();
            }
            return View(show);
        }

        // POST: Shows/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,nombre,img,calCritica,calAudiencia,plataformas,produccion,fecha,actores,genero")] Show show)
        {
            if (id != show.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(show);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShowExists(show.Id))
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
            return View(show);
        }

        // GET: Shows/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Show == null)
            {
                return NotFound();
            }

            var show = await _context.Show
                .FirstOrDefaultAsync(m => m.Id == id);
            if (show == null)
            {
                return NotFound();
            }

            return View(show);
        }

        // POST: Shows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Show == null)
            {
                return Problem("Entity set 'tomates_podridosContext.Show'  is null.");
            }
            var show = await _context.Show.FindAsync(id);
            if (show != null)
            {
                _context.Show.Remove(show);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShowExists(int id)
        {
          return (_context.Show?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
