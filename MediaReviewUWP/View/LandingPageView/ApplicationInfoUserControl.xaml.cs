using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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

            switch (index)
            {
                case 0:
                    {
                        title = "Discover & Explore";
                        description = "Immerse yourself in a world of media like never before. Discover trending movies, TV shows, and captivating stories curated just for you. Dive into a personalized journey of entertainment.";
                        break;
                    }
                case 1:
                    {
                        title = "Rate & Review";
                        description = " Share your opinions and make your voice heard. Rate your favorite shows, leave detailed reviews, and help others find their next binge-worthy experience.";
                        break;
                    }
                    case 2:
                    {
                        title = "Connect & Engage";
                        description = "Join a thriving community of movie enthusiasts. Follow other users, interact with their reviews, and build your profile as a media connoisseur.";
                        break;
                    }
            }
            PagerTitle.Text = title;
            PagerDescription.Text = description;
        }

        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            SigninButtonClick?.Invoke();
        }
    }
}
