namespace tomates_podridos.Models
{
    public class Peliculagenero
    {

        public int Id { get; set; }
        public int generoId { get; set; }

        public genero genero { get; set; }

        public int PeliculaId { get; set; }

        public Pelicula Pelicula { get; set; }


    }
}
