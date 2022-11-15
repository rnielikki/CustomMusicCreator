using CustomMusicCreator;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PanPakapon
{
    /// <summary>
    /// Interaction logic for FileSelector.xaml
    /// </summary>
    public partial class FileSelector : UserControl
    {
        private string _label;
        public string Label {
            get => _label;
            set
            {
                _label = value;
                TextLabel.Content = value;
            }
        }
        private int _seconds;
        private TimeSpan _time;
        public int Seconds{
            get => _seconds;
            set
            {
                _time = TimeSpan.FromSeconds(value);
                _seconds = value;
                TextTime.Content = _time.ToString(@"mm\:ss");
            }
        }
        public bool HasValidFile { get; private set; }

        public string FileName { get; private set; } = "";

        private readonly ImageSource _okImage;
        private readonly ImageSource _xImage;

        private readonly WavValidator _wavValidator;
        public FileSelector()
        {
            InitializeComponent();
            _okImage = (ImageSource)FindResource("okImage");
            _xImage = (ImageSource)FindResource("xImage");
            _wavValidator= new WavValidator();
        }
        private void GetWavFile(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.DefaultExt = ".wav";
            dialog.Filter = "WAV files (.wav)|*.wav";
            dialog.CheckFileExists = true;
            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                string fileName = dialog.FileName;
                PathViewer.Text = fileName;
                FileName = fileName;
                UpdateStatus(ValidationImage, _wavValidator.ValidateWav(fileName, _time));
            }
        }
        private void UpdateStatus(Image image, WavValidationCode code)
        {
            HasValidFile = code == WavValidationCode.Valid;
            image.Source = HasValidFile ? _okImage : _xImage;
        }
    }
}
