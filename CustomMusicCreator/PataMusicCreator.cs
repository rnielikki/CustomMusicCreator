using CustomMusicCreator.Logics;
using CustomMusicCreator.Utils;

namespace CustomMusicCreator
{
    /// <summary>
    /// Entry class of the app. This must be public.
    /// </summary>
    public class PataMusicCreator
    {
        private ILogger _logger;
        private MusicSplitter _splitter;
        private AtracConverter _atracConverter;
        private SgdConverter _sgdConverter;
        public PataMusicCreator(ILogger logger)
        {
            _logger = logger;
            _splitter = new MusicSplitter();
            _atracConverter = new AtracConverter(logger);
            _sgdConverter = new SgdConverter(logger);
        }
        /// <summary>
        /// Start converting music.
        /// </summary>
        /// <param name="baseMusicPath0">Intro music. played only once.</param>
        /// <param name="baseMusicPath1">Base music, before command input.</param>
        /// <param name="level1MusicPath">No fever command music phase 1 (fever worm doesn't bounce)</param>
        /// <param name="level2MusicPath">No fever command music phase 2 (fever worm bounces)</param>
        /// <param name="level3MusicPath">Fever command music</param>
        /// <note>For level 2 and level 3 music, the first 4 seconds part is intro, which means, the loop part starts from 00:04.</note>
        public void Convert(PataMusicModel model)
        {
            try
            {
                if (!Directory.Exists(model.DestinationDirectory))
                {
                    throw new InvalidDataException($"Directory [{model.DestinationDirectory}] is invalid.");
                }
                string tempPath = Path.Combine(model.DestinationDirectory, FilePathUtils.TempPath);
                if (Directory.Exists(tempPath))
                {
                    Directory.Delete(tempPath, true);
                }
                var tempDirectory = Directory.CreateDirectory(tempPath);

                var musicUnits = new PataMusicUnit[]
                {
                    new PataMusicUnit(_logger, _splitter, _atracConverter, tempDirectory)
                        .SetInfo(model.BaseMusicPath, "base", new TimeSpan(0, 0, 8)),

                    new PataMusicUnit(_logger, _splitter, _atracConverter, tempDirectory)
                        .SetInfo(model.Level1MusicPath, "level1", new TimeSpan(0, 0, 16)),

                    new PataMusicUnit(_logger, _splitter, _atracConverter, tempDirectory)
                    .SetInfo(model.Level2MusicPath, "level2", new TimeSpan(0, 0, 20)),
                    new PataMusicUnit(_logger, _splitter, _atracConverter, tempDirectory)
                        .SetInfo(model.Level3MusicPath, "level3", new TimeSpan(0, 1, 8))
                };
                foreach (var musicUnit in musicUnits)
                {
                    musicUnit.Split();
                }
                List<string> atracList = new List<string>();
                foreach (var musicUnit in musicUnits)
                {
                    atracList.AddRange(musicUnit.ConvertToAtrac());
                }
                _logger.LogMessage($"[ SGD CONVERTER ] Started to convert to Sgd");
                var sgdConverted = _sgdConverter.ConvertAll(atracList.ToArray());
                _logger.LogMessage($"Files are converted to SGD successfully.");

                _logger.LogMessage("[ BND RePacker ] Start repacking progress... (logging may not supported)");
                using var repacker = new BgmRepacker();
                repacker.ReplaceFiles(sgdConverted);
                repacker.ReplaceFile(new VoiceRetriever().LoadSgd(model.VoiceTheme));
                repacker.Pack(model.DestinationPath);
                _logger.LogMessage("Packing is successfully done.");
                _logger.LogMessage("Cleaning the build...");
                Directory.Delete(tempPath, true);
                _logger.LogMessage($"The music is available on {model.DestinationPath} - Enjoy!");
            }
            catch(Exception e)
            {
                //Don't remove temp file here. Temp files can help for diagnosing.
                _logger.LogError(e.Message);
                throw;
            }
        }
    }
}
