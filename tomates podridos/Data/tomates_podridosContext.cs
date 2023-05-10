using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using tomates_podridos.Models;

namespace tomates_podridos.Data
{
    public class tomates_podridosContext : DbContext
    {
        public tomates_podridosContext (DbContextOptions<tomates_podridosContext> options)
            : base(options)
        {
        }

        public DbSet<tomates_podridos.Models.ExpertoEnCine> ExpertoEnCine { get; set; } = default!;

        public DbSet<tomates_podridos.Models.Cinefilo>? Cinefilo { get; set; }

        public DbSet<tomates_podridos.Models.ComentarioAudiencia>? ComentarioAudiencia { get; set; }

        public DbSet<tomates_podridos.Models.ComentarioCritica>? ComentarioCritica { get; set; }

        public DbSet<tomates_podridos.Models.genero>? genero { get; set; }

        public DbSet<tomates_podridos.Models.Pelicula>? Pelicula { get; set; }

        public DbSet<tomates_podridos.Models.Peliculagenero>? Peliculagenero { get; set; }
    }
}
