namespace IMDB2025.DTO
{
    public class Actor
    {
        public int MovieId { get; set; }
        public int PersonId { get; set; }
        public string CharacterName { get; set; } = null!;
        public Person Person { get; set; } = null!;
        public Movie Movie { get; set; } = null!;
    }
}
