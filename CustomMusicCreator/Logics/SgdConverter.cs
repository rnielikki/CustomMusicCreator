using System.Text;
using CustomMusicCreator.Utils;

namespace CustomMusicCreator
{
    /// <summary>
    /// Wrapper of Sgd Converter. This uses Atrac2Sgd.exe.
    /// </summary>
    internal class SgdConverter
    {
        private string _samplePath;
        private const string _sampleRelativePath = "files/sample.sgd";
        private ProcessExecuter _executer;
        private const long _validLength = 24928;
        internal SgdConverter(ILogger logger)
        {
            _samplePath = Path.Combine(FilePathUtils.ResourcePath, _sampleRelativePath);
            _executer = new ProcessExecuter("atrac2sgd.exe", logger);
        }
        internal string[] ConvertAll(string[] filePaths)
        {
            StringBuilder parameters = new StringBuilder();
            string[] results = new string[filePaths.Length];

            for(int i=0;i<filePaths.Length;i++)
            {
                string filePath = filePaths[i];
                string sgdFileName = Path.GetFileNameWithoutExtension(filePath)+".sgd";
                string resultPath = FilePathUtils.GetSiblingPath(filePath, sgdFileName);
                File.Copy(_samplePath, resultPath, true);
                parameters.Append($"\"{filePath}\" \"{resultPath}\" ");

                results[i] = resultPath;
            }
            _executer.ExecuteProcess(parameters.ToString());
            foreach (var filePath in results)
            {
                //file didn't change, or even if changed, the length is invalid.
                if (new FileInfo(filePath).Length != _validLength)
                {
                    throw new InvalidOperationException(
                        $"Failed to update sgd file. The cause can be file size mismatch, or an error occured while converting the file.");
                }
            }
            return results;
        }
    }
}
