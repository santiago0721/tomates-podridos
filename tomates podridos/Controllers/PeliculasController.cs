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
    public class PeliculasController : Controller
    {
        private readonly tomates_podridosContext _context;

        public PeliculasController(tomates_podridosContext context)
        {
            _context = context;
        }

        // GET: Peliculas
        public async Task<IActionResult> Index()
        {
              return _context.Pelicula != null ? 
                          View(await _context.Pelicula.ToListAsync()) :
                          Problem("Entity set 'tomates_podridosContext.Pelicula'  is null.");
        }

        // GET: Peliculas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pelicula == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Pelicula
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pelicula == null)
            {
                return NotFound();
            }

            ViewBag.generos = this.mandar_genero(id);
            ViewBag.audiencia = this.mandar_audiencia(id);
            ViewBag.critica = this.mandar_crtitica(id); 
            return View(pelicula);
        }



        //cargar los datos de las otras tablas
        public string mandar_genero(int? id) 
        {
            var consulta =  (from p in _context.Peliculagenero where p.PeliculaId == id select p.generoId);

            if (consulta.Count() == 0) { return null; }

           
            var resultado = consulta.ToList();
            string generos = "";
            foreach( int genero in resultado) 
            {
                generos += (from g in _context.genero where g.Id == genero select g.Name).FirstAsync().Result + "--";
            }

            return generos[..^2];


        }


        public List<List<string>> mandar_crtitica(int? id)
        {
            var consulta = (from p in _context.ComentarioCritica where p.PeliculaId == id select p);

            if (consulta.Count() == 0) { return null; }

            List<List<string>> critica = new List<List<string>>();
            
            foreach (ComentarioCritica critica_ in consulta)
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

            var consulta = (from p in _context.ComentarioAudiencia where p.PeliculaId == id select p);

            if (consulta.Count() == 0) { return null; }

            List<List<string>> audiencia = new List<List<string>>();

            foreach (ComentarioAudiencia critica_ in consulta)
            {
                List<string> aux = new List<string>();
                aux.Add(critica_.Name);
                aux.Add(critica_.Description);
                audiencia.Add(aux);

            }

            return audiencia;


        }

        // GET: Peliculas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Peliculas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,nombre,img,calCritica,calAudiencia,plataformas,synopsis,clasificacion,equipoDir,fecha,duracion,actores")] Pelicula pelicula)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pelicula);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pelicula);
        }

        // GET: Peliculas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pelicula == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Pelicula.FindAsync(id);
            if (pelicula == null)
            {
                return NotFound();
            }
            return View(pelicula);
        }





        // POST: Peliculas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,nombre,img,calCritica,calAudiencia,plataformas,synopsis,clasificacion,equipoDir,fecha,duracion,actores")] Pelicula pelicula)
        {
            if (id != pelicula.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pelicula);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PeliculaExists(pelicula.Id))
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
            return View(pelicula);
        }

        
        public async Task<IActionResult> info() 
        {
            return _context.Pelicula != null ?
                          View(await _context.Pelicula.ToListAsync()) :
                          Problem("Entity set 'tomates_podridosContext.Pelicula'  is null.");
        }


        // GET: Peliculas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pelicula == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Pelicula
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pelicula == null)
            {
                return NotFound();
            }

            return View(pelicula);
        }

        // POST: Peliculas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pelicula == null)
            {
                return Problem("Entity set 'tomates_podridosContext.Pelicula'  is null.");
            }
            var pelicula = await _context.Pelicula.FindAsync(id);
            if (pelicula != null)
            {
                _context.Pelicula.Remove(pelicula);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PeliculaExists(int id)
        {
          return (_context.Pelicula?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
