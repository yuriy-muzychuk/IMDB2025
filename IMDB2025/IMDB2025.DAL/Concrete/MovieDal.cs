using IMDB2025.DAL.Interfaces;
using IMDB2025.DTO;
using Microsoft.Data.SqlClient;

namespace IMDB2025.DAL.Concrete
{
    public class MovieDal : IMovieDal
    {
        public Movie Create(Movie movie)
        {
            SqlConnection connection = new SqlConnection("Data Source=Firefly;Initial Catalog=IMDB2025;Integrated Security=True;Trust Server Certificate=True");

            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Movies (Title, GenreId, ReleaseDate) OUTPUT inserted.MovieId VALUES (@title, @genreId, @date)";

            command.Parameters.AddWithValue("@title", movie.Title);
            command.Parameters.AddWithValue("@genreId", movie.Genre.GenreId);
            command.Parameters.AddWithValue("@date", movie.ReleaseDate);

            //int affectedRows = command.ExecuteNonQuery();

            movie.MovieId = (int)command.ExecuteScalar();

            connection.Close();

            return movie;
        }

        public bool Delete(int movieId)
        {
            throw new NotImplementedException();
        }

        public List<Movie> GetAll()
        {
            SqlConnection connection = new SqlConnection("Data Source=Firefly;Initial Catalog=IMDB2025;Integrated Security=True;Trust Server Certificate=True");

            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT m.*, g.Name as GenreName FROM Movies m join Genres g on m.GenreId = g.GenreId";

            SqlDataReader reader = command.ExecuteReader();

            List<Movie> movies = new List<Movie>();

            while (reader.Read())
            {
                Movie movie = new Movie
                {
                    MovieId = (int)reader["MovieId"],
                    Title = (string)reader["Title"],
                    Genre = new Genre { 
                        GenreId = (int)reader["GenreId"],
                        Name = (string)reader["GenreName"]
                    },
                    ReleaseDate = (DateTime)reader["ReleaseDate"]
                };
                movies.Add(movie);
            }
            connection.Close();
            return movies;
        }

        public Movie GetById(int movieId)
        {
            throw new NotImplementedException();
        }

        public Movie Update(Movie movie)
        {
            throw new NotImplementedException();
        }
    }
}
