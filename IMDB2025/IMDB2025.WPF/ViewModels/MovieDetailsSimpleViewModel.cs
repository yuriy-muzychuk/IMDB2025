using IMDB2025.BL.Interfaces;
using IMDB2025.DTO;
using System.ComponentModel;

namespace IMDB2025.WPF.ViewModels
{
    public class MovieDetailsSimpleViewModel : INotifyPropertyChanged
    {
        private IMovieManager _manager;
        private Movie _movie;

        public MovieDetailsSimpleViewModel(IMovieManager manager, Movie? movie = null)
        {
            _manager = manager;
            Movie = movie ?? new Movie();
            Genres = _manager.GetAllGenres();
        }

        public Movie Movie
        {
            get { return _movie; }
            set
            {
                _movie = value;
                OnPropertyChanged(nameof(Movie));
            }
        }

        public List<Genre> Genres { get; set; }
        public void Save()
        {
            Movie = _manager.UpdateMovie(Movie);
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
        #endregion
    }
}
