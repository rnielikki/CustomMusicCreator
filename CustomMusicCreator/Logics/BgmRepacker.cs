using CustomMusicCreator.Utils;
using System.Runtime.InteropServices;

namespace CustomMusicCreator
{
    internal class BgmRepacker :IDisposable
    {
        [DllImport("libbndwrapper.dll", CallingConvention =CallingConvention.Cdecl)]
        private static extern IntPtr BND_Create();
        [DllImport("libbndwrapper.dll", CallingConvention =CallingConvention.Cdecl)]
        private static extern void BND_Delete(IntPtr bnd);
        [DllImport("libbndwrapper.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool Load(IntPtr bnd, string file, bool log);
        [DllImport("libbndwrapper.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int Count_Files(IntPtr bnd);
        [DllImport("libbndwrapper.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr Get_Full_Name(IntPtr bnd, int id);
        [DllImport("libbndwrapper.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void Replace_File(IntPtr bnd, int id, string path);
        [DllImport("libbndwrapper.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void Save(IntPtr bnd, string path);

        private readonly IntPtr _bnd;
        private readonly string[] _bndContents;

        private const string _bgmRelativepath = "files/BGM.DAT";
        internal BgmRepacker()
        {
            string path = Path.Combine(FilePathUtils.ResourcePath, _bgmRelativepath);
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"BGM.DAT not found from ${path}");
            }
            _bnd =  BND_Create();
            Load(_bnd, path, false);

            int count = Count_Files(_bnd);
            _bndContents = new string[count];
            for (int i = 0; i <count ;i++)
            {
                IntPtr ptr = Get_Full_Name(_bnd, i);
                _bndContents[i] =  Marshal.PtrToStringAnsi(ptr);
            }
        }
        internal void ReplaceFiles(string[] filePaths)
        {
            foreach (var filePath in filePaths)
            {
                ReplaceFile(filePath);
            }
        }
        internal void ReplaceFile(string filePath)
        {
            if (!File.Exists(filePath)) throw new ArgumentException($"The file {filePath} doesn't exist.");
            var name = Path.GetFileName(filePath);
            int id = Array.IndexOf(_bndContents, name);
            if (id < 0) throw new ArgumentException($"The bnd file doens't contain {name}." +
                $"Remember to match file name (without extension) same as the bnd name.");
            Replace_File(_bnd, id, filePath);
        }
        internal void Pack(string outputPath)
        {
            try
            {
                Save(_bnd, Path.Combine(outputPath, "BGM.DAT"));
            }
            catch{
                throw new ExternalException("Failed to pack the file. But if you give another shot, it may success.");
            }
        }

        public void Dispose()
        {
            BND_Delete(_bnd);
        }
    }
}
