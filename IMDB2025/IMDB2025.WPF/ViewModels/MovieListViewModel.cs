using IMDB2025.BL.Interfaces;
using IMDB2025.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace IMDB2025.WPF.ViewModels
{
    public class MovieListViewModel : INotifyPropertyChanged
    {
        private readonly IMovieManager _movieManager;

        public ObservableCollection<Movie> Movies { get; }
        public ICollectionView MoviesView { get; }

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

            Movies = new ObservableCollection<Movie>(_movieManager.GetAllMovies());
            MoviesView = CollectionViewSource.GetDefaultView(Movies);
            MoviesView.Filter = FilterPredicate;
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

        // Public methods (no ICommand) to be called by the View's code-behind
        public void AddMovie()
        {
            var nextId = Movies.Any() ? Movies.Max(m => m.MovieId) + 1 : 1;
            var newMovie = new Movie
            {
                MovieId = nextId,
                Title = $"New Movie {nextId}",
                Genre = new Genre { Name = "Unknown" },
                ReleaseDate = null
            };
            Movies.Add(newMovie);
            SelectedMovie = newMovie;
        }

        public void EditMovie()
        {
            if (SelectedMovie != null)
            {
                // Simple demo behavior; consider moving UI dialogs out of VM in stricter MVVM
                MessageBox.Show($"Edit movie: {SelectedMovie.Title} (ID: {SelectedMovie.MovieId})", "Edit", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Please select a movie to edit.", "Edit", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}
