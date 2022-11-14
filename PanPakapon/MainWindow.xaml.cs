using CustomMusicCreator;
using CustomMusicCreator.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace PanPakapon
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string[] FileName = new[] { "", "", "", "", "" };
        public string Test { get; set; } = "test";
        private string VoiceName = "";
        private string ResultPath = "";
        public MainWindow()
        {
            InitializeComponent();
            Style = (Style)FindResource(typeof(Window));
            VoiceList.ItemsSource = VoiceData.Get().Voices;
            if (VoiceList.Items.Count > 0)
            {
                VoiceList.SelectedIndex = 0;
            }
        }
        private void GetWavFile0(object sender, RoutedEventArgs e) => GetWavFile(0, Browser0_Result);
        private void GetWavFile1(object sender, RoutedEventArgs e) => GetWavFile(1, Browser1_Result);
        private void GetWavFile2(object sender, RoutedEventArgs e) => GetWavFile(2, Browser2_Result);
        private void GetWavFile3(object sender, RoutedEventArgs e) => GetWavFile(3, Browser3_Result);
        private void GetResultPath(object sender, RoutedEventArgs e) => GetResultPath(BrowserR_Result);

        private void GetResultPath(TextBox viewer)
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
                viewer.Text = fileName;
            }
        }

        private void GetWavFile(int index, TextBox viewer)
        {
            if (FileName.Length <= index || index < 0) return;
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.DefaultExt = ".wav";
            dialog.Filter = "WAV files (.wav)|*.wav";
            dialog.CheckFileExists = true;
            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                string fileName = dialog.FileName;
                viewer.Text = fileName;
                FileName[index] = fileName;
            }
        }
    }
}
