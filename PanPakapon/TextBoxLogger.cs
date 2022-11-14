using CustomMusicCreator;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace PanPakapon
{
    internal class TextBoxLogger:ILogger
    {
        private RichTextBox _textBox;
        internal TextBoxLogger(RichTextBox textBox)
        {
            _textBox = textBox;
        }
        public void Clear() => _textBox.Document.Blocks.Clear();

        public void LogError(string message)
        {
            Log(message, Brushes.LightPink);
        }

        public void LogMessage(string message)
        {
            Log(message, Brushes.White);
        }

        public void LogWarning(string message)
        {
            Log(message, Brushes.Yellow);
        }
        private void Log(string message, Brush brush)
        {
            var paragraph = new Paragraph(new Run(message));
            paragraph.Foreground = brush;
            _textBox.Document.Blocks.Add(paragraph);
            _textBox.Focus();
            _textBox.CaretPosition = _textBox.Document.ContentEnd;
            _textBox.ScrollToEnd();
        }
    }
}
