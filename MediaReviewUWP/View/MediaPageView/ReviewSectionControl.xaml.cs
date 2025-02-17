using MediaReviewClassLibrary;
using MediaReviewClassLibrary.Data;
using MediaReviewClassLibrary.Models;
using MediaReviewClassLibrary.Models.Enitites;
using MediaReviewUWP.View.Contract;
using MediaReviewUWP.ViewModel;
using MediaReviewUWP.ViewModel.Contract;
using MediaReviewUWP.ViewObject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace MediaReviewUWP.View.MediaPageView
{
    public sealed partial class ReviewSectionControl : UserControl, IManageReviewView, INotifyPropertyChanged
    {
        private IReviewSectionViewModel _vm;
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
            _vm = new ReviewSectionViewModel(this);
            this.InitializeComponent();
            ReviewList = new ObservableCollection<MediaReviewVObj>();
        }

        private void FetchMediaReview()
        {
            _vm.GetMediaReviews(_mediaId);
        }

        public void ReloadData()
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
                _vm.AddReview(MediaId, review);
            }
        }

        public void UpdateMediaReviewList(List<MediaReviewBObj> mediaReviews)
        {
            _ =  Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                var newReviewDict = mediaReviews.ToDictionary(r => r.ReviewId);
                for (int i = ReviewList.Count - 1; i >= 0; i--)
                {
                    var existingReview = ReviewList[i];
                    if (!newReviewDict.ContainsKey(existingReview.ReviewId))
                    {
                        ReviewList.RemoveAt(i);
                    }
                }

                foreach (var review in mediaReviews)
                {
                    var existingReview = ReviewList.FirstOrDefault(r => r.ReviewId == review.ReviewId);
                    if (existingReview != null)
                    {
                        existingReview.UpdateFrom(review);
                    }
                    else
                    {
                        ReviewList.Add(new MediaReviewVObj(review));
                    }
                }
            });
        }

        public async Task AddMediaReviewToList(MediaReviewBObj mediaReview)
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

        public async Task UpdateExistingReview(Review updatedReview)
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

        private void EditReviewButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is MediaReviewVObj review)
            {
                EditReviewTextBox.Text = review.Description;
                EditReviewDialog.Tag = review.ReviewId;
                _ = EditReviewDialog.ShowAsync();
            }
        }

        private void DeleteReviewButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is MediaReviewVObj review)
            {
                DeleteReviewDialog.Tag = review.ReviewId;
                _ = DeleteReviewDialog.ShowAsync();
            }
        }

        public async Task DeleteReviewFromList(Review deletedReview)
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

        public void UserRatingChanged(UserRatingChangedEventArgs e)
        {
            var userId = SessionManager.User.UserId;
            _ =  Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
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
            if (EditReviewTextBox.Text.Trim().Length == 0 || EditReviewTextBox.Text.Trim().Length > 512)
            {
                EditReviewErrorTextBox.Visibility = Visibility.Visible;
                SubmitReviewBtn.IsEnabled = false;
            }
            else
            {
                SubmitReviewBtn.IsEnabled = true;
                EditReviewErrorTextBox.Visibility = Visibility.Collapsed;
            }
        }

        public async Task ChangeFolloweeStatus(long followeeId, bool isFollowing)
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
            if (sender is ButtonBase b && b.DataContext is MediaReviewVObj review)
            {
                _vm.UpdateFollow(review.UserId, !review.Following);
            }
        }

        //private void EditReviewTextBox_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (EditReviewTextBox.Text.Trim().Length <= 0 || EditReviewTextBox.Text.Trim().Length > 512)
        //    {
        //        EditReviewErrorTextBox.Visibility = Visibility.Visible;
        //        if (EditReviewErrorTextBox.Text.Trim().Length > 512)
        //        {
        //            EditReviewErrorTextBox.Text = EditReviewErrorTextBox.Text.Substring(0, 512);
        //        }
        //    }
        //    else
        //    {
        //        EditReviewErrorTextBox.Visibility = Visibility.Collapsed;
        //        EditReviewDialog.IsPrimaryButtonEnabled = true;
        //    }
        //}

        private void ImageBrush_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
        }

        private void ContentDialogCloseBtn_Click(object sender, RoutedEventArgs e)
        {
            EditReviewDialog.Hide();
        }

        private void SubmitReviewBtn_Click(object sender, RoutedEventArgs e)
        {
            string reviewContent = EditReviewTextBox.Text;

            if (reviewContent.Trim().Length > 0 && reviewContent.Trim().Length <= 512)
            {
                _vm.EditReview((long)EditReviewDialog.Tag, reviewContent);
                EditReviewDialog.Hide();
            }
        }

        private void DeleteReviewContentDialogCloseBtn_Click(object sender, RoutedEventArgs e)
        {
            DeleteReviewDialog.Hide();
        }

        private void DeleteReviewBtn_Click(object sender, RoutedEventArgs e)
        {
             DeleteReviewDialog.Hide();
            _vm.DeleteReview((long)DeleteReviewDialog.Tag);
        }
    }
}