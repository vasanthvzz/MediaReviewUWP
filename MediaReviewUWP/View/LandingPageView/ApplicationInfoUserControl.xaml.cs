using System;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace MediaReviewUWP.View.LandingPageView
{
    public sealed partial class ApplicationInfoUserControl : UserControl
    {
        public event Action SigninButtonClick;

        public ApplicationInfoUserControl()
        {
            this.InitializeComponent();
        }

        private void MainPipsPager_SelectedIndexChanged(Microsoft.UI.Xaml.Controls.PipsPager sender, Microsoft.UI.Xaml.Controls.PipsPagerSelectedIndexChangedEventArgs args)
        {
            var index = MainPipsPager.SelectedPageIndex;

            string title = "";
            string description = "";
            string imageName = $"assets/PiperImage1.png";
            PagerImage.Source = new BitmapImage(new Uri($"ms-appx:///{imageName}"));
            var loader = new ResourceLoader();
            title = loader.GetString("PipsPageTitle" + (index + 1));
            description = loader.GetString("PipsPageDesc" + (index + 1));

            PagerTitle.Text = title;
            PagerDescription.Text = description;
        }

        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            SigninButtonClick?.Invoke();
        }
    }
}