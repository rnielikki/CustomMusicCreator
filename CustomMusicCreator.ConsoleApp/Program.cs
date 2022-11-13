using CustomMusicCreator;
using CustomMusicCreator.ConsoleApp;

string themePath = "C:\\Users\\Lumi\\Music\\themes\\ubobon";
new PataMusicCreator(new ConsoleLogger())
    .Convert(
        Path.Combine(themePath, "base.wav"),
        Path.Combine(themePath, "lv1.wav"),
        Path.Combine(themePath, "lv2.wav"),
        Path.Combine(themePath, "lv3.wav"),
        "15DownAndOut",
        themePath
    );