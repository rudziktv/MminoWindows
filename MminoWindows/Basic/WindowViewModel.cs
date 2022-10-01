using MminoWindows.Triggers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace MminoWindows.Basic
{
    internal class WindowViewModel : BaseViewModel
    {
        internal Window window;

        public Command MinimizeCommand { get; set; }
        public Command MaximizeCommand { get; set; }
        public Command CloseAppCommand { get; set; }

        internal void InitializeWindow(Window window)
        {
            this.window = window;
            MinimizeCommand = new(Minimize);
            MaximizeCommand = new(Maximize);
            CloseAppCommand = new(CloseApp);
        }

        private void WindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            window.WindowStyle = WindowStyle.None;
        }

        internal void Minimize()
        {
            window.WindowState = WindowState.Minimized;
        }

        internal void Maximize()
        {
            if (window.WindowState == WindowState.Maximized)
            {
                window.WindowState = WindowState.Normal;
            }
            else
            {
                window.WindowState = WindowState.Maximized;
            }
        }

        internal void CloseApp()
        {
            App.Current.Shutdown();
        }

        internal void Open()
        {
            window.DataContext = this;
            window.Show();
        }

        internal void OpenDialog()
        {
            window.DataContext = this;
            window.ShowDialog();
        }

        internal void Close()
        {
            window.Close();
        }
    }
}
