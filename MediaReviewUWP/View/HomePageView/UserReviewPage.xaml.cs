using MediaReviewClassLibrary.Models;
using MediaReviewClassLibrary.Models.Enitites;
using MediaReviewUWP.Utility;
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
using Windows.UI.Xaml.Media.Imaging;

namespace MediaReviewUWP.View.HomePageView
{
    public sealed partial class UserReviewPage : Page, IUserReviewPage, INotifyPropertyChanged, ITabItemContent
    {
        private IUserReviewViewModel _vm;

        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler<MediaTileEventArgs> MediaReviewClicked;

        public ObservableCollection<UserReviewVObj> UserReviewList
        {
            get { return (ObservableCollection<UserReviewVObj>)GetValue(UserReviewListProperty); }
            set { SetValue(UserReviewListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UserReviewList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UserReviewListProperty =
            DependencyProperty.Register("UserReviewList", typeof(ObservableCollection<UserReviewVObj>), typeof(UserReviewPage), new PropertyMetadata(null));

        public UserReviewPage()
        {
            this.InitializeComponent();
            _vm = new UserReviewViewModel(this);
            UserReviewList = new ObservableCollection<UserReviewVObj>();
            UserReviewList.CollectionChanged += UserReviewList_CollectionChanged;
        }

        private void Page_Loaded(object sender, RoutedEventArgs args)
        {
            _vm.GetUserReviews();
            //CheckReviewList();
        }

        private void UserReviewList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            //CheckReviewList();
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

        public void ReloadPageContent()
        {
            _vm.GetUserReviews();
        }

        private void Image_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            if (sender is Image image)
            {
                image.Source = new BitmapImage(new Uri(ImageManager.GetDefaultTileImagePath()));
            }
        }

        public async Task UpdateUserReviews(List<UserReviewBObj> userReviews)
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

                CheckReviewList();
            });
        }

        private void UserReviewListView_Click(object sender, ItemClickEventArgs e)
        {
            UserReviewVObj userReview = e.ClickedItem as UserReviewVObj;
            MediaTileEventArgs args = new MediaTileEventArgs(userReview);
            MediaReviewClicked?.Invoke(null, args);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is UserReviewVObj review)
            {
                DeleteReviewDialog.Tag = review.ReviewId;
                _ = DeleteReviewDialog.ShowAsync();
            }
        }

        private  void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is UserReviewVObj review)
            {
                EditReviewTextBox.Text = review.Description;
                EditReviewDialog.Tag = review.ReviewId;
                _ = EditReviewDialog.ShowAsync();
            }
        }

        private void EditReviewTextBox_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            if (EditReviewTextBox.Text.Trim().Length <= 0 || EditReviewTextBox.Text.Trim().Length > 512)
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

        public async Task DeleteReview(Review deletedReview)
        {
            if (deletedReview == null) return;
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                var existingReview = UserReviewList.FirstOrDefault(review => deletedReview.ReviewId == review.ReviewId);
                if (existingReview != null)
                {
                    UserReviewList.Remove(existingReview);
                }
                CheckReviewList();
            });
        }

        public async Task UpdateExistingReview(Review updatedReview)
        {
            if (updatedReview == null) return;
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                var existingReview = UserReviewList.FirstOrDefault(review => review.ReviewId == updatedReview.ReviewId);
                if (existingReview != null)
                {
                    existingReview.Description = updatedReview.Description;
                }
            });
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

        private void Grid_PointerEntered(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            if (sender is Grid grid)
            {
                var reviewButtonGrid = grid.FindName("ReviewButtonGrid") as Grid;
                if (reviewButtonGrid != null)
                {
                    reviewButtonGrid.Visibility = Visibility.Visible;
                }
            }
        }

        private void Grid_PointerExited(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            if (sender is Grid grid)
            {
                var reviewButtonGrid = grid.FindName("ReviewButtonGrid") as Grid;
                if (reviewButtonGrid != null)
                {
                    reviewButtonGrid.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void DeleteReviewBtn_Click(object sender, RoutedEventArgs e)
        {
            DeleteReviewDialog.Hide();
            _vm.DeleteReview((long)DeleteReviewDialog.Tag);
        }

        private void DeleteReviewContentDialogCloseBtn_Click(object sender, RoutedEventArgs e)
        {
            DeleteReviewDialog.Hide();
        }
    }
}