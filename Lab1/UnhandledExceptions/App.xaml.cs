using System.Threading;
using System.Windows;

namespace UnhandledExceptions
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Thread.Sleep(3000);
        }
    }
}