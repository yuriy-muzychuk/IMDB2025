using System.Text;

namespace IMDB2025.DTO
{
    public class Movie
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public Genre Genre { get; set; }
        public DateTime? ReleaseDate { get; set; }

        public List<Actor> Actors { get; set; } = new List<Actor>();
        public List<Person> Directors { get; set; } = new List<Person>();
        public override string ToString()
        {
            string releaseDateStr = ReleaseDate.HasValue
                ? ReleaseDate.Value.ToShortDateString()
                : "N/A";
            StringBuilder builder = new StringBuilder();
            builder.Append($"{MovieId}: {Title} - {Genre?.Name ?? "N/A"} - {releaseDateStr}\n");
            builder.Append("Directors: ");
            if (Directors.Any())
            {
                builder.Append(string.Join(", ", Directors.Select(d => $"{d.FirstName} {d.LastName}")));
            }
            else
            {
                builder.Append("N/A");
            }
            builder.Append("\nActors: ");
            if (Actors.Any())
            {
                builder.Append(string.Join(", ", Actors.Select(a => $"{a.Person.FirstName} {a.Person.LastName} ({a.CharacterName})")));
            }
            else
            {
                builder.Append("N/A");
            }
            return builder.ToString();
        }
    }
}
