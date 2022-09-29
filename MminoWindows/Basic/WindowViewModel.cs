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

        internal void InitializeWindow(Window window)
        {
            this.window = window;
            window.Activated += WindowActivated;
            window.SizeChanged += WindowSizeChanged;
            MinimizeCommand = new(Minimize);
            MaximizeCommand = new(Maximize);
        }

        private void WindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            window.WindowStyle = WindowStyle.None;
        }

        private void WindowActivated(object? sender, EventArgs e)
        {
            //window.WindowStyle = WindowStyle.None;

            window.Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, (DispatcherOperationCallback)delegate (object unused)
            {
                window.WindowStyle = WindowStyle.None;
                return null;
            }, null);
        }

        internal void Minimize()
        {
            window.WindowStyle = WindowStyle.SingleBorderWindow;
            window.WindowState = WindowState.Minimized;
        }

        internal void Maximize()
        {
            if (window.WindowState == WindowState.Maximized)
            {
                window.WindowStyle = WindowStyle.SingleBorderWindow;
                window.WindowState = WindowState.Normal;
            }
            else
            {
                window.WindowStyle = WindowStyle.SingleBorderWindow;
                window.WindowState = WindowState.Maximized;
            }
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
