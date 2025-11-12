using IMDB2025.WPF.Interfaces;
using System.Windows.Input;

namespace IMDB2025.WPF.Commands
{
    internal class CloseCommand : ICommand
    {
        private ICloseable _vm;
        public CloseCommand(ICloseable vm)
        {
            _vm = vm;
        }

        #region ICommand
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _vm.Close?.Invoke();
        }
        #endregion
    }
}
