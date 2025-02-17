using System;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace MediaReviewUWP.Utility
{
    public static class FileManager
    {
        public static async Task<bool> FileExistsAsync(string profilePictureUri)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(profilePictureUri))
                {
                    var uri = new Uri(profilePictureUri);

                    if (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps)
                    {
                        var httpClient = new Windows.Web.Http.HttpClient();
                        var requestTask = httpClient.SendRequestAsync(new Windows.Web.Http.HttpRequestMessage(Windows.Web.Http.HttpMethod.Head, uri));
                        var timeoutTask = Task.Delay(500);
                        var completedTask = Task.WaitAny(requestTask.AsTask(), timeoutTask);
                        if (timeoutTask.IsCompleted)
                        {
                            return false;
                        }
                        var response = await requestTask;
                        return response.StatusCode == Windows.Web.Http.HttpStatusCode.Ok;
                    }
                    else if (uri.Scheme == Uri.UriSchemeFile)
                    {
                        var localPath = uri.LocalPath;
                        var file = await Windows.Storage.StorageFile.GetFileFromPathAsync(localPath);
                        return file != null;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }

            return false;
        }

        public static async Task LoadImageAsync(string imagePath, BitmapImage bitmapImage, BitmapImage placeholderImage)
        {
            try
            {
                if (await FileManager.FileExistsAsync(imagePath))
                {
                    bitmapImage.UriSource = new Uri(imagePath, UriKind.Absolute);
                }
                else
                {
                    bitmapImage.UriSource = placeholderImage.UriSource;
                }
            }
            catch
            {
                bitmapImage.UriSource = placeholderImage.UriSource;
            }
        }
    }
}