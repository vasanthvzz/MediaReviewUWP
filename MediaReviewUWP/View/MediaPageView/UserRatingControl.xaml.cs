using MediaReviewClassLibrary.Models.Enitites;
using MediaReviewUWP.View.Contract;
using MediaReviewUWP.ViewModel;
using MediaReviewUWP.ViewModel.Contract;
using Windows.UI.Xaml.Controls;

namespace MediaReviewUWP.View.MediaPageView
{

    public sealed partial class UserRatingControl : UserControl , IUserRatingView
    {
        private IUserRatingViewModel _vm;
        
        public Rating UserRating
        {
            get
            {
                return this.DataContext as Rating;
            }
            set { }
        }

        public UserRatingControl()
        {
            _vm = new UserRatingViewModel(this);
            this.InitializeComponent();
            this.DataContextChanged += (s, e) =>
            {
                Bindings.Update();
            };
        }

        private void UserRC_ValueChanged(RatingControl sender, object args)
        {
            _vm.UpdateUserRating(UserRating);
        }

        public void SetUserRating(Rating rating) 
        {
            UserRating = rating;
        }
    }
}


