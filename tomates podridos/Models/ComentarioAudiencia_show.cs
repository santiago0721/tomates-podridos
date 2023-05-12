namespace tomates_podridos.Models
{
    public class ComentarioAudiencia_show
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int ShowId { get; set; }

        public Show Show { get; set; }

    }
}
