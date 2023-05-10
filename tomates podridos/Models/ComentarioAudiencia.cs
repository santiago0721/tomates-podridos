namespace tomates_podridos.Models
{
    public class ComentarioAudiencia
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int PeliculaId { get; set; }

        public Pelicula Pelicula { get; set; }

    }
}
