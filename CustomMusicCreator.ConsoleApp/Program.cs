using CustomMusicCreator;
using CustomMusicCreator.ConsoleApp;

string bgm = "C:\\Users\\Lumi\\Documents\\PATAPATAPATAPON\\---apps\\research_test\\bgm.dat";
using var test = new TestClass(new ConsoleLogger());
test.LoadFromPath(bgm);