using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FilRougeMW
{
    class RelayCommand : ICommand
    {
        private readonly Action actionAExecuter;
        private object v;
        public RelayCommand(Action action)
        {
            actionAExecuter = action;
        }
        public RelayCommand(object v)
        {
            this.v = v;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public void Execute(object parameter)
        {
            actionAExecuter();
        }


    }
}
