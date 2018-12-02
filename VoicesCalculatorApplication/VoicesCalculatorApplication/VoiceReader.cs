using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace VoicesCalculatorApplication
{
	public class VoiceReader
	{
	    #region Fields

	    private readonly string _folderPath;

	    #endregion

	    #region Ctor

	    public VoiceReader(string folderPath)
	    {
	        _folderPath = folderPath;
	    }

	    #endregion

	    public Voice[] GetVoices(Voting voting)
		{
            return Directory.GetFiles(_folderPath)
                .Select(x => GetVoice(voting, x))
                .Where(x => x != null)
                .ToArray();
		}

        private Voice GetVoice(Voting voting, string filePath)
		{
            var fileName = Path.GetFileNameWithoutExtension(filePath);
            using (var reader = new StreamReader(filePath))
            {
                var fileContent = reader.ReadToEnd();
                var lines = fileContent.SplitIgnoringEmptyChars('\n');
                if (lines.Length < Voice.PlacesCount)
                {
                    Messager.WriteLine("ОШИБКА: Количество строк в файле {0} не должно быть меньше {1}!", fileName, Voice.PlacesCount);
                    return null;
                }
                if (lines.Length > Voice.PlacesCount)
                {
                    Messager.WriteLine("ОШИБКА: Количество строк в файле {0} не должно быть больше {1}!", fileName, Voice.PlacesCount);
                    return null;
                }

                var voice = new Voice();

                bool arePlayersDetermined = true;
                for (int i = 0; i < Voice.PlacesCount; i++)
                {
                    var line = lines[i];
                    var player = GetPlayer(voting, line);
                    if (player == null)
                    {
                        Messager.WriteLine("ОШИБКА: В файле {0} не определён игрок из строки \"{1}\"", fileName, line);
                        arePlayersDetermined = false;
                    }
                    voice.Places[i] = player;
                }

                if (!arePlayersDetermined)
                    return null;

                var equalItems = voice.Places.GetEqualItems();
                if (equalItems.Any())
                {
                    foreach (var equalItem in equalItems)
                        Messager.WriteLine("ОШИБКА: В файле {0} игрок \"{1}\" записан более одного раза!", fileName, equalItem);
                    return null;
                }

                Messager.WriteLine("Файл {0} успешно считан!", fileName);
                return voice;
            }
		}

        private static Player GetPlayer(Voting voting, string line)
        {
            var strings = line.SplitIgnoringEmptyChars('.', ' ');

            foreach (var str in strings)
            {
                var player = voting.Players.FirstOrDefault(x => x.Surname.Equals(str, StringComparison.InvariantCultureIgnoreCase));
                if (player != null)
                    return player;
            }
            return null;
        }
    }
}
