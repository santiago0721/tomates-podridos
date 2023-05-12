using System;
using System.Collections.Generic;
using System.ComponentModel;
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
                
                ViewBag.men = "esta url no existe";
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
                if (ModelState.IsValid)
                {
                    if (link.Split("/")[3] == "tv") 
                    {
                        var show = this.crear_show(document);            
                        var validar = _context.Show.Any(x => x.nombre == show.nombre);
                        if (validar) 
                        {
                            ViewBag.men = "Ya existe en la base de datos";
                        }
                        else 
                        {
                            _context.Show.Add(show);
                            await  _context.SaveChangesAsync();


                            int show_id = show.Id;
                            this.reviews_show(link, show_id);

                            ViewBag.men = $"trabajo realizado,|   show cargado con exito    | cargada con exito ";

                        }
                    }


                    else
                    {
                        var pelicula = this.crear(document);
                        var validar = _context.Pelicula.Any(x => x.nombre == pelicula.nombre);

                        if (validar)
                        {
                            ViewBag.men = "Ya existe en la base de datos";
                        }
                        else
                        {
                            _context.Add(pelicula);
                            await _context.SaveChangesAsync();

                            int pelicula_id = pelicula.Id;

                            //se invoca reviews para cargar esta informacion de los comentarios
                            this.reviews(link, pelicula_id);


                            this.Crear_genero(document, pelicula_id);


                            ViewBag.men = $"trabajo realizado,|    {titulo_pelicula}    | cargada con exito ";

                        }
                       
                    }
                    return View();
                }
                ViewBag.men = $"no es valido,|    {titulo_pelicula}    | pailasssss";
                return View();
            }
            

        }






        public IActionResult tops()

        {
            
            var Response = this.call_url("https://www.rottentomatoes.com").Result;
            var document = this.parse_html(Response);
            var lista_links = this.links(document);


            var peliculas = this.PeliculasTop(lista_links[0]);
            var shows = this.ShowsTop(lista_links[1]);
            this.ExisteShow(shows);
           



                return View();
        }

        




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






        // datos pelicula

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


            if (total == "") 
            { return total; }
            else { return total[..^1]; }
            
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





        //reviews




        //se debe agregar  /review a la url al final
        public async void reviewCriticos(HtmlDocument htmlDoc,int id)
        {
            var critica = htmlDoc.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "").Contains("review-text"))
                .ToList();

            var nombre = htmlDoc.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "").Contains("reviewer-name-and-publication"))
                .ToList();

            
            for (int i = 0; i <= 2; i++)
            {
                var critica_ = new ComentarioCritica();
                critica_.Name = nombre[i].InnerText.Trim().Split("\n")[0].Trim();
                critica_.Description = critica[i].InnerText.Trim().Split("\n")[0].Trim();
                critica_.PeliculaId = id;
                _context.ComentarioCritica.Add(critica_);
                _context.SaveChangesAsync();

            }


        }

        //    /reviews?type=user    al final de la url inicial
        public async void reviewAudiencia(HtmlDocument htmlDoc, int id)
        {
            var critica = htmlDoc.DocumentNode.Descendants("p")
                .Where(node => node.GetAttributeValue("data-qa", "").Contains("review-text"))
                .ToList();

            var nombre = htmlDoc.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "").Contains("review-data"))
                .ToList();

            for (int i = 0; i <= 2; i++)
            {
                var critica_ = new ComentarioAudiencia();
                critica_.Name = nombre[i].InnerText.Trim().Split("\n")[0].Trim();
                critica_.Description = critica[i].InnerText.Trim().Split("\n")[0].Trim();
                critica_.PeliculaId = id;
                _context.ComentarioAudiencia.Add(critica_);
                _context.SaveChangesAsync();

               

            }


        }


        public async void reviewCriticos_show(HtmlDocument htmlDoc, int id)
        {
            var critica = htmlDoc.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "").Contains("review-text"))
                .ToList();

            var nombre = htmlDoc.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "").Contains("reviewer-name-and-publication"))
                .ToList();


            for (int i = 0; i <= 2; i++)
            {
                var critica_ = new ComentarioCritica_show();
                critica_.Name = nombre[i].InnerText.Trim().Split("\n")[0].Trim();
                critica_.Description = critica[i].InnerText.Trim().Split("\n")[0].Trim();
                critica_.ShowId = id;
                _context.ComentarioCritica_show.Add(critica_);
                _context.SaveChangesAsync();

            }


        }


        public async void reviewAudiencia_show(HtmlDocument htmlDoc, int id)
        {
            var critica = htmlDoc.DocumentNode.Descendants("p")
                .Where(node => node.GetAttributeValue("data-qa", "").Contains("review-text"))
                .ToList();

            var nombre = htmlDoc.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "").Contains("review-data"))
                .ToList();

            for (int i = 0; i <= 2; i++)
            {
                var critica_ = new ComentarioAudiencia_show();
                critica_.Name = nombre[i].InnerText.Trim().Split("\n")[0].Trim();
                critica_.Description = critica[i].InnerText.Trim().Split("\n")[0].Trim();
                critica_.ShowId = id;
                _context.ComentarioAudiencia_show.Add(critica_);
                _context.SaveChangesAsync();



            }


        }



        // crear 

        // se revisa que genero no existe en la tabla y se agrega
        public async void Crear_genero(HtmlDocument document,int id_pelicula)
        {

            var generos_pelicula = this.datos_principales(document)[1];
            List<string> data = this.generos(generos_pelicula);

            foreach (string genero_ in data)
            {
                System.Threading.Thread.Sleep(5000);
                if (!(_context.genero.Any(x => x.Name == genero_)))
                {
                    genero genero = new genero();
                    genero.Name = genero_;

                    _context.genero.Add(genero);
                    _context.SaveChangesAsync();

                }
                
            }
            System.Threading.Thread.Sleep(5000);

            this.CrearPeliculaGenero(this.Consultar_GeneroId(data),id_pelicula);
           
        }

        //se crea un objeto pelicula
        public Pelicula crear(HtmlDocument document)
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
            pelicula.duracion = datos[4];
            pelicula.actores = this.actores(document);

            return pelicula;

        }


        public Show crear_show(HtmlDocument document) 
        {
            Show show = new Show();
            show.nombre = this.titulo_show(document);
            show.img = this.img(document);
            show.calCritica = this.tomatometerscore(document);
            show.calAudiencia = this.audiencescore(document);
            show.plataformas = this.plataformas(document);

            List<String> datos_principales = this.datos_principales_show(document);
            show.produccion = datos_principales[2];
            show.fecha = datos_principales[0];
            show.actores = this.actores(document);
            show.genero = datos_principales[1];


            return show;
        
        }





        //referencias entre tablas




        //llama los metodos de review y sube a base de datos
        public void reviews(string link, int id)
        {
            link += "/reviews";

            var Response = this.call_url(link).Result;
            HtmlDocument document = this.parse_html(Response);

            this.reviewCriticos(document, id);

            link += "?type=user";

            Response = this.call_url(link).Result;
            document = this.parse_html(Response);

            this.reviewAudiencia(document, id);


        }


        public void reviews_show(string link, int id)
        {
            link += "/reviews";

            var Response = this.call_url(link).Result;
            HtmlDocument document = this.parse_html(Response);

            this.reviewCriticos_show(document, id);

            link += "?type=user";

            Response = this.call_url(link).Result;
            document = this.parse_html(Response);

            this.reviewAudiencia_show(document, id);


        }







        public List<int> Consultar_GeneroId(List<string> generos) 
        
        {
            List<int> generos_id = new List<int>();

            foreach(string genero in generos) 
            {
                generos_id.Add((from x in _context.genero where x.Name == genero select x.Id).FirstOrDefault()); 
            }

            return generos_id;
        }


        public async void CrearPeliculaGenero(List<int> id_genero,int id_pelicula) 
        {
            foreach(int id_ge in id_genero) 
            {
                Peliculagenero data = new Peliculagenero();
                data.generoId = id_ge;
                data.PeliculaId = id_pelicula;
                _context.Peliculagenero.Add(data);
                _context.SaveChangesAsync();
            }
        }



        //show 


        public string titulo_show(HtmlDocument htmlDoc)
        {
            var nombre_pelicula = htmlDoc.DocumentNode.Descendants("img")
                .Where(node => node.ParentNode.GetAttributeValue("class", "").Contains("thumbnail")).
                ToList().First().GetAttributeValue("alt", " ")[..^12];

            return nombre_pelicula;
        }

        public List<string> datos_principales_show(HtmlDocument htmlDoc)

        {
            string genero = "none";
            string Premiere_date = "none";
            string Executive_producers = "none";


            var info_central_ = htmlDoc.DocumentNode.Descendants("li")
                .Where(node => node.GetAttributeValue("class", "").Contains("info-item")).ToList();



            foreach (var node in info_central_)
            {
                if (!(node == info_central_[0]))
                {
                    var titulo = node.Descendants("b").Where(node => node.GetAttributeValue("class", "")
                    .Contains("info-item-label")).ToList().First().InnerText;


                    var valor = node.Descendants("span").Where(node => node.GetAttributeValue("class", "")
                    .Contains("info-item-value")).ToList().First().InnerText;

                    if (titulo == "Premiere Date:") { Premiere_date = valor.Trim(); }

                    else if (titulo == "Executive producers:") { Executive_producers = valor.Trim(); }
                }
                else
                {
                    genero = node.Descendants("span").Where(node => node.GetAttributeValue("class", "")
                    .Contains("info-item-value")).ToList().First().InnerText.Trim();
                }
            }

            Console.WriteLine(Premiere_date);
            Console.WriteLine(genero);
            Console.WriteLine(Executive_producers);

            List<string> lista = new List<string>() { Premiere_date, genero, Executive_producers };
            return lista;
        }



        // tops

        //retorna una lista de dos listas
        //la primera lista tiene los links de los tops de peliculas ----- la segunda tops de shows


        public List<List<string>> links(HtmlDocument htmlDoc)
        {
            var datos = htmlDoc.DocumentNode.Descendants("ul")
                .Where(node => node.GetAttributeValue("slot", "").Contains("list-items"))
                .ToList();


            var peliculas = datos[0].Descendants("li").ToList();
            var shows = datos[1].Descendants("li").ToList();

            List<string> links_peliculas = new List<string>();
            List<string> links_shows = new List<string>();
            List<List<string>> links_ = new List<List<string>>();

            for (int i = 0; i < 10; i++)
            {
                var link_pelicula = peliculas[i].Descendants("a")
                .Where(node => node.GetAttributeValue("class", "").Contains("dynamic-text-list__tomatometer-group"))
                .ToList().First().GetAttributeValue("href", " ");

                links_peliculas.Add(link_pelicula);

                var link_show = shows[i].Descendants("a")
                .Where(node => node.GetAttributeValue("class", "").Contains("dynamic-text-list__tomatometer-group"))
                .ToList().First().GetAttributeValue("href", " ");

                links_shows.Add(link_show);
            }


            links_.Add(links_peliculas);
            links_.Add(links_shows);

            return links_;

        }




        public List<Pelicula> PeliculasTop(List<string> links) 
        {
        List<Pelicula> top = new List<Pelicula>();

            foreach (var link in links) 
            {
                var link_completo = "https://www.rottentomatoes.com" + link;
                var Response = this.call_url(link_completo).Result;
                var document = this.parse_html(Response);
                var peli = this.crear(document);
                top.Add(peli);
            }
            return top;
        }

        public List<Show> ShowsTop(List<string> links)
        {
            List<Show> top = new List<Show>();

            foreach (var link in links)
            {
                var link_completo = "https://www.rottentomatoes.com/" + link;
                var Response = this.call_url(link_completo).Result;
                var document = this.parse_html(Response);
                var peli = this.crear_show(document);
                top.Add(peli);
            }
            return top;
        }


        // recibe los tops de peliculas y los que no existen los agrega a la base de datos
        public async Task ExistePelicula(List<Pelicula> peliculas) 
        {
            foreach(var peli in peliculas) 
            {
                var respuesta = _context.Pelicula.Any(x => x.nombre == peli.nombre);
                if (!(respuesta)) 
                {
                    _context.Pelicula.Add(peli);
                    await _context.SaveChangesAsync();
                }
            }
        
        }

        public async Task ExisteShow(List<Show> shows)
        {
            foreach (var show_ in shows)
            {
                var respuesta = _context.Show.Any(x => x.nombre == show_.nombre);
                if (!(respuesta))
                {
                    _context.Show.Add(show_);
                    await _context.SaveChangesAsync();
                }

                topsShows top = new topsShows();
                top.ShowId = show_.Id;
                _context.topsShows.Add(top);
                await _context.SaveChangesAsync();
            }

        }


        public async Task guardar_datos(List<Pelicula> peliculas,List<Show> shows) 
        {
            foreach(var peli in peliculas) 
            {
                topsPelicula top = new topsPelicula();
                top.PeliculaId = peli.Id;
                _context.topsPelicula.Add(top);
                await _context.SaveChangesAsync();

            }

            foreach(var show_ in shows) 
            {
                topsShows top = new topsShows();
                top.ShowId = show_.Id;
                _context.topsShows.Add(top);
                await _context.SaveChangesAsync();
            }
        
        }

    }
}
