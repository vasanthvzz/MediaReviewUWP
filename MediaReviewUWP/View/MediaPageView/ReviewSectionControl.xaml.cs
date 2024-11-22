using MediaReviewClassLibrary.Models;
using MediaReviewClassLibrary.Models.Enitites;
using MediaReviewUWP.View.Contract;
using MediaReviewUWP.ViewModel;
using MediaReviewUWP.ViewModel.Contract;
using MediaReviewUWP.ViewObjects;
using System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;



namespace MediaReviewUWP.View.MediaPageView
{
    public sealed partial class ReviewSectionControl : UserControl, IReviewSectionView 
    {
        private IReviewSectionViewModel _viewModel;

        private long _mediaId;
        public long MediaId
        {
            get => _mediaId;
            set
            {
                if (_mediaId != value)
                {
                    _mediaId = value;
                    OnMediaIdFetched();
                }
            }
        }

        public ReviewSectionControl()
        {
            _viewModel = new ReviewSectionViewModel(this);
            this.InitializeComponent();
        }

        private void OnMediaIdFetched()
        {
            if (MediaId != 0)
            {
                RetrieveReviews();
            }
        }

        private void RetrieveReviews()
        {
          ReviewListComponent.RetriveMediaReviews(MediaId);
        }

        private void ReviewTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null  && textBox.Text.Length > 512)
            {
                DisplayMessage("Input Error" , "Review cannot exceed 512 characters.");

                textBox.Text = textBox.Text.Substring(0, 512);

                textBox.SelectionStart = textBox.Text.Length;
            }
        }

        // Helper method to display an error message
        private void DisplayMessage(string title,string message)
        {
            var dialog = new ContentDialog()
            {
                Title = title,
                Content = message,
                CloseButtonText = "OK"
            };

            _ = dialog.ShowAsync(); // Show the dialog asynchronously
        }

        private void ReviewSubmit_Click(object sender, RoutedEventArgs e)
        {
            var review = ReviewTb.Text.Trim();
            if(review.Length != 0)
            {
                _viewModel.AddReview(MediaId,review);
            }
        }

        public async void OnReviewAdded(MediaReviewBObj review)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                DisplayMessage("Review Added", "Your review has been added successfully!");
                ReviewTb.Text = "";
            });
            UpdateReviewList(review);
        }

        private void UpdateReviewList(MediaReviewBObj review)
        {
            ReviewListComponent.UpdateMediaReviews(new MediaReviewVObj(review));
        }
    }
}
