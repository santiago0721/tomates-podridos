namespace tomates_podridos.Models
{
    public class Pelicula
    {
        public int Id { get; set; }

        public string nombre { get; set; }

        public string img { get; set; } 

        public string calCritica { get; set; }

        public string calAudiencia { get; set; }

        public string plataformas { get; set; }

        public string synopsis { get; set; }

        public string clasificacion { get; set;}

        public string equipoDir { get; set; }

        public string fecha { get; set; }

        public string duracion { get; set;}

        public string actores { get; set;}

        public int ComentarioAudienciaId { get; set; }

        public ComentarioAudiencia ComentarioAudiencia { get; set; }

        public int ComentarioCriticaId { get; set;}
    
        public ComentarioCritica ComentarioCritica { get; set;} 
    }
}
