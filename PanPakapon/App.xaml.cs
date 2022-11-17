using System.Windows;

namespace PanPakapon
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void ShowError(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message,
                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
