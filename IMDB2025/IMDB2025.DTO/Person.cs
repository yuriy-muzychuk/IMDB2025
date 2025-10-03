namespace IMDB2025.DTO
{
    public class Person
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; } = null!;
        public string? LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public bool IsFemale { get; set; }
        public ICollection<Actor> Roles { get; set; } = new List<Actor>();
        public ICollection<Movie> MoviesDirected { get; set; } = new List<Movie>();
    }
}
