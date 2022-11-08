using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMusicCreator
{
    public interface ILogger
    {
        public void LogMessage(string message);
        public void LogWarning(string message);
        public void LogError(string message);
    }
}
