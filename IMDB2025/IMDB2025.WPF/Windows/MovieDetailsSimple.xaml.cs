using IMDB2025.WPF.ViewModels;
using System.Windows;

namespace IMDB2025.WPF.Windows
{
    /// <summary>
    /// Interaction logic for MovieDetailsSimple.xaml
    /// </summary>
    public partial class MovieDetailsSimple : Window
    {
        private readonly MovieDetailsSimpleViewModel _viewModel;

        public MovieDetailsSimple(MovieDetailsSimpleViewModel vm)
        {
            _viewModel = vm;
            DataContext = vm;

            InitializeComponent();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Save();
            DialogResult = true;
            Close();
        }
    }
}
