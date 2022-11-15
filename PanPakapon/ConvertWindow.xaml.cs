using CustomMusicCreator;
using System.Windows;

namespace PanPakapon
{
    /// <summary>
    /// Interaction logic for ConvertWindow.xaml
    /// </summary>
    public partial class ConvertWindow : Window
    {
        private bool _converting;
        private readonly TextBoxLogger _logger;
        private readonly PataMusicCreator _pataMusicCreator;
        public ConvertWindow()
        {
            InitializeComponent();
            _logger = new TextBoxLogger(LogBox);
            _pataMusicCreator = new PataMusicCreator(_logger);
        }
        internal void Convert(PataMusicModel model)
        {
            if (_converting) return;
            SetConvertingStatus(true);
            _logger.Clear();
            try
            {
                _pataMusicCreator.Convert(model);
            }
            catch
            {
                MessageBox.Show("Error while converting. Check the log to see detail.",
                    "Converting error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                SetConvertingStatus(false);
            }

            return;
        }
        private void SetConvertingStatus(bool converting)
        {
            _converting = converting;
            CloseButton.IsEnabled = !converting;
        }
        private void CloseWindow(object sender, RoutedEventArgs e) => Close();
    }
}
