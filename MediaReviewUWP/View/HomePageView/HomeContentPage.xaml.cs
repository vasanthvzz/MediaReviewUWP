using MediaReviewClassLibrary.Models.Enitites;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MediaReviewUWP.View.HomePageView
{
    public sealed partial class HomeContentPage : Page
    {
        public HomeContentPage()
        {
            this.InitializeComponent();
        }

        public void UpdateMedia(List<Media> mediaList)
        {
            MediaTileGridComponent.UpdateMedia(mediaList);
        }
    }
}
