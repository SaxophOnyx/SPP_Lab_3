using System;
using System.Windows.Input;

namespace Client.ViewModelContent.Commands
{
    public class GeneralCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private Action _action;

        private Func<bool>? _canExecuteFunc;


        public GeneralCommand(Action action, Func<bool> canExecuteFunc)
        {
            _action = action;
            _canExecuteFunc = canExecuteFunc;
        }

        public GeneralCommand(Action action)
        {
            _action = action;
            _canExecuteFunc = null;
        }


        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }

        public bool CanExecute(object? parameter)
        {
            return _canExecuteFunc != null ? _canExecuteFunc() : true; 
        }

        public void Execute(object? parameter)
        {
            _action();
        }
    }
}
