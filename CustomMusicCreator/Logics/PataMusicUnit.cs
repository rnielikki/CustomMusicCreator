using System.Collections;
using System.Text;

namespace CustomMusicCreator.Logics
{
    /// <summary>
    /// Contains a unit of Music phase, like "base", "level1" etc.
    /// </summary>
    internal class PataMusicUnit
    {
        //----------------------------- with SetExecuters()
        /// <summary>
        /// Is this initalized with <see cref="SetExecuters"/>.
        /// </summary>
        private readonly ILogger _logger;
        private readonly MusicSplitter _splitter;
        private readonly AtracConverter _atracConverter;
        private readonly DirectoryInfo _tempDirectoryInfo;

        private string _fileName = "";
        private string _filePath = "";
        private string _prefix = "";
        private TimeSpan _timeSpan = TimeSpan.Zero;
        private string[] _splitted = Array.Empty<string>();

        internal PataMusicUnit(ILogger logger, MusicSplitter splitter, AtracConverter atracConverter, DirectoryInfo tempDirectoryInfo)
        {
            _logger = logger;
            _splitter = splitter;
            _atracConverter = atracConverter;
            _tempDirectoryInfo = tempDirectoryInfo;
        }
        internal PataMusicUnit SetInfo(string filePath, string prefix, TimeSpan timeSpan)
        {
            _filePath = filePath;
            _fileName = Path.GetFileName(_filePath);
            _prefix = prefix;
            _timeSpan = timeSpan;
            return this;
        }
        internal IEnumerator Convert()
        {
            Split();
            yield return null;
            ConvertToAtrac();
        }
        //IK not good architecture, maybe fix later
        internal void Split()
        {
            AssureNotNull();
            _logger.LogMessage($"[ SPLITTER ] Started to Split --- {_fileName}");
            _splitted = _splitter.ValidateAndLoadPaths(_tempDirectoryInfo, _filePath, _prefix, _timeSpan);
            _logger.LogMessage($"{_fileName} successfully splitted.");
        }
        internal string[] ConvertToAtrac()
        {
            AssureNotNull();
            if (_splitted.Length == 0) Split();
            if (_splitted.Length ==0) throw new InvalidOperationException("Splitted value shouldn't be null. Also this message shouldn't be shown.");
            _logger.LogMessage($"[ ATRAC CONVERTER ] Started to convert to Atrac--- {_fileName}");
            var atracConverted = _atracConverter.Convert(_splitted, _prefix);
            _logger.LogMessage($"{_fileName} successfully converted to atrac format.");
            return atracConverted;
        }
        private void AssureNotNull()
        {
            if (string.IsNullOrEmpty(_filePath) || string.IsNullOrEmpty(_prefix) || _timeSpan == TimeSpan.Zero)
            {
                throw new InvalidOperationException("Call SetInfo() first before calling this method.");
            }
        }
    }
}
