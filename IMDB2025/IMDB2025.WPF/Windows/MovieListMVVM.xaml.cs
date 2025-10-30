using AutoMapper;
using IMDB2025.BL.Concrete;
using IMDB2025.BL.Interfaces;
using IMDB2025.DALEF.Concrete;
using IMDB2025.DALEF.MapperProfiles;
using IMDB2025.DTO;
using IMDB2025.WPF.ViewModels;
using Microsoft.Extensions.DependencyInjection;
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
