using CustomMusicCreator;

namespace PanPakapon.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("test ;)");
            if (args.Length < 5)
            {
                Console.WriteLine("Usage:" +
                    $"\n{Path.GetFileName(Environment.GetCommandLineArgs()[0])}" +
                    " BASE_THEME_PATH LEVEL1_THEME_PATH LEVEL2_THEME_PATH LEVEL_3_THEME_PATH VOICE_THEME [OUTPUT_NAME]" +
                    "\nOutput name is optional.");
                PrintVoices();
            }
            else if (!VoiceData.Get().HasVoice(args[4]))
            {
                Console.WriteLine("Voice name is invalid.");
                PrintVoices();
            }
            else
            {
                string filePath;
                if (args.Length < 6)
                {
                    filePath = Path.Combine(Directory.GetCurrentDirectory(), "BGM.DAT");
                }
                else
                {
                    filePath = Path.GetFullPath(args[5]);
                }
                var creator = new PataMusicCreator(new ConsoleLogger());
                creator.Convert(new PataMusicModel(
                    args[0], args[1], args[2], args[3], args[4], filePath
                    ));
            }
            /*
            var creator = new PataMusicCreator(new ConsoleLogger());
            creator.Convert(new PataMusicModel(
                ));
            */
            /*
new CustomMusicCreator.PataMusicModel(
    BaseTheme.FileName, Level1Theme.FileName, Level2Theme.FileName, Level3Theme.FileName,
    Voices.VoiceName, SavePathGetter.ResultPath)
);
            */
        }
        static void PrintVoices()
        {
            Console.WriteLine("Available voices:\n" + string.Join('\n', VoiceData.Get().Voices)
                +"\nRemember to put voice name with number!");
        }
    }
}


