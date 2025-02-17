using System;
using Windows.Globalization;
using Windows.UI.Xaml;

namespace MediaReviewUWP.Utility
{
    public static class FontManager
    {
        public static event Action FontChanged;

        public static void UpdateFont()
        {
            var FontSize = new FontSizeRepo();

            var language = ApplicationLanguages.PrimaryLanguageOverride;
            if (language == "en")
            {
                FontSize = new FontSizeRepo(32, 20, 18, 12, 9);
            }
            else if (language == "ta")
            {
                FontSize = new FontSizeRepo(28, 20, 15, 12, 9);
            }
            SetFontSize(FontSize);
        }

        private static void SetFontSize(FontSizeRepo fontSizeRepo)
        {
            Application.Current.Resources["H1"] = fontSizeRepo.H1Size;
            Application.Current.Resources["H2"] = fontSizeRepo.H2Size;
            Application.Current.Resources["H3"] = fontSizeRepo.H3Size;
            Application.Current.Resources["H4"] = fontSizeRepo.H4Size;
            Application.Current.Resources["H5"] = fontSizeRepo.H5Size;
        }
    }

    public class FontSizeRepo
    {
        public double H1Size { get; set; }
        public double H2Size { get; set; }
        public double H3Size { get; set; }
        public double H4Size { get; set; }
        public double H5Size { get; set; }

        public FontSizeRepo(double h1, double h2, double h3, double h4, double h5)
        {
            H1Size = h1;
            H2Size = h2;
            H3Size = h3;
            H4Size = h4;
            H5Size = h5;
        }

        public FontSizeRepo()
        {
            H1Size = 32;
            H2Size = 24;
            H3Size = 18;
            H4Size = 15;
            H5Size = 12;
        }
    }
}