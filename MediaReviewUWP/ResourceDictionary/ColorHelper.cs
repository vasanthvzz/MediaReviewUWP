using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace MediaReviewUWP.ResourceDictionary
{
    public static class ColorHelper
    {
        public static Color GetLighterShade(Color color, double factor = 0.2)
        {
            return AdjustBrightness(color, factor);
        }

        public static Color GetDarkerShade(Color color, double factor = 0.2)
        {
            return AdjustBrightness(color, -factor);
        }

        private static Color AdjustBrightness(Color color, double factor)
        {
            factor = Clamp(factor, -1.0, 1.0);

            byte r = AdjustColorChannel(color.R, factor);
            byte g = AdjustColorChannel(color.G, factor);
            byte b = AdjustColorChannel(color.B, factor);

            return Color.FromArgb(color.A, r, g, b);
        }

        private static byte AdjustColorChannel(byte channel, double factor)
        {
            double adjusted = channel + (factor > 0 ? (255 - channel) * factor : channel * factor);
            return (byte)Clamp(adjusted, 0, 255);
        }

        private static double Clamp(double value, double min, double max)
        {
            return value < min ? min : (value > max ? max : value);
        }
    }
}
