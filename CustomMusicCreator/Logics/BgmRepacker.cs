using System.Runtime.InteropServices;
using CustomMusicCreator.Models;

namespace CustomMusicCreator
{
    public class BgmRepacker :IDisposable
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

        private ILogger _logger;
        private IntPtr _bnd;
        private string[] _bndContents;
        public BgmRepacker(string path, ILogger logger)
        {
            _logger = logger;
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
            var name = Path.GetFileNameWithoutExtension(filePath);
            int id = Array.IndexOf(_bndContents, name);
            if (id < 0) throw new ArgumentException($"The bnd file doens't contain {name}." +
                $"Remember to match file name (without extension) same as the bnd name.");
            Replace_File(_bnd, id, filePath);
        }
        internal void Pack(string outputPath)
        {
            try
            {
                Save(_bnd, outputPath);
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
