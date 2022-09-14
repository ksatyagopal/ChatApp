using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChatApp.Commands
{
    public class RelayCommand : ICommand
    {
        private Action<object> action;
        private Func<object, bool> canExecuteAction;

        public RelayCommand(Action<object> action, Func<object, bool> checkFunc)
        {
            this.action = action;
            this.canExecuteAction = checkFunc;
        }
        public RelayCommand(Action<object> action)
        {
            this.action = action;
        }

        public event EventHandler? CanExecuteChanged
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

        public bool CanExecute(object? parameter)
        {
            return canExecuteAction == null ? true : canExecuteAction(parameter);
        }

        public void Execute(object? parameter)
        {
            action(parameter);
        }
    }
}
