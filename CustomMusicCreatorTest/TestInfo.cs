using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMusicCreatorTest
{
    internal static class TestInfo
    {
        internal const string FilePath = "testfiles";
        internal static string GetFilePath(string dir, string file)
        {
            return Path.Combine(FilePath, dir, file);
        }
    }
}
