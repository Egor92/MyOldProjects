using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FindTheExcessImage
{
    abstract class ImageProvider
    {
        private readonly HttpClient _client = new HttpClient();

        public abstract Task<Picture[]> GetImages(Word word, ImageColor color);

        private bool _isAvailable;

        private async Task<bool> TestTheConnection(Word word)
        {
            return (await GetImages(word, ImageColor.None)).Any();
        }

        public static async Task<bool> HasConnection(Word word)
        {
            foreach (var imageProvider in Providers)
                imageProvider._isAvailable = await imageProvider.TestTheConnection(word);

            return Providers.Any(x => x._isAvailable);
        }

        private static readonly ImageProvider[] Providers = new ImageProvider[] {new Google(), new Yandex(), new Bing()};

        public static ImageProvider GetRandomProvider()
        {
            var availabledProviders = Providers.Where(x => x._isAvailable).ToArray();

            if (!availabledProviders.Any())
                return null;

            return availabledProviders[new Random().Next(0, availabledProviders.Length)];
        }


        private class Google : ImageProvider
        {
	        static Google()
	        {
		        ColorsStrings = new Dictionary<ImageColor, string>
                {
                    { ImageColor.Red, "red" },
                    { ImageColor.Orange, "orange" },
                    { ImageColor.Yellow, "yellow" },
                    { ImageColor.Green, "green" },
                    { ImageColor.Teal, "teal" },
                    { ImageColor.Blue, "blue" },
                    { ImageColor.Purple, "purple" },
                    { ImageColor.Pink, "pink" },
                    { ImageColor.White, "white" },
                    { ImageColor.Gray, "gray" },
                    { ImageColor.Black, "black" },
                    { ImageColor.Brown, "brown" },
                };
	        }

			private readonly Regex _regex = new Regex(@"imgurl=(.*?)&amp;w=(\d*).*?""tu"":""(.*?)""");

	        private static readonly Dictionary<ImageColor, string> ColorsStrings;

            public override async Task<Picture[]> GetImages(Word word, ImageColor color)
            {
                try
                {
                    string colorString = color != ImageColor.None ? ",ic:specific,isc:" + ColorsStrings[color] : "";
                    var uri = new Uri(string.Format("https://www.google.ru/search?q={0}&safe=active&tbs=isz:m,sur:f{1}&tbm=isch",
                                word, colorString));
                    var response = await _client.GetStringAsync(uri);

                    var result = _regex.Matches(response)
                                .Cast<Match>()
								.Where(x => int.Parse(x.Groups[2].ToString()) <= 900)
                                .Select(x => new Picture(word, x.Groups[1].ToString(), x.Groups[3].ToString()))
								.ToArray();
                    return result;
                }
                catch
                {
                    return new Picture[0];
                }
            }
        }

        

        private class Yandex : ImageProvider
        {
	        static Yandex()
	        {
				ColorsStrings = new Dictionary<ImageColor, string>
                {
                    { ImageColor.Red, "red" },
                    { ImageColor.Orange, "orange" },
                    { ImageColor.Yellow, "yellow" },
                    { ImageColor.Green, "green" },
                    { ImageColor.Teal, "teal" },
                    { ImageColor.Blue, "blue" },
                    { ImageColor.Purple, "violet" },
                    { ImageColor.Pink, "red" },
                    { ImageColor.White, "white" },
                    { ImageColor.Gray, "gray" },
                    { ImageColor.Black, "black" },
                    { ImageColor.Brown, "orange" },
                };
	        }

            private readonly Regex _regex = new Regex(@"<img class=""b-images-item__preview"".*? src=""(.*?)"".*?<a class=""b-link b-images-item__maxdim"" href=""(.*?)""");

	        private static readonly Dictionary<ImageColor, string> ColorsStrings;

            public override async Task<Picture[]> GetImages(Word word, ImageColor color)
            {
                try
                {
                    string colorString = color != ImageColor.None ? "&icolor=" + ColorsStrings[color] : "";
                    var response = await _client.GetStringAsync(new Uri(string.Format("http://images.yandex.ru/yandsearch?text={0}&isize=small{1}", word, colorString)));

                    return _regex.Matches(response)
                         .Cast<Match>()
                         .Select(x => new Picture(word, x.Groups[2].ToString(), x.Groups[1].ToString()))
                         .ToArray();
                }
                catch
                {
                    return new Picture[0];
                }
            }
        }


        private class Bing : ImageProvider
        {
	        static Bing()
	        {
				ColorsStrings = new Dictionary<ImageColor, string>
                {
                    { ImageColor.Red, "RED" },
                    { ImageColor.Orange, "ORANGE" },
                    { ImageColor.Yellow, "YELLOW" },
                    { ImageColor.Green, "GREEN" },
                    { ImageColor.Teal, "TEAL" },
                    { ImageColor.Blue, "BLUE" },
                    { ImageColor.Purple, "PURPLE" },
                    { ImageColor.Pink, "PINK" },
                    { ImageColor.White, "WHITE" },
                    { ImageColor.Gray, "GRAY" },
                    { ImageColor.Black, "BLACK" },
                    { ImageColor.Brown, "BROWN" },
                };
	        }

            private readonly Regex _regex = new Regex(@"oi:&quot;(.*?)&quot;.*?<img.*?src2=""(.*?)""");

	        private static readonly Dictionary<ImageColor, string> ColorsStrings;

            public override async Task<Picture[]> GetImages(Word word, ImageColor color)
            {
                try
                {
					string parametersString = color != ImageColor.None ? "+filterui:color2-FGcls_" + ColorsStrings[color] : string.Empty;
					var response = await _client.GetStringAsync(new Uri(string.Format("http://www.bing.com/images/search?filt=all&q={0}&qft=+filterui:imagesize-medium{1}", word, parametersString)));

                    return _regex.Matches(response)
                         .Cast<Match>()
                         .Select(x => new Picture(word, x.Groups[1].ToString(), x.Groups[2].ToString()))
                         .ToArray();
                }
                catch
                {
                    return new Picture[0];
                }
            }
        }
    }
}
