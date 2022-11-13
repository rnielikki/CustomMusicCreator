namespace CustomMusicCreator.Utils
{
    public static class FilePathUtils
    {
        public const string TempPath = ".patamusic_temp";
        internal static string GetParentDirectory(string file) =>
            Path.GetDirectoryName(file)
                ?? throw new ArgumentException($"The file path {file} is invalid.");
        internal static string GetSiblingPath(string file, string siblingFileName) =>
            Path.Combine(GetParentDirectory(file), siblingFileName);
    }
}
