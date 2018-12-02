using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace VoicesCalculatorApplication
{
    public class Voting
    {
        private readonly int[] _pointsByPlace = { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };

        #region Players

        private readonly IList<Player> _players = new List<Player>();

        public IEnumerable<Player> Players
        {
            get { return _players; }
        }

        #endregion

        public void ReadPalyers(string filePath)
        {
            _players.Clear();
            using (var reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line == null)
                        continue;
                    var playerInfo = line.Split(' ');
                    StringBuilder stringBuilder = new StringBuilder();
                    for (int i = 0; i < playerInfo.Length - 1; i++)
                    {
                        stringBuilder.Append(playerInfo[i]);
                        if (i != playerInfo.Length - 2)
                            stringBuilder.Append(' ');
                    }
                    string name = stringBuilder.ToString();
                    string surname = playerInfo.Last();
                    var player = new Player(name, surname);
                    _players.Add(player);
                }
            }
        }

        public IDictionary<Player, int> CalculateResult(IEnumerable<Voice> voices)
        {
            var pointsByPlayer = Players.ToDictionary(x => x, x => 0);

            foreach (var voice in voices)
            {
                for (int i = 0; i < voice.Places.Length; i++)
                {
                    var player = voice.Places[i];
                    pointsByPlayer[player] += _pointsByPlace[i];
                }
            }

            return pointsByPlayer;
        }
    }
}
