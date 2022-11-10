using System.Diagnostics;

namespace CustomMusicCreator
{
    internal class ProcessExecuter
    {
        public const string ResourcePath = "resources";
        private string _exePath;
        private ILogger _logger;

        internal ProcessExecuter(string exeName, ILogger logger)
        {
            _exePath = Path.Combine(ResourcePath, exeName);
            if (!File.Exists(_exePath))
            {
                throw new FileNotFoundException($"The executable [{exeName}] doesn't exist in {ResourcePath} directory.");
            }
            _logger = logger;
        }

        internal void ExecuteProcess(string parameters)
        {
            var process = new Process();
            process.StartInfo.FileName = _exePath;
            process.StartInfo.Arguments = parameters;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.OutputDataReceived += LogOutputIfNotNull;
            process.ErrorDataReceived += LogErrorIfNotNull;
            if (!process.Start())
            {
                throw new FileLoadException($"Error: [{Path.GetFileName(_exePath)}] found, but failed to execute.");
            }
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();
            process.Close();
        }
        private void LogOutputIfNotNull(object sender, DataReceivedEventArgs args)
        {
            string? data = args.Data;
            if (!string.IsNullOrEmpty(data)) _logger.LogMessage(data);
        }
        private void LogErrorIfNotNull(object sender, DataReceivedEventArgs args)
        {
            string? data = args.Data;
            if (!string.IsNullOrEmpty(data)) _logger.LogError(data);
        }
    }
}
