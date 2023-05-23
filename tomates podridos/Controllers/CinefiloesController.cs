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
    public class CinefiloesController : Controller
    {
        private readonly tomates_podridosContext _context;

        public CinefiloesController(tomates_podridosContext context)
        {
            _context = context;
        }

        // GET: Cinefiloes
        public async Task<IActionResult> Index()
        {
              return _context.Cinefilo != null ? 
                          View(await _context.Cinefilo.ToListAsync()) :
                          Problem("Entity set 'tomates_podridosContext.Cinefilo'  is null.");
        }

        // GET: Cinefiloes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cinefilo == null)
            {
                return NotFound();
            }

            var cinefilo = await _context.Cinefilo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cinefilo == null)
            {
                return NotFound();
            }

            return View(cinefilo);
        }

        // GET: Cinefiloes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cinefiloes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,password")] Cinefilo cinefilo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cinefilo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cinefilo);
        }

        // GET: Cinefiloes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cinefilo == null)
            {
                return NotFound();
            }

            var cinefilo = await _context.Cinefilo.FindAsync(id);
            if (cinefilo == null)
            {
                return NotFound();
            }
            return View(cinefilo);
        }

        // POST: Cinefiloes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,password")] Cinefilo cinefilo)
        {
            if (id != cinefilo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cinefilo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CinefiloExists(cinefilo.Id))
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
            return View(cinefilo);
        }

        // GET: Cinefiloes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cinefilo == null)
            {
                return NotFound();
            }

            var cinefilo = await _context.Cinefilo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cinefilo == null)
            {
                return NotFound();
            }

            return View(cinefilo);
        }

        // POST: Cinefiloes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cinefilo == null)
            {
                return Problem("Entity set 'tomates_podridosContext.Cinefilo'  is null.");
            }
            var cinefilo = await _context.Cinefilo.FindAsync(id);
            if (cinefilo != null)
            {
                _context.Cinefilo.Remove(cinefilo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CinefiloExists(int id)
        {
          return (_context.Cinefilo?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public IActionResult login()

        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string name, string password)
        {

            var validar = await _context.Cinefilo
            .FirstOrDefaultAsync(m => m.password == password && m.Name == name);
            if (validar != null)
            {
                ViewBag.Name = name;
                ViewBag.Password = password;
                return RedirectToAction(nameof(menu));

            }
            else
            {
                ViewBag.mensaje = "datos No validos";
                return View();
            }

        }


        public IActionResult menu()

        {
            List<Pelicula> lista_peliculas = this.Buscar_peliculas();
            List<Show> lista_shows = this.Buscar_shows();
            List<Pelicula> lista_tops_peliculas = this.buscar_tops_peliculas();
            List<Show> lista_tops_shows = this.buscar_tops_shows();    

            ViewBag.peliculas = lista_peliculas;
            ViewBag.Shows = lista_shows;
            ViewBag.peliculas_tops = lista_tops_peliculas;
            ViewBag.Shows_tops = lista_tops_shows;


            ViewBag.self = this;
            return View();
        }


        public string mandar_genero(int? id)
        {
            var consulta = (from p in _context.Peliculagenero where p.PeliculaId == id select p.generoId);

            if (consulta.Count() == 0) { return null; }


            var resultado = consulta.ToList();
            string generos = "";
            foreach (int genero in resultado)
            {
                generos += (from g in _context.genero where g.Id == genero select g.Name).FirstAsync().Result + "--";
            }

            return generos[..^2];


        }


        public List<Pelicula> Buscar_peliculas() 
        {
            var consulta = from c in _context.Pelicula select c;

            if (consulta.Count() == 0) { return null; }

            else
            {
                return consulta.ToList();
            }
        }

        public List<Show> Buscar_shows() 
        {
            var consulta = from c in _context.Show select c;

            if (consulta.Count() == 0) { return null; }

            else
            {
                return consulta.ToList();
            }

        }
        

        


        public List<Pelicula> buscar_tops_peliculas() 
        {

            List<Pelicula> lista_peliculas = new List<Pelicula>();

            var consulta = from c in _context.topsPelicula select c.PeliculaId;

            if (consulta.Count() == 0) { return null; }

            else
            {

                foreach (var pelicula_id in consulta)
                {
                    Pelicula pelicula = (from g in _context.Pelicula where g.Id == pelicula_id select g).FirstAsync().Result;
                    lista_peliculas.Add(pelicula);
                }

                return lista_peliculas;

            }

        }

        public List<Show> buscar_tops_shows()
        {

            var consulta = from c in _context.topsShows select c.ShowId;
            List<Show> lista_shows = new List<Show>();

            if (consulta.Count() == 0) { return null; }

            else
            {
                foreach (var show_id in consulta)
                {
                    Show show = (from g in _context.Show where g.Id == show_id select g).FirstAsync().Result;
                    lista_shows.Add(show);
                }

                return lista_shows;
            }

        }

    }
}
