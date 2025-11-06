using IMDB2025.DTO;
using IMDB2025.WPF.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace IMDB2025.WPF.Windows
{
    /// <summary>
    /// Interaction logic for MovieListMVVM.xaml
    /// </summary>
    public partial class MovieListMVVM : Window
    {
        private readonly MovieListViewModel _viewModel;

        public MovieListMVVM(MovieListViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var editViewModel = ActivatorUtilities.CreateInstance<MovieDetailsSimpleViewModel>(App.Services!, new Movie());
            var editWin = new MovieDetailsSimple(editViewModel);
            editWin.Owner = this;
            editWin.ShowDialog();
            _viewModel.Refresh();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedMovie == null)
                return;

            // Pass the selected Movie into the constructor chain. ActivatorUtilities will resolve other services.
            var editViewModel = ActivatorUtilities.CreateInstance<MovieDetailsSimpleViewModel>(App.Services!, _viewModel.SelectedMovie);
            var editWin = new MovieDetailsSimple(editViewModel);
            editWin.Owner = this;
            editWin.ShowDialog();
            _viewModel.Refresh();
        }
    }
}
