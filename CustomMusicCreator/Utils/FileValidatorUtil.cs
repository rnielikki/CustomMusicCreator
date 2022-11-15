using System.Security.Cryptography;

namespace CustomMusicCreator.Utils
{
    /// <summary>
    /// Validates hash of the pre-defined file.
    /// </summary>
    internal static class FileValidatorUtil
    {
        private static readonly Dictionary<string, byte[]> _predefinedHash =
            new Dictionary<string, byte[]>()
            {
                {"psp_at3tool.exe",
                    Convert.FromHexString("823199113c59ffcd3fc3e1855dbb88a9cd44d29e82e7a7cfc42e8c92f2a61341") },
                {"atrac2sgd.exe",
                    Convert.FromHexString("c27f76e056d991aabbf75a38103118d2583c40b78363f40a5ff4f82f84c57706") },
                {"msvcr71.dll",
                    Convert.FromHexString("8094af5ee310714caebccaeee7769ffb08048503ba478b879edfef5f1a24fefe") }
            };
        internal static void ValidateFile(string filePath)
        {
            string fileName = Path.GetFileName(filePath);
            if (!_predefinedHash.TryGetValue(fileName, out byte[]? hash))
            {
                throw new InvalidOperationException($"Opening {filePath} is not allowed.");
            }
            using SHA256 sha256 = SHA256.Create();
            using var fileStream = File.OpenRead(filePath);

            if (!sha256.ComputeHash(fileStream).SequenceEqual(hash))
            {
                throw new InvalidDataException($"The file in {filePath} is corrupted. Download the resource again from official download path.");
            }
        }
    }
}
