using System;
using System.IO;
using System.Linq;

namespace VoicesCalculatorApplication
{
	public class Program
	{
        private const string TimeFormat = @"yyyy-MM-dd hh-mm-ss";
        
        [STAThread]
		static void Main(string[] args)
		{
		    InitializeMessager();

		    var voting = new Voting();
            voting.ReadPalyers(@"../2015/players.txt");
            Messager.WriteLine("Считывание игроков завершено");
            Messager.WriteLine();

			var voiceReader = new VoiceReader(@"../2015/voices");
            var voices = voiceReader.GetVoices(voting);
			Messager.WriteLine("Считывание файлов голосов завершено");
            Messager.WriteLine("Удачно считано {0} файлов", voices.Length);
            Messager.WriteLine();

            var pointsByPlayer = voting.CalculateResult(voices);
		    var pairs = pointsByPlayer.OrderByDescending(x => x.Value).ToList();
            for (int i = 0; i < pairs.Count; i++)
            {
                var player = pairs[i].Key;
                var points = pairs[i].Value;
                Messager.WriteLine("{0,2}: {1,3} баллов - {2}", i+1, points, player.ToString());
            }

			Console.Read();
		}

	    private static void InitializeMessager()
	    {
            string timeString = DateTime.Now.ToString(TimeFormat);
            var fileName = string.Format("{0}.txt", timeString);
	        var filePath = Path.Combine("../2015/results/", fileName);
            IWriter fileWriter = new FileWriter(filePath);
            IWriter consoleWriter = new ConsoleWriter();
            IWriter outputWriter = new OutputWriter();

            Messager.AddWriter(fileWriter);
            Messager.AddWriter(consoleWriter);
            Messager.AddWriter(outputWriter);
	    }
	}
}
