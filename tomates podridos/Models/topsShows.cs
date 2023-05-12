namespace tomates_podridos.Models
{
    public class topsShows
    {
        public int Id { get; set; } 
        public int ShowId { get; set; }

        public Show Show { get; set; }
    }
}
