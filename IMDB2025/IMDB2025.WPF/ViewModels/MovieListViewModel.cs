using IMDB2025.BL.Interfaces;
using IMDB2025.DTO;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace IMDB2025.WPF.ViewModels
{
    public class MovieListViewModel : INotifyPropertyChanged
    {
        private readonly IMovieManager _movieManager;

        private ObservableCollection<Movie> _movieList;
        public ObservableCollection<Movie> Movies
        {
            get { return _movieList; }
            set
            {
                _movieList = value;
                OnPropertyChanged(nameof(Movies));
            }
        }
        public ICollectionView MoviesView { get; private set; }

        private string _filterText = string.Empty;
        public string FilterText
        {
            get => _filterText;
            set
            {
                if (_filterText == value) return;
                _filterText = value;
                OnPropertyChanged(nameof(FilterText));
                MoviesView.Refresh();
            }
        }

        private Movie? _selectedMovie;
        public Movie? SelectedMovie
        {
            get => _selectedMovie;
            set
            {
                if (_selectedMovie == value) return;
                _selectedMovie = value;
                OnPropertyChanged(nameof(SelectedMovie));
            }
        }

        public MovieListViewModel(IMovieManager movieManager)
        {
            _movieManager = movieManager ?? throw new ArgumentNullException(nameof(movieManager));

            Refresh();
        }

        private bool FilterPredicate(object? obj)
        {
            if (string.IsNullOrWhiteSpace(FilterText))
                return true;

            if (obj is Movie m)
            {
                var q = FilterText.Trim();
                return (m.Title?.IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0)
                       || (m.Genre?.Name?.IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            return false;
        }

        public void Refresh()
        {
            Movies = new ObservableCollection<Movie>(_movieManager.GetAllMovies());
            MoviesView = CollectionViewSource.GetDefaultView(Movies);
            MoviesView.Filter = FilterPredicate;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}
