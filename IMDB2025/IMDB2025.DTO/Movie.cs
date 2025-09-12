namespace IMDB2025.DTO
{
    public class Movie
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public Genre Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
