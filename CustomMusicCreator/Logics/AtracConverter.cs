using CustomMusicCreator.Utils;

namespace CustomMusicCreator
{
    internal class AtracConverter
    {
        private readonly ProcessExecuter _executer;
        internal AtracConverter(ILogger logger)
        {
            _executer = new ProcessExecuter("psp_at3tool.exe", logger);
            var dllPath = Path.Combine(FilePathUtils.ResourcePath, "msvcr71.dll");
            if (!File.Exists(dllPath))
            {
                throw new FileNotFoundException($"Error: Couldn't find msvcr71.dll in {FilePathUtils.ResourcePath} directory.");
            }
            FileValidatorUtil.ValidateFile(dllPath);
        }
        internal string[] Convert(string[] filePath, string prefix)
        {
            var results = new string[filePath.Length];
            for(int i=0;i<filePath.Length;i++)
            {
                string name = $"ptpat_battle_{prefix}_{i + 1:00}.wav";
                results[i] = ConvertEach(filePath[i], name);
            }
            return results;
        }
        internal string ConvertEach(string filePath, string newName)
        {
            var fullPath = Path.GetFullPath(filePath);
            var fileName = Path.GetFileName(filePath);
            var newFullPath = FilePathUtils.GetSiblingPath(fullPath, newName);
            using var stream = File.OpenRead(filePath);
            //execute here
            _executer.ExecuteProcess($"-e -br 48 \"{fullPath}\" \"{newFullPath}\"");
            if (!File.Exists(newFullPath))
            {
                throw new InvalidOperationException($"[PSP_AT3TOOL] Failed to create file from {fileName}. " +
                    $"Converting process failed.");
            }
            else if (new FileInfo(newFullPath).Length == 0)
            {
                throw new InvalidOperationException($"[PSP_AT3TOOL] Failed to create file from {fileName}. " +
                    $"An empty file was created as result. Did you check format?");
            }
            return newFullPath;
        }
    }
}