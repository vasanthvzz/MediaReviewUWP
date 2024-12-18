using Windows.UI;
using Windows.UI.Xaml.Media;

namespace MediaReviewUWP.Utils
{
    public enum AccentColor
    {
        BLUE,
        GREEN,
        ORANGE
    }

    public class Accent
    {
        public AccentColor ThemeColor { get; set; }

        public SolidColorBrush DarkShade1 { get; set; }
        public SolidColorBrush DarkShade2 { get; set; }
        public SolidColorBrush DarkShade3 { get; set; }

        public SolidColorBrush LightShade1 { get; set; }
        public SolidColorBrush LightShade2 { get; set; }
        public SolidColorBrush LightShade3 { get; set; }

        public SolidColorBrush MainShade { get; set; }

        public Accent(AccentColor themeColor, SolidColorBrush mainShade, SolidColorBrush lightShade1,
            SolidColorBrush lightShade2, SolidColorBrush lightShade3, SolidColorBrush darkShade1,
            SolidColorBrush darkShade2, SolidColorBrush darkShade3)
        {
            ThemeColor = themeColor;
            MainShade = mainShade;
            LightShade1 = lightShade1;
            LightShade2 = lightShade2;
            LightShade3 = lightShade3;
            DarkShade1 = darkShade1;
            DarkShade2 = darkShade2;
            DarkShade3 = darkShade3;
        }
    }

    public static class AccentRepo
    {
        private static Accent _blueTheme;
        private static Accent _orangeTheme;
        private static Accent _greenTheme;

        static AccentRepo()
        {
            // Define Blue Accent
            _blueTheme = new Accent(
                            themeColor: AccentColor.BLUE,
                            mainShade: new SolidColorBrush(Color.FromArgb(0xFF, 0x1A, 0x87, 0xE3)),
                            lightShade1: new SolidColorBrush(Color.FromArgb(0xFF, 0x3F, 0x9A, 0xE7)), 
                            lightShade2: new SolidColorBrush(Color.FromArgb(0xFF, 0x63, 0xAD, 0xEC)),
                            lightShade3: new SolidColorBrush(Color.FromArgb(0xFF, 0x88, 0xC1, 0xF0)),
                            darkShade1: new SolidColorBrush(Color.FromArgb(0xFF, 0x1A, 0x52, 0x82)), 
                            darkShade2: new SolidColorBrush(Color.FromArgb(0xFF, 0x1A, 0x39, 0x53)), 
                            darkShade3: new SolidColorBrush(Color.FromArgb(0xFF, 0x1A, 0x30, 0x42)) 
                                );


            // Define Orange Accent
            _orangeTheme = new Accent(
                themeColor: AccentColor.ORANGE,
                            mainShade: new SolidColorBrush(Color.FromArgb(0xFF, 0xCF, 0x89, 0x1F)),
                            lightShade1: new SolidColorBrush(Color.FromArgb(0xFF, 0xD7, 0x9C, 0x43)),
                            lightShade2: new SolidColorBrush(Color.FromArgb(0xFF, 0xDE, 0xAF, 0x67)),
                            lightShade3: new SolidColorBrush(Color.FromArgb(0xFF, 0xE6, 0xC2, 0x8B)), 
                            darkShade1: new SolidColorBrush(Color.FromArgb(0xFF, 0x78, 0x53, 0x1C)),
                            darkShade2: new SolidColorBrush(Color.FromArgb(0xFF, 0x5C, 0x42, 0x1C)),
                            darkShade3: new SolidColorBrush(Color.FromArgb(0xFF, 0x3E, 0x30, 0x1B))
                            );

            // Define Green Accent
            _greenTheme = new Accent(
                themeColor: AccentColor.GREEN,
                 mainShade: new SolidColorBrush(Color.FromArgb(0xFF, 0x51, 0x92, 0x54)),
                            lightShade1: new SolidColorBrush(Color.FromArgb(0xFF, 0x6D, 0xA3, 0x6F)),
                            lightShade2: new SolidColorBrush(Color.FromArgb(0xFF, 0x89, 0xB5, 0x8B)),
                            lightShade3:  new SolidColorBrush(Color.FromArgb(0xFF, 0xA5, 0xC6, 0xA6)),
                            darkShade1: new SolidColorBrush(Color.FromArgb(0xFF, 0x36, 0x58, 0x38)),
                            darkShade2: new SolidColorBrush(Color.FromArgb(0xFF, 0x2E, 0x46, 0x2F)),
                            darkShade3: new SolidColorBrush(Color.FromArgb(0xFF, 0x25, 0x32, 0x26))
            );
        }

        public static Accent GetBlueTheme() { return _blueTheme; }
        public static Accent GetGreenTheme() { return _greenTheme; }
        public static Accent GetOrangeTheme() { return _orangeTheme; }
    }
}
