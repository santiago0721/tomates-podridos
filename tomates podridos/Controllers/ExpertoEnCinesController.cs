using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using tomates_podridos.Data;
using tomates_podridos.Models;

namespace tomates_podridos.Controllers
{
    public class ExpertoEnCinesController : Controller
    {
        private readonly tomates_podridosContext _context;

        public ExpertoEnCinesController(tomates_podridosContext context)
        {
            _context = context;
        }

        // GET: ExpertoEnCines
        public async Task<IActionResult> Index()
        {
              return _context.ExpertoEnCine != null ? 
                          View(await _context.ExpertoEnCine.ToListAsync()) :
                          Problem("Entity set 'tomates_podridosContext.ExpertoEnCine'  is null.");
        }

        // GET: ExpertoEnCines/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ExpertoEnCine == null)
            {
                return NotFound();
            }

            var expertoEnCine = await _context.ExpertoEnCine
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expertoEnCine == null)
            {
                return NotFound();
            }

            return View(expertoEnCine);
        }

        // GET: ExpertoEnCines/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ExpertoEnCines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,password")] ExpertoEnCine expertoEnCine)
        {
            if (ModelState.IsValid)
            {
                _context.Add(expertoEnCine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(expertoEnCine);
        }

        // GET: ExpertoEnCines/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ExpertoEnCine == null)
            {
                return NotFound();
            }

            var expertoEnCine = await _context.ExpertoEnCine.FindAsync(id);
            if (expertoEnCine == null)
            {
                return NotFound();
            }
            return View(expertoEnCine);
        }

        // POST: ExpertoEnCines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,password")] ExpertoEnCine expertoEnCine)
        {
            if (id != expertoEnCine.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expertoEnCine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpertoEnCineExists(expertoEnCine.Id))
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
            return View(expertoEnCine);
        }

        // GET: ExpertoEnCines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ExpertoEnCine == null)
            {
                return NotFound();
            }

            var expertoEnCine = await _context.ExpertoEnCine
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expertoEnCine == null)
            {
                return NotFound();
            }

            return View(expertoEnCine);
        }

        // POST: ExpertoEnCines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ExpertoEnCine == null)
            {
                return Problem("Entity set 'tomates_podridosContext.ExpertoEnCine'  is null.");
            }
            var expertoEnCine = await _context.ExpertoEnCine.FindAsync(id);
            if (expertoEnCine != null)
            {
                _context.ExpertoEnCine.Remove(expertoEnCine);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpertoEnCineExists(int id)
        {
          return (_context.ExpertoEnCine?.Any(e => e.Id == id)).GetValueOrDefault();
        }


        public IActionResult login()

        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string name, string password)
        {

            var validar = await _context.ExpertoEnCine
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
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> menu(string link) 
        
        {
            
            HtmlDocument document;
            try
            {
                var Response = this.call_url(link).Result;
                document = this.parse_html(Response);
                

            }
            catch (Exception)
            {
                
                ViewBag.men = "usta url no existe";
                return View();
            }

            string titulo_pelicula = this.titulo_pelicula(document);
            
            if (titulo_pelicula == "#") // url valida o no
            
                {
                // url no valida 
                ViewBag.men = "esta url noooooooo";
                return View();
            }
            else 
            {
                // url valida
                ViewBag.men = $"trabajo realizado,|    {titulo_pelicula}    | cargada con exito";
                return View();
            }
            

        }


        /*public Pelicula crear(HtmlDocument document) 
        {
            Pelicula pelicula = new Pelicula();
            pelicula.nombre = this.titulo_pelicula(document);
            pelicula.img = this.img(document);
            pelicula.calCritica = this.tomatometerscore(document);
            pelicula.calAudiencia = this.audiencescore(document);
            pelicula.plataformas = this.plataformas(document);
            pelicula.synopsis = this.synopsis(document);
            var datos = this.datos_principales(document);
            pelicula.clasificacion = datos[0];
            pelicula.equipoDir = datos[2];
            pelicula.fecha = datos[3];
            pelicula.duracion= datos[4];
            pelicula.actores= this.actores(document);


        
        }*/

        // web scraping

        //conexion
        HtmlDocument parse_html(string html)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            return htmlDoc;

        }
        async Task<string> call_url(string fullUrl)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync(fullUrl);
            return response;
        }


        // datos a traer

        public string audiencescore(HtmlDocument htmlDoc)
        {
            var audiencescore = htmlDoc.DocumentNode.Descendants("score-board")
                .Where(node => node.GetAttributeValue("id", "").Contains("scoreboard"))
                .ToList().First().GetAttributeValue("audiencescore", "");

            return audiencescore;
        }

        public string tomatometerscore(HtmlDocument htmlDoc)
        {
            var tomatometerscore = htmlDoc.DocumentNode.Descendants("score-board")
                .Where(node => node.GetAttributeValue("id", "").Contains("scoreboard"))
                .ToList().First().GetAttributeValue("tomatometerscore", "");
            return tomatometerscore;
        }
        

        public string titulo_pelicula(HtmlDocument htmlDoc)
        {
            string nombre_pelicula;
            try
            {
                nombre_pelicula = htmlDoc.DocumentNode.Descendants("img")
                    .Where(node => node.ParentNode.GetAttributeValue("class", "").Contains("thumbnail")).
                    ToList().First().GetAttributeValue("alt", " ").Remove(0, 18);
            }
            catch (Exception)
            {
                return "#";       //representa que no se puede traer esta informacion con el web scraping
            }
            return nombre_pelicula;
        }

        public string img(HtmlDocument htmlDoc)
        {
            var img = htmlDoc.DocumentNode.Descendants("img")
                    .Where(node => node.ParentNode.GetAttributeValue("class", "").Contains("thumbnail")).
                    ToList().First().GetAttributeValue("src", " ").Trim();
            return img;

        }

        public string synopsis(HtmlDocument htmlDoc)
        {
            var synopsis = htmlDoc.DocumentNode.Descendants("p")
                .Where(node => node.GetAttributeValue("data-qa", "").
                Contains("movie-info-synopsis")).ToList().First().InnerText.Trim();
            Console.WriteLine(synopsis);

            return synopsis;

        }

        public string plataformas(HtmlDocument htmlDoc)
        {
            var plataformas = htmlDoc.DocumentNode.Descendants("where-to-watch-meta")
                .Where(node => node.GetAttributeValue("data-qa", "").Contains("affiliate-item"))
                .ToList();

            string total = "";
            foreach (var node in plataformas)
            {
                total += node.GetAttributeValue("affiliate", "") + ",";

            }

            return total[..^1];
        }

        public List<string> datos_principales(HtmlDocument htmlDoc)

        {
            string clasificacion = "none";
            string genero = "none";
            string equipo_dir = "none";
            string fecha = "none";
            string duracion = "none";

            var info_central_ = htmlDoc.DocumentNode.Descendants("li")
                .Where(node => node.GetAttributeValue("class", "").Contains("info-item")).ToList();



            foreach (var node in info_central_)
            {
                var titulo = node.Descendants("b").Where(node => node.GetAttributeValue("class", "")
                .Contains("info-item-label")).ToList().First().InnerText;


                var valor = node.Descendants("span").Where(node => node.GetAttributeValue("class", "")
                .Contains("info-item-value")).ToList().First().InnerText;

                if (titulo == "Rating:") { clasificacion = valor.Trim(); }

                else if (titulo == "Genre:") { genero = valor.Trim(); }

                else if (titulo == "Director:") { equipo_dir = valor.Trim(); }

                else if (titulo == "Release Date (Theaters):") { fecha = valor.Trim().Substring(0, 12); }

                else if (titulo == "Runtime:") { duracion = valor.Trim(); }
            }

            List<string> lista = new List<string>() { clasificacion, genero, equipo_dir, fecha, duracion };
            return lista;
        }

        //pasar una lista con los generos de cada pelicula
        public List<string> generos(string data)
        {
            List<string> retorno = new List<string>();
            var lista = data.Split("\n");

            foreach (string line in lista)
            {
                if (line == lista.Last()) { retorno.Add(line.Trim()); }

                else
                {
                    if (!string.IsNullOrEmpty(line.Trim())) { retorno.Add(line.Trim()[..^1]); }

                }
            }
            Console.WriteLine(retorno.Count);
            return retorno;
        }

        public string actores(HtmlDocument htmlDoc)
        {
            var reparto = htmlDoc.DocumentNode.Descendants("div")
                .Where(node => node.ParentNode.GetAttributeValue("class", "").Contains("cast-and-crew-item ")).
                ToList();

            string Actores = "";

            for (int i = 0; i < 5; i++)
            {
                var actual = reparto[i].InnerText.Trim();
                Actores += "," + actual.Split("\n")[0].Trim();
            }


            return Actores.Substring(1);
        }




    }
}
