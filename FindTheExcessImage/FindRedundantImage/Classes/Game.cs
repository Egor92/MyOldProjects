using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using PortableClassLibrary;

namespace FindTheExcessImage
{
    static class Game
    {
        public const int TurnsCount = 50;
        public const int HintsCount = 5;

        private static Turn[] _turns = new Turn[TurnsCount];
        private static int _hintsUsedCount;
        private static Word[] _words;

        public static int ImagesCount = 4;

        public static Task[] Tasks = new Task[TurnsCount];

        public static int GuessedImagesCount { get; private set; }
        public static int CurrentTurnNumber { get; private set; }
        public static int ColoredImagesChance { get; private set; }
        public static Turn CurrentTurn
        {
            get { return _turns[CurrentTurnNumber]; }
        }

        public static int HintsLeft
        {
            get { return HintsCount - _hintsUsedCount; }
        }


        public static async Task InitializeFirstTurn()
        {
            CurrentTurnNumber = 0;
            GuessedImagesCount = 0;
            _hintsUsedCount = 0;

            ColoredImagesChance = await GetColoredImagesChance();
            await WordsManager.LoadWords();

            _words = WordsManager.GetRandomWords(TurnsCount).ToArray();

            _turns[0] = new Turn(_words[0], _words[GetNextInteger(0, TurnsCount)]);
            await _turns[0].Initialize();

        }


        private static int GetNextInteger(int index, int count)
        {
            return index == count - 1 ? 0 : index + 1;
        }

        private static async Task<int> GetColoredImagesChance()
        {
            try
            {
                return int.Parse(await new HttpClient().GetStringAsync(SpecialPaths.ColoredImagesChanceFilePath));
            }
            catch
            {
                return 90;
            }
        }


        public static async Task DoTurn(Picture selectedPicture)
        {
            bool isRightChoice = CurrentTurn.SelectPicture(selectedPicture);
            if (isRightChoice)
                GuessedImagesCount++;
            int delay = isRightChoice ? 1000 : 3000;

            await Task.Delay(delay);

            if (CurrentTurnNumber == TurnsCount - 1)
            {
                CurrentTurnNumber = TurnsCount;
                return;
            }

            await Tasks[CurrentTurnNumber + 1];
            CurrentTurnNumber++;
        }

        public static bool IsExcessImage(Picture selectedImage)
        {
            return CurrentTurn.ExcessPicture == selectedImage;
        }

        public static Word GetHint()
        {
            if (HintsLeft <= 0)
                return null;
            _hintsUsedCount++;
            return CurrentTurn.RightWord;
        }

        public static async void PreloadImages()
        {
            for (int i = 1; i < TurnsCount; i++)
                _turns[i] = new Turn(_words[i], _words[GetNextInteger(i, TurnsCount)]);

            _turns = _turns.Mix(from: 1, to: TurnsCount-1).ToArray();

            for (int i = 1; i < TurnsCount; i++)
            {
                Tasks[i] = _turns[i].Initialize();
                await Tasks[i];
            }
        }
    }
}
