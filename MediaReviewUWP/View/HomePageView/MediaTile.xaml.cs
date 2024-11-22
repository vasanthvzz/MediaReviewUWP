using MediaReviewClassLibrary.Models;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace MediaReviewUWP.View.HomePageView
{
    public sealed partial class MediaTile : UserControl
    {
        public MediaTileVObj mediaTile
        {
            get
            { return this.DataContext as MediaTileVObj; }
        }

        public MediaTile()
        {
            this.InitializeComponent();
            this.DataContextChanged += (s, e) => Bindings.Update();
        }
    }
}
