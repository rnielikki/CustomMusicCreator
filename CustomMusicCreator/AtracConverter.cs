namespace CustomMusicCreator
{
    internal class AtracConverter
    {
        private ProcessExecuter _executer;
        internal AtracConverter(ILogger logger)
        {
            _executer = new ProcessExecuter("psp_at3tool.exe", logger);
            if (!File.Exists(Path.Combine(ProcessExecuter.ResourcePath, "msvcr71.dll")))
            {
                throw new FileNotFoundException($"Error: Couldn't find msvcr71.dll in {ProcessExecuter.ResourcePath} directory.");
            }
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
            var fullPath = Path.GetFullPath(filePath);
            var fileName = Path.GetFileName(filePath);
            var newFullPath = Path.Combine(Path.GetDirectoryName(fullPath) ?? Directory.GetDirectoryRoot(fullPath), newName);

            using var stream = File.OpenRead(filePath);
            //execute here
            _executer.ExecuteProcess($"-e -br 48 {fullPath} {newFullPath}");
            if (!File.Exists(newFullPath))
            {
                throw new InvalidOperationException($"[PSP_AT3TOOL] Failed to create file from {fileName}. " +
                    $"The converting process didn't generate the file.");
            }
            else if (new FileInfo(newFullPath).Length == 0)
            {
                throw new InvalidOperationException($"[PSP_AT3TOOL] Failed to create file from {fileName}. " +
                    $"The converting process generated empty file, possibly having format issue.");
            }
        }
    }
}