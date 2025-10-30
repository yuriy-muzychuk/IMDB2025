using IMDB2025.BL.Interfaces;
using IMDB2025.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB2025.BL.Concrete
{
    public class MovieManager : IMovieManager
    {
        private readonly IMovieDal _movieDal;
        public MovieManager(IMovieDal movieDal)
        {
            _movieDal = movieDal;
        }

        public List<DTO.Movie> GetAllMovies()
        {
            return _movieDal.GetAll();
        }
    }
}
