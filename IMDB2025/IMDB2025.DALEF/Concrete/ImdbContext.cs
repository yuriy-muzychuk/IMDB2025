using IMDB2025.DALEF.Data;
using Microsoft.EntityFrameworkCore;

namespace IMDB2025.DALEF.Concrete
{
    public class ImdbContext: ImdbContextOriginal
    {
        private readonly string _connStr;

        public ImdbContext(string connStr)
        {
            _connStr = connStr;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer(_connStr);
    }
}
