using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Data;

namespace MediaReviewUWP.ResourceDictionary
{
    public class ColorManipulationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is Color baseColor && parameter is string state)
            {
                switch (state)
                {
                    case "PointerOver":
                        return ManipulateColor(baseColor, 0.2f);
                    case "Pressed":
                        return ManipulateColor(baseColor, -0.2f);
                }
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        private Color ManipulateColor(Color color, float factor)
        {
            return Color.FromArgb(
                color.A,
                (byte)Math.Clamp(color.R + color.R * factor, 0, 255),
                (byte)Math.Clamp(color.G + color.G * factor, 0, 255),
                (byte)Math.Clamp(color.B + color.B * factor, 0, 255)
            );
        }
    }
}
