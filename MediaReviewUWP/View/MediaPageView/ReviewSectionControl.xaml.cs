using MediaReviewClassLibrary;
using MediaReviewClassLibrary.Models;
using MediaReviewClassLibrary.Models.Enitites;
using MediaReviewClassLibrary.Utlis;
using MediaReviewUWP.View.Contract;
using MediaReviewUWP.ViewModel;
using MediaReviewUWP.ViewModel.Contract;
using MediaReviewUWP.ViewObject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace MediaReviewUWP.View.MediaPageView
{
    public sealed partial class ReviewSectionControl : UserControl, IManageReviewView, INotifyPropertyChanged
    {
        private ISessionManager _sessionManager = MediaReviewDIServiceProvider.GetRequiredService<ISessionManager>();
        private IReviewSectionViewModel _viewModel;
        public ObservableCollection<MediaReviewVObj> ReviewList { get; set; }

        private long _mediaId;

        public event PropertyChangedEventHandler PropertyChanged;

        public long MediaId
        {
            get => _mediaId;
            set
            {
                if (_mediaId != value)
                {
                    _mediaId = value;
                }
            }
        }

        public ReviewSectionControl()
        {
            _viewModel = new ReviewSectionViewModel(this);
            this.InitializeComponent();
            ReviewList = new ObservableCollection<MediaReviewVObj>();
        }

        private void FetchMediaReview()
        {
            _viewModel.GetMediaReviews(MediaId);
        }

        internal void ReloadData()
        {
            FetchMediaReview();
        }

        private void ReviewTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && textBox.Text.Length > 512)
            {
                DisplayMessage("Input Error", "Review cannot exceed 512 characters.");

                textBox.Text = textBox.Text.Substring(0, 512);

                textBox.SelectionStart = textBox.Text.Length;
            }
        }

        private void DisplayMessage(string title, string message)
        {
            var dialog = new ContentDialog()
            {
                Title = title,
                Content = message,
                CloseButtonText = "OK"
            };

            _ = dialog.ShowAsync();
        }

        private void ReviewSubmit_Click(object sender, RoutedEventArgs e)
        {
            var review = ReviewTb.Text.Trim();
            if (review.Length != 0)
            {
                ReviewTb.Text = "";
                _viewModel.AddReview(MediaId, review);
            }
        }

        public async void OnReviewAdded(MediaReviewBObj review)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                DisplayMessage("Review Added", "Your deletedReview has been added successfully!");
                ReviewTb.Text = "";
            });
            AddMediaReviewToList(review);
        }

        public async void UpdateMediaReviewList(List<MediaReviewBObj> mediaReviews)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                var reviewDict = ReviewList.ToDictionary(r => r.ReviewId);

                for (int i = ReviewList.Count - 1; i >= 0; i--)
                {
                    var existingReview = ReviewList[i];
                    if (!reviewDict.ContainsKey(existingReview.ReviewId))
                    {
                        ReviewList.RemoveAt(i);
                    }
                }

                foreach (var review in mediaReviews)
                {
                    var existingRating = ReviewList.FirstOrDefault(r => r.ReviewId == review.ReviewId);
                    if (existingRating != null)
                    {
                        existingRating.UpdateFrom(review);
                    }
                    else
                    {
                        ReviewList.Add(new MediaReviewVObj (review));
                    }
                }
            });
        }

        public async void AddMediaReviewToList(MediaReviewBObj mediaReview)
        {
            var existingReview = ReviewList.FirstOrDefault(review => review.ReviewId == mediaReview.ReviewId);
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                if (existingReview != null)
                {
                    existingReview.Description = mediaReview.Description;
                }
                else
                {
                    ReviewList.Add(new MediaReviewVObj(mediaReview));
                }
            });
        }

        public async void UpdateExistingReview(Review updatedReview)
        {
            if (updatedReview == null) return;
            var existingReview = ReviewList.FirstOrDefault(review => review.ReviewId == updatedReview.ReviewId);

            if (existingReview != null)
            {
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    existingReview.Description = updatedReview.Description;
                });
            }
        }

        private async void EditReviewButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is MediaReviewVObj review)
            {
                EditReviewTextBox.Text = review.Description;
                await EditReviewDialog.ShowAsync();
                string reviewContent = EditReviewTextBox.Text;
                long userId = _sessionManager.RetriveUserFromStorage().UserId;

                if (reviewContent.Trim().Length > 0 && reviewContent.Trim().Length <= 512)
                {
                    _viewModel.EditReview(review.ReviewId, review.UserId, reviewContent);
                }
            }
        }

        private void EditReviewDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args) { }

        private void DeleteReviewButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is MediaReviewVObj review)
            {
                _viewModel.DeleteReview(review.ReviewId);
            }
        }

        public async void DeleteReviewFromList(Review deletedReview)
        {
            if (deletedReview == null) return;
            var existingReview = ReviewList.FirstOrDefault(review => deletedReview.ReviewId == review.ReviewId);

            if (existingReview != null)
            {
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    ReviewList.Remove(existingReview);
                });
            }
        }

        public async void UserRatingChanged(UserRatingChangedEventArgs e)
        {
            var userId = _sessionManager.RetriveUserFromStorage().UserId;
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                foreach (var review in ReviewList)
                {
                    if (review.UserId == userId)
                    {
                        review.UserRating = e.CurrentValue;
                    }
                }
            });
        }

        private void EditReviewTextBox_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            if (EditReviewTextBox.Text.Trim().Length <= 0 || EditReviewTextBox.Text.Trim().Length > 512)
            {
                EditReviewErrorTextBox.Visibility = Visibility.Visible;
                EditReviewDialog.IsPrimaryButtonEnabled = false;
            }
            else
            {
                EditReviewErrorTextBox.Visibility = Visibility.Collapsed;
                EditReviewDialog.IsPrimaryButtonEnabled = true;
            }
        }

        public async void ChangeFolloweeStatus(long followeeId, bool isFollowing)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                foreach (var review in ReviewList.Where(review => review.UserId == followeeId))
                {
                    review.Following = isFollowing;
                }
            });
        }

        private void FollowButton_Click(object sender, RoutedEventArgs e)
        {
            if(sender is ButtonBase b && b.DataContext is MediaReviewVObj review)
            {
                _viewModel.UpdateFollow(review.UserId, !review.Following);
            }
        }
    }
}
