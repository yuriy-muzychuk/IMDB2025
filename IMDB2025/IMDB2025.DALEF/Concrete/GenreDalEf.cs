using AutoMapper;
using IMDB2025.DAL.Interfaces;
using IMDB2025.DTO;

namespace IMDB2025.DALEF.Concrete
{
    public class GenreDalEf: IGenreDal
    {
        private readonly string _connStr;
        private readonly IMapper _mapper;

        public GenreDalEf(string connStr, IMapper mapper)
        {
            _connStr = connStr;
            _mapper = mapper;
        }
        public List<Genre> GetAll()
        {
            using(var context = new ImdbContext(_connStr))
            {
                return _mapper.Map<List<Genre>>(context.Genres.ToList());
            }
        }
    }
}
