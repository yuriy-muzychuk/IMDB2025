using IMDB2025.BL.Interfaces;
using IMDB2025.DAL.Interfaces;
using IMDB2025.DTO;

namespace IMDB2025.BL.Concrete
{
    public class MovieManager : IMovieManager
    {
        private readonly IMovieDal _movieDal;
        private readonly IGenreDal _genreDal;

        public MovieManager(IMovieDal movieDal, IGenreDal genreDal)
        {
            _movieDal = movieDal;
            _genreDal = genreDal;
        }

        public List<Movie> GetAllMovies()
        {
            return _movieDal.GetAll();
        }

        public Movie CreateMovie(Movie movie)
        {
            return _movieDal.Create(movie);
        }

        public Movie? GetMovieById(int movieId)
        {
            return _movieDal.GetById(movieId);
        }

        public Movie UpdateMovie(Movie movie)
        {
            return _movieDal.Update(movie);
        }

        public List<Genre> GetAllGenres()
        {
            return _genreDal.GetAll();
        }
    }
}
