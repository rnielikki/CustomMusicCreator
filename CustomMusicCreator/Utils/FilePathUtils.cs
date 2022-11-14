namespace CustomMusicCreator.Utils
{
    public static class FilePathUtils
    {
        public const string TempPath = ".patamusic_temp";
        public const string ResourcePath = "resources";
        internal static string GetParentDirectory(string file) =>
            Path.GetDirectoryName(file)
                ?? throw new ArgumentException($"The file path {file} is invalid.");
        internal static string GetSiblingPath(string file, string siblingFileName) =>
            Path.Combine(GetParentDirectory(file), siblingFileName);
        //for duplicated file.
        public static string GetSaveFileName(string path)
        {
            if (File.Exists(path)) return path;
            var parentDirectory = GetParentDirectory(path);
            var fileName = Path.GetFileNameWithoutExtension(path);
            var fileExtension = Path.GetExtension(path);
            int counter = 0;
            do
            {
                counter++;
                path = Path.Combine(parentDirectory, fileName + counter + fileExtension);
            } while (File.Exists(path));
            return path;
        }
    }
}
