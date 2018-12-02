using System;

namespace FindTheExcessImage
{
    public enum ImageColor
    {
        None,   //Google    Yandex   Bing
        Red,    //red       red      RED
        Orange, //orange    orange   ORANGE
        Yellow, //yellow    yellow   YELLOW
        Green,  //green     green    GREEN
        Teal,   //teal      cyan     TEAL
        Blue,   //blue      blue     BLUE
        Purple, //purple    violet   PURPLE
        Pink,   //pink      (red)    PINK
        White,  //white     white    WHITE
        Gray,   //gray      gray     GRAY
        Black,  //black     black    BLACK
        Brown  //brown     (orange) BROWN
    }

	public static class ImageColorHelper
	{
		private static readonly ImageColor[] ImageColors =
			{
				ImageColor.Black, 
				ImageColor.Blue, 
				ImageColor.Brown, 
				ImageColor.Gray, 
				ImageColor.Green, 
				ImageColor.Orange, 
				ImageColor.Pink, 
				ImageColor.Purple, 
				ImageColor.Red, 
				ImageColor.Teal, 
				ImageColor.White, 
				ImageColor.Yellow
			};

		public static ImageColor GetRandom()
		{
		    return new Random(DateTime.Now.Millisecond*DateTime.Now.Minute + DateTime.Now.Second).Next(100) > Game.ColoredImagesChance 
                ? ImageColors.GetRandom() 
                : ImageColor.None;
		}
	}
}