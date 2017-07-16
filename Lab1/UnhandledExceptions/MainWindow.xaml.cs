using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace UnhandledExceptions
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<BaseToolWindow> _toolWindows;
        private double _x;
        private double _y;

        public MainWindow()
        {
            InitializeComponent();
            Dispatcher.UnhandledException += DispatcherOnUnhandledException;
            Loaded += (sender, args) =>
            {
                _toolWindows = new List<BaseToolWindow>
                {
                    new Tools {Owner = this},
                    new PalettesPicker {Owner = this}
                };

                foreach (var toolWindow in _toolWindows)
                    toolWindow.Show();
            };
        }

        protected override void OnLocationChanged(EventArgs e)
        {
            base.OnLocationChanged(e);
            var x = Left - _x;
            var y = Top - _y;
            foreach (var toolWindow in _toolWindows
                .Where(toolWindow => !toolWindow.IsNotSticky && !toolWindow.IsClosed))
            {
                toolWindow.UpdateWindowLocation(x, y);
                toolWindow.IsNotSticky = false;
            }
            UpdatePrevLocation();
        }

        private void ReOpenButton_OnClick(object sender, RoutedEventArgs e)
        {
            for (var i = 0; i < _toolWindows.Count; i++)
            {
                if (!_toolWindows[i].IsClosed) continue;

                var tool = _toolWindows[i];
                _toolWindows[i] = (BaseToolWindow)Activator.CreateInstance(tool.GetType());
                _toolWindows[i].Owner = this;
                _toolWindows[i].Show();
                _toolWindows[i].Left = tool.Left;
                _toolWindows[i].Top = tool.Top;
                _toolWindows[i].IsNotSticky = tool.IsNotSticky;
            }
        }

        private void ExceptionButton_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void DispatcherOnUnhandledException(object sender,
            DispatcherUnhandledExceptionEventArgs dispatcherUnhandledExceptionEventArgs)
        {
            Debug.WriteLine(Content);
            MessageBox.Show(dispatcherUnhandledExceptionEventArgs.Exception.Message);
            dispatcherUnhandledExceptionEventArgs.Handled = true;
        }

        private void UpdatePrevLocation()
        {
            _x = Left;
            _y = Top;
        }
    }
}