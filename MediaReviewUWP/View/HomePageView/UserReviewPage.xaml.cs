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


namespace MediaReviewUWP.View.HomePageView
{
    public sealed partial class UserReviewPage : Page, IUserReviewPage, INotifyPropertyChanged
    {
        public ObservableCollection<UserReviewVObj> UserReviewList { get; set; }
        private IUserReviewViewModel _vm;
        public event PropertyChangedEventHandler PropertyChanged;

        public UserReviewPage()
        {
            this.InitializeComponent();
            _vm = new UserReviewViewModel(this);
            UserReviewList = new ObservableCollection<UserReviewVObj>();
            UserReviewList.CollectionChanged += UserReviewList_CollectionChanged;
        }

        private void UserReviewList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            CheckReviewList();
        }

        private void CheckReviewList()
        {
            if (UserReviewList != null && UserReviewList.Count != 0)
            { 
                EmptyReviewTb.Visibility = Visibility.Collapsed;
            }
            else
            {
                EmptyReviewTb.Visibility = Visibility.Visible;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs args)
        {
            _vm.GetUserReviews();
        }

        public void ReloadData()
        {
            _vm.GetUserReviews();
        }

        private void Image_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }

        public async void UpdateUserReviews(List<UserReviewBObj> userReviews)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                var reviewDict = userReviews.ToDictionary(r => r.ReviewId);

                for (int i = UserReviewList.Count - 1; i >= 0; i--)
                {
                    var existingReview = UserReviewList[i];
                    if (!reviewDict.ContainsKey(existingReview.ReviewId))
                    {
                        UserReviewList.RemoveAt(i);
                    }
                }

                foreach (var review in userReviews)
                {
                    var existingReview = UserReviewList.FirstOrDefault(r => r.ReviewId == review.ReviewId);
                    if (existingReview != null)
                    {
                        existingReview.UpdateFrom(review);
                    }
                    else
                    {
                        UserReviewList.Add(new UserReviewVObj(review));
                    }
                }
            });
        }

        private void ReviewMediaClick_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is UserReviewVObj review)
            {
                _vm.DeletReview(review.ReviewId);
            }
        }

        private async void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is UserReviewVObj review)
            {
                EditReviewTextBox.Text = review.Description;
                await EditReviewDialog.ShowAsync();
                string reviewContent = EditReviewTextBox.Text;

                if (reviewContent.Trim().Length > 0 && reviewContent.Trim().Length <= 512)
                {
                    _vm.EditReview(review.ReviewId, reviewContent);
                }
            }
        }

        private void EditReviewDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args) { }

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
         
        public async void DeleteReview(Review deletedReview)
        {
            if (deletedReview == null) return;
            var existingReview = UserReviewList.FirstOrDefault(review => deletedReview.ReviewId == review.ReviewId);

            if (existingReview != null)
            {
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    UserReviewList.Remove(existingReview);
                });
            }
        }

        public async void UpdateExistingReview(Review updatedReview)
        {
            if (updatedReview == null) return;
            var existingReview = UserReviewList.FirstOrDefault(review => review.ReviewId == updatedReview.ReviewId);

            // Update the Description if the deletedReview exists
            if (existingReview != null)
            {
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    existingReview.Description = updatedReview.Description;
                });
            }
        }
    }
}
