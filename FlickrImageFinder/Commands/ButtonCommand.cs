using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace FlickrImageFinder.Commands
{
    public class ButtonCommand : ICommand
    {
        private Action<object> execute;

        public event EventHandler CanExecuteChanged;

        public ButtonCommand(Action<object> execute)
        {
            this.execute = execute;
        }
        public bool CanExecute(object parameter) => true;
        public void Execute(object parameter)
        {
            this.execute(parameter);
        }
        protected void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
