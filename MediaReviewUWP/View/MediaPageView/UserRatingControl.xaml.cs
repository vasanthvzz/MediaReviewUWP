using MediaReviewClassLibrary.Models.Enitites;
using MediaReviewUWP.View.Contract;
using MediaReviewUWP.ViewModel;
using MediaReviewUWP.ViewModel.Contract;
using System;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace MediaReviewUWP.View.MediaPageView
{
    public sealed partial class UserRatingControl : UserControl, IUserRatingView
    {
        private IUserRatingViewModel _vm;

        public event EventHandler<UserRatingChangedEventArgs> UserRatingChanged;

        public Rating UserRating
        {
            get
            {
                return this.DataContext as Rating;
            }
            set { this.DataContext = value; }
        }

        public UserRatingControl()
        {
            this.InitializeComponent();
            _vm = new UserRatingViewModel(this);

            this.DataContextChanged += (s, e) =>
            {
                Bindings.Update();
                if (UserRating != null)
                {
                    OnUserRatingChanged(new UserRatingChangedEventArgs(UserRating.Score));
                }
            };
        }

        private void UserRC_ValueChanged(RatingControl sender, object args)
        {
            short currentValue = (short)sender.Value;
            _vm.UpdateUserRating(UserRating);
        }

        public void OnUserRatingChanged(UserRatingChangedEventArgs e)
        {
            UserRatingChanged?.Invoke(this, e);
        }

        public async Task UpdatedUserRating(Rating userRating)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                UserRating.Score = userRating.Score;
                OnUserRatingChanged(new UserRatingChangedEventArgs(UserRating.Score));
            });
        }
    }

    public class UserRatingChangedEventArgs : EventArgs
    {
        public short CurrentValue { get; }

        public UserRatingChangedEventArgs(short currentValue)
        {
            CurrentValue = currentValue;
        }
    }
}