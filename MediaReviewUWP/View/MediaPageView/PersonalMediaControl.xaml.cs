using MediaReviewClassLibrary.Models.Enitites;
using MediaReviewUWP.View.Contract;
using MediaReviewUWP.ViewModel;
using MediaReviewUWP.ViewModel.Contract;
using System;
using System.Diagnostics;
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
            Window.Current.SizeChanged += (s, e) =>
            {
                Debug.WriteLine(e.Size.Width);
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
            FavouriteStatusTb.Text = (bool)FavouriteTb.IsChecked ? "Favourited" : "Favourite";
            MarkAsSeenStatusTb.Text = (bool)HasWatchedTb.IsChecked ? "Marked as seen" : "Mark as seen";
            WathcListStatusTb.Text = (bool)WatchlistTb.IsChecked ? "In Watchlist" : "Watch list";
        }
    }
}
