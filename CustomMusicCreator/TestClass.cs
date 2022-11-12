using System.Runtime.InteropServices;

namespace CustomMusicCreator
{
    public class TestClass :IDisposable
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
        public TestClass(ILogger logger)
        {
            _logger = logger;
            _bnd =  BND_Create();
        }
        public void LoadFromPath(string path)
        {
            Load(_bnd, path, false);
            int count = Count_Files(_bnd);
            for (int i = 0; i <count ;i++)
            {
                IntPtr ptr = Get_Full_Name(_bnd, i);
                string name = Marshal.PtrToStringAnsi(ptr);
                _logger.LogMessage(name);
            }
        }

        public void Dispose()
        {
            BND_Delete(_bnd);
        }
    }
}
