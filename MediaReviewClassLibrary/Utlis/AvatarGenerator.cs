using SkiaSharp;
using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;

namespace MediaReviewClassLibrary.Utlis
{
    public static class AvatarGenerator
    {
        public static async Task<string> GenerateImage(string userName)
        {
            // Ensure the userName is not empty or null and extract the first letter
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentException("User name cannot be null or empty.", nameof(userName));

            // Get the first letter of the user name and ensure it's uppercase
            char initialLetter = char.ToUpper(userName[0]);

            // Image dimensions
            int width = 200, height = 200;

            // Create a new bitmap with SkiaSharp
            using (var surface = SKSurface.Create(new SKImageInfo(width, height)))
            {
                var canvas = surface.Canvas;

                // Randomize a background color based on the initial letter
                Random random = new Random(initialLetter.GetHashCode());
                SKColor backgroundColor = new SKColor(
                    (byte)random.Next(100, 256),
                    (byte)random.Next(100, 256),
                    (byte)random.Next(100, 256)
                );

                // Fill the background with the generated color
                canvas.Clear(backgroundColor);

                // Set font and text alignment for the initial letter
                using (var paint = new SKPaint())
                {
                    paint.Color = SKColors.White;
                    paint.TextSize = 150;
                    paint.IsAntialias = true;
                    paint.TextAlign = SKTextAlign.Center;
                    paint.Typeface = SKTypeface.CreateDefault();

                    // Measure the text size
                    SKRect textBounds = new SKRect();
                    paint.MeasureText(initialLetter.ToString(), ref textBounds);

                    // Calculate the position of the initial letter in the center of the image
                    float x = width / 2f;
                    float y = height / 2f + textBounds.Height / 2f;  // Adjust for text height

                    // Draw the initial letter in the center of the image
                    canvas.DrawText(initialLetter.ToString(), x, y, paint);
                }

                // Get the app's local folder to save the image
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;

                // Create the GeneratedAvatars folder inside the LocalFolder if it doesn't exist
                StorageFolder avatarsFolder = await localFolder.CreateFolderAsync("GeneratedAvatars", CreationCollisionOption.OpenIfExists);

                // Set the file path with the user's name (use the full user name, not just the initial)
                string fileName = $"{userName}_Avatar.png";
                StorageFile avatarFile = await avatarsFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);

                // Save the image as PNG
                using (var image = surface.Snapshot())
                using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
                using (var stream = await avatarFile.OpenStreamForWriteAsync())
                {
                    data.SaveTo(stream);
                }

                return avatarFile.Path;  // Return the full file path of the generated avatar
            }
        }
    }
}
