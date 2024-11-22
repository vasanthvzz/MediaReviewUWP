using Windows.UI;
using Windows.UI.Xaml.Media;

namespace MediaReviewUWP.Utils
{
    public enum ThemeColor
    {
        BLUE,
        GREEN,
        ORANGE
    }

    public class Theme
    {
        public ThemeColor ThemeColor { get; set; }
        public SolidColorBrush MainShade { get; set; }
        public SolidColorBrush LightShade { get; set; }
        public SolidColorBrush DarkShade { get; set; }
        public SolidColorBrush DarkestShade { get; set; }

        public Theme(ThemeColor themeColor, SolidColorBrush mainShade, SolidColorBrush lightShade, SolidColorBrush darkShade, SolidColorBrush darkestShade)
        {
            ThemeColor = themeColor;
            MainShade = mainShade;
            LightShade = lightShade;
            DarkShade = darkShade;
            DarkestShade = darkestShade;
        }
    }

    public static class ThemeRepo
    {
        private static Theme BlueTheme;
        private static Theme OrangeTheme;
        private static Theme GreenTheme;

        static ThemeRepo()
        {
            // Define Blue Theme
            BlueTheme = new Theme(
                themeColor: ThemeColor.BLUE,
                mainShade: new SolidColorBrush(Color.FromArgb(0xFF, 0x1A, 0x87, 0xE3)),   // Main shade #FF1A87E3
                lightShade: new SolidColorBrush(Color.FromArgb(0xFF, 0x88, 0xC1, 0xF0)),  // Light shade
                darkShade: new SolidColorBrush(Color.FromArgb(0xFF, 0x1A, 0x39, 0x53)),   // Dark shade
                darkestShade: new SolidColorBrush(Color.FromArgb(0xFF, 0x1A, 0x30, 0x42)) // Darkest shade
            );

            // Define Orange Theme
            OrangeTheme = new Theme(
                themeColor: ThemeColor.ORANGE,
                mainShade: new SolidColorBrush(Color.FromArgb(0xFF, 0xCF, 0x89, 0x1F)),   // Main shade
                lightShade: new SolidColorBrush(Color.FromArgb(0xFF, 0xE6, 0xC2, 0x8B)),  // Light shade
                darkShade: new SolidColorBrush(Color.FromArgb(0xFF, 0x5B, 0x47, 0x2A)),   // Dark shade
                darkestShade: new SolidColorBrush(Color.FromArgb(0xFF, 0x3E, 0x30, 0x1B)) // Darkest shade
            );

            // Define Green Theme
            GreenTheme = new Theme(
                themeColor: ThemeColor.GREEN,
                mainShade: new SolidColorBrush(Color.FromArgb(0xFF, 0x51, 0x92, 0x54)),   // Main shade
                lightShade: new SolidColorBrush(Color.FromArgb(0xFF, 0xA5, 0xC6, 0xA6)),  // Light shade
                darkShade: new SolidColorBrush(Color.FromArgb(0xFF, 0x38, 0x4A, 0x38)),   // Dark shade
                darkestShade: new SolidColorBrush(Color.FromArgb(0xFF, 0x25, 0x32, 0x26)) // Darkest shade
            );
        }

        public static Theme GetBlueTheme() { return BlueTheme; }
        public static Theme GetGreenTheme() { return GreenTheme; }
        public static Theme GetOrangeTheme() { return OrangeTheme; }
    }
}
