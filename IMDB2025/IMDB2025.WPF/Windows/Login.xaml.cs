using IMDB2025.WPF.Interfaces;
using IMDB2025.WPF.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace IMDB2025.WPF.Windows
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login(LoginViewModel vm)
        {
            DataContext = vm;
            InitializeComponent();
            Loaded += Login_Loaded;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ((dynamic)this.DataContext).Password = ((PasswordBox)sender).Password;
            }
        }

        private void Login_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is ICloseable cvm)
            {
                cvm.Close += () =>
                {
                    DialogResult = false;
                    Close();
                };
            }
            if (DataContext is LoginViewModel lvm)
            {
                lvm.LoginSuccessful += () =>
                {
                    DialogResult = true;
                    Close();
                };
                lvm.LoginFailed += () =>
                {
                    MessageBox.Show("Invalid credentials", "Error");
                };
            }
        }
    }
}
