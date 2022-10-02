using MminoWindows.Basic;
using MminoWindows.WindowModel;
using MminoWindows.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MminoWindows.Triggers
{
    internal class UpdateViewCommand : ICommand
    {
        private readonly MainViewModel viewModel;

        public UpdateViewCommand(MainViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (parameter.ToString() == "begin")
            {
                viewModel.SelectedViewModel = new ViewModel.BeginViewModel();
            }
            else if (parameter.ToString() == "menu")
            {
                viewModel.SelectedViewModel = new ViewModel.MenuViewModel();
            }
            else if (parameter.ToString() == "about")
            {
                var a = new AboutWindowModel();
                a.OpenDialog();
            }
            else if (parameter.ToString() == "update")
            {
                var a = new UpdaterWindowModel();
                a.Open();
            }
        }
    }
}
