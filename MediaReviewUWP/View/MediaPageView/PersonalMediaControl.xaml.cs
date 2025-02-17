using MediaReviewClassLibrary.Models.Enitites;
using MediaReviewUWP.View.Contract;
using MediaReviewUWP.ViewModel;
using MediaReviewUWP.ViewModel.Contract;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace MediaReviewUWP.View.MediaPageView
{
    public sealed partial class PersonalMediaControl : UserControl, IPersonalMediaControl
    {
        private IPersonalMediaViewModel _viewModel;

        public PersonalMedia UserPersonalMedia
        {
            get
            { return this.DataContext as PersonalMedia; }
            set { this.DataContext = value; }
        }

        public PersonalMediaControl()
        {
            _viewModel = new PersonalMediaViewModel(this);
            this.InitializeComponent();
            this.DataContextChanged += (s, e) =>
            {
                Bindings.Update();
                ChangebuttonContent();
            };
        }

        public void UpdatePersonalMedia(PersonalMedia personalMedia)
        {
            this.DataContext = personalMedia;
        }

        private void PersonalMediaUpdate(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleButton)
            {
                ChangebuttonContent();
                _viewModel?.UpdatePersonalMedia(UserPersonalMedia);
            }
        }

        private void ChangebuttonContent()
        {
            var loader = new ResourceLoader();
            FavouriteStatusTb.Text = (bool)FavouriteTb.IsChecked ? loader.GetString("Favourited") : loader.GetString("Favourite");
            MarkAsSeenStatusTb.Text = (bool)HasWatchedTb.IsChecked ? loader.GetString("MarkedAsSeen") : loader.GetString("MarkAsSeen");
            WathcListStatusTb.Text = (bool)WatchlistTb.IsChecked ? loader.GetString("InWatchlist") : loader.GetString("Watchlist");
        }
    }
}