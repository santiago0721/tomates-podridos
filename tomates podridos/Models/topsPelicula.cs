namespace tomates_podridos.Models
{
    public class topsPelicula
    {
        public int Id { get; set; }
        public int PeliculaId { get; set; }

        public Pelicula Pelicula { get; set; }
    }
}
