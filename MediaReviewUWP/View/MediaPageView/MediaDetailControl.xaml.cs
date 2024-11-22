using MediaReviewUWP.ViewObjects;
using System;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace MediaReviewUWP.View.MediaPageView
{
    public sealed partial class MediaDetailControl : UserControl
    {
        public MediaDetailVObj MediaDetail
        {
            get
            { return this.DataContext as MediaDetailVObj; }
        }

        public MediaDetailControl()
        {
            this.InitializeComponent();
            this.DataContextChanged += (s, e) => { 
                Bindings.Update();
                InitAfterDetailFetched();
                PersonalMediaContentComponent.DataContext = MediaDetail?.UserPersonalMedia;
                UserRatingComponent.DataContext = MediaDetail?.UserRating;
            };
        }

        private void InitAfterDetailFetched()
        {
            if (MediaDetail != null && MediaDetail.Runtime.Trim().Length == 0)
            {
                FirstDotIcon.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
        }
    }
}