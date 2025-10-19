using AutoMapper;
using IMDB2025.BL.Concrete;
using IMDB2025.BL.Interfaces;
using IMDB2025.DALEF.Concrete;
using IMDB2025.DALEF.MapperProfiles;
using IMDB2025.DTO;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace IMDB2025.WPF.Windows
{
    /// <summary>
    /// Interaction logic for MovieList.xaml
    /// </summary>
    public partial class MovieListDI : Window
    {
        public ObservableCollection<Movie> Movies { get; } = new ObservableCollection<Movie>();
        private ICollectionView _moviesView;

        public MovieListDI(IMovieManager movieManager)
        {
            InitializeComponent();
            DataContext = this;
            
            Movies = new ObservableCollection<Movie>(movieManager.GetAllMovies());

            _moviesView = CollectionViewSource.GetDefaultView(Movies);
            _moviesView.Filter = FilterPredicate;
        }

        private bool FilterPredicate(object obj)
        {
            if (string.IsNullOrWhiteSpace(FilterTextBox.Text))
                return true;

            if (obj is Movie m)
            {
                var q = FilterTextBox.Text.Trim();
                return (m.Title?.IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0)
                       || (m.Genre?.Name?.IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            return false;
        }

        private void FilterTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            _moviesView?.Refresh();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // Minimal add behavior: create and select a new movie entry.
            var nextId = Movies.Any() ? Movies.Max(m => m.MovieId) + 1 : 1;
            var newMovie = new Movie
            {
                MovieId = nextId,
                Title = $"New Movie {nextId}",
                Genre = new Genre { Name = "Unknown" },
                ReleaseDate = null
            };
            Movies.Add(newMovie);
            MoviesDataGrid.SelectedItem = newMovie;
            MoviesDataGrid.ScrollIntoView(newMovie);

            // In a real app replace with an add/edit dialog and persist changes.
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (MoviesDataGrid.SelectedItem is Movie sel)
            {
                MessageBox.Show($"Edit movie: {sel.Title} (ID: {sel.MovieId})", "Edit", MessageBoxButton.OK, MessageBoxImage.Information);
                // Replace this with your edit dialog/flow.
            }
            else
            {
                MessageBox.Show("Please select a movie to edit.", "Edit", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
