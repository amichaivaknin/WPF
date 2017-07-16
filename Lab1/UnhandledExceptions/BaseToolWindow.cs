using System;
using System.Windows;

namespace UnhandledExceptions
{
    public class BaseToolWindow : Window
    {
        public BaseToolWindow()
        {
            WindowStyle = WindowStyle.ToolWindow;
            ShowInTaskbar = false;
        }

        public bool IsNotSticky { get; set; }
        public bool IsClosed { get; set; }

        protected override void OnLocationChanged(EventArgs e)
        {
            base.OnLocationChanged(e);
            IsNotSticky = true;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            IsClosed = true;
        }

        public void UpdateWindowLocation(double deltaX, double deltaY)
        {
            Left += deltaX;
            Top += deltaY;
        }
    }
}