using System;
using Windows.UI.Xaml.Media.Imaging;

namespace MediaReviewUWP.Utility
{
    public static class ImageManager
    {
        public static BitmapImage GetDefaultTileImage()
        {
            return new BitmapImage(new Uri("ms-appx:///Assets/PiperImage1.png"));
        }

        public static string GetDefaultTileImagePath()
        {
            return "ms-appx:///Assets/tempImage.jpg";
        }

        public static string GetDefaultPosterImagePath()
        {
            return "ms-appx:///Assets/defaultPosterImage.jpg";
        }
    }
}