namespace CustomMusicCreator
{
    internal class AtracConverter
    {
        private readonly ILogger _logger;
        private readonly TimeSpan _range;
        internal AtracConverter(ILogger logger)
        {
            _logger = logger;
            _range = new TimeSpan(TimeSpan.TicksPerMillisecond / 2);
        }
        internal void Convert(string[] filePath, string prefix)
        {
            for(int i=0;i<filePath.Length;i++)
            {
                ConvertEach(filePath[i], $"ptpat_battle_{prefix}_{i+1:00}.wav");
            }
        }
        internal void ConvertEach(string filePath, string newName)
        {
            try
            {
                var fullPath = Path.GetFullPath(filePath);
                var fileName = Path.GetFileName(filePath);

                using var stream = File.OpenRead(filePath);
                //execute here
                //File.Move(Path.GetFileName(filePath), newName);
                if (!File.Exists(
                    Path.Combine(Path.GetDirectoryName(filePath) ?? Directory.GetDirectoryRoot(filePath), newName)))
                {
                    _logger.LogError("Error from [PSP_AT3TOOL]");
                    throw new FileNotFoundException($"Failed to create file from {fileName}. " +
                        $"The converting process didn't generate the file.");
                }
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }
    }
}