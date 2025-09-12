using IMDB2025.DTO;

namespace IMDB2025.DAL.Interfaces
{
    public interface IMovieDal
    {
        Movie Create(Movie movie);
        List<Movie> GetAll();
        Movie GetById(int movieId);
        Movie Update(Movie movie);
        bool Delete(int movieId);
    }
}
