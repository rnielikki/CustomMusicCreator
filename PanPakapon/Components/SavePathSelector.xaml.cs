using System.Security.Policy;
using System.Windows;
using System.Windows.Controls;

namespace PanPakapon
{
    /// <summary>
    /// Interaction logic for SavePathSelector.xaml
    /// </summary>
    public partial class SavePathSelector : UserControl
    {
        public string ResultPath { get; private set; } = "";
        public bool HasValidPath => !string.IsNullOrEmpty(ResultPath);
        public SavePathSelector()
        {
            InitializeComponent();
        }

        private void GetResultPath(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.DefaultExt = ".DAT";
            dialog.Filter = "DAT files|*.DAT";
            dialog.CheckFileExists = false;
            dialog.CheckPathExists = true;
            dialog.ValidateNames = true;
            var result = dialog.ShowDialog();
            if (result == true)
            {
                string fileName = dialog.FileName;
                ResultPath = fileName;
                PathViewer.Text = fileName;
            }
        }
    }
}
