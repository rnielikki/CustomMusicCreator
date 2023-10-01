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
        private double _defaultHeight;
        private Brush _warningColor;
        private Brush _errorColor;
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
        private readonly ImageSource _convertImage;
        private readonly ImageSource _xImage;

        private readonly WavValidator _wavValidator;
        public FileSelector()
        {
            InitializeComponent();
            _okImage = (ImageSource)FindResource("okImage");
            _convertImage = (ImageSource)FindResource("convertImage");
            _xImage = (ImageSource)FindResource("xImage");
            _wavValidator= new WavValidator();
            _defaultHeight = ValidationTextParent.Height;

            _warningColor = new SolidColorBrush(Color.FromRgb(220, 220, 100));
            _errorColor = new SolidColorBrush(Color.FromRgb(255, 150, 150));
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
            var isValid = code == WavValidationCode.Valid;
            var isConvertable = WavFormatConverter.IsConvertable(code);
            HasValidFile = isValid | isConvertable;
            image.Source = isValid ? _okImage : isConvertable?_convertImage:_xImage;
            switch (code)
            {
                case WavValidationCode.Valid:
                    ValidationTextParent.Height = 0;
                    ValidationText.Text = "";
                    break;
                case WavValidationCode.FormatError:
                    ValidationText.Foreground = _errorColor;
                    ValidationTextParent.Height = _defaultHeight;
                    ValidationText.Text = "Error: Invalid WAV file.";
                    break;
                case WavValidationCode.LengthError:
                    ValidationText.Foreground = _errorColor;
                    ValidationTextParent.Height = _defaultHeight;
                    ValidationText.Text = "Error: Length difference is more than 1ms. Check on the external app (e.g. Audacity) for detail.";
                    break;
                case WavValidationCode.SampleRateError:
                    ValidationText.Foreground = _warningColor; 
                    ValidationTextParent.Height = _defaultHeight;
                    ValidationText.Text = "Warning: Sample rate is not 44100Hz. The file will be converted automatically.";
                    break;
                case WavValidationCode.EncodingError:
                    ValidationText.Foreground = _warningColor;
                    ValidationTextParent.Height = _defaultHeight;
                    ValidationText.Text = "Warning: File is not 16bit single PCM. The file will be converted automatically.";
                    break;
            }
        }
    }
}
