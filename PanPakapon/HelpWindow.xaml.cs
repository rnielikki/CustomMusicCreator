using System.Diagnostics;
using System.Windows;

namespace PanPakapon
{
    /// <summary>
    /// Interaction logic for HelpWindow.xaml
    /// </summary>
    public partial class HelpWindow : Window
    {
        public HelpWindow()
        {
            InitializeComponent();
        }

        private void CloseWindow(object sender, RoutedEventArgs e) => Close();
         private void LinkClick(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
             Process.Start(new ProcessStartInfo(e.Uri.ToString()) { UseShellExecute = true });
        }
   }
}
