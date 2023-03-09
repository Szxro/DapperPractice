namespace Entities
{
    public class Movies
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public bool isLive { get; set; }

        public DateTime ReleaseDate { get; set; }

        public HashSet<Cinema> Cinemas { get; set; } = new();
    }
}