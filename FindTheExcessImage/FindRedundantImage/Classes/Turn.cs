using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace FindTheExcessImage
{
    class Turn
    {
        public Word ExcessWord { get; private set; }
        public Word RightWord { get; private set; }
        public List<Picture> RightPictures { get; private set; }
		public Picture ExcessPicture { get; private set; }
		public IEnumerable<Picture> DisplayedImages
        {
            get { return RightPictures.Take(Game.ImagesCount - 1).Concat(new[] { ExcessPicture }).Mix(); }
        }

        public Turn(Word rightWord, Word excessWord)
        {
            RightWord = rightWord;
            ExcessWord = excessWord;
			RightPictures = new List<Picture>();
        }

        public async Task Initialize()
        {
			var color = ImageColorHelper.GetRandom();
            RightPictures = (await GetImages(RightWord, color)).Mix().GetRandom(Game.ImagesCount - 1).ToList();
			ExcessPicture = (await GetImages(ExcessWord, color)).GetRandom();

            await Task.WhenAll(DisplayedImages.Select(x => x.Preload()));
        }

		private static async Task<Picture[]> GetImages(Word word, ImageColor color)
		{
			var result = await ImageProvider.GetRandomProvider().GetImages(word, color);
			if (!result.Any())
			{
				if (!(await ImageProvider.HasConnection(word)))
				{
					throw new Exception("Images for this word not found: " + word);
				}
				do
				{
					result = await ImageProvider.GetRandomProvider().GetImages(word, color);
				} while (!result.Any());
				return result;
			}
			return result;
		}

        public async Task PreloadImages()
        {
            Parallel.ForEach(DisplayedImages, x => x.Preload());
        }

	    public bool SelectPicture(Picture selectedPicture)
	    {
		    if (selectedPicture == ExcessPicture)
		    {
			    selectedPicture.Brush = new SolidColorBrush(Colors.Green);
			    return true;
		    }
		    else
		    {
			    ExcessPicture.Brush = new SolidColorBrush(Colors.Green);
			    selectedPicture.Brush = new SolidColorBrush(Colors.Red);
			    return false;
		    }
	    }
    }
}
