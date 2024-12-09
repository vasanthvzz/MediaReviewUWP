using MediaReviewClassLibrary.Models;
using MediaReviewClassLibrary.Models.Enitites;
using MediaReviewUWP.View.Contract;
using MediaReviewUWP.ViewModel;
using MediaReviewUWP.ViewModel.Contract;
using MediaReviewUWP.ViewObject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;
using static MediaReviewUWP.View.HomePageView.ShowMediaListControl;


namespace MediaReviewUWP.View.HomePageView
{

    public sealed partial class UserRatedMediaPage : Page, IUserRatedMediaPage, INotifyPropertyChanged
    {
        public ObservableCollection<UserRatingVObj> UserRatingList { get; set; }
        private IUserRatedMediaViewModel _vm;
        public event EventHandler<MediaTileEventArgs> RatedMediaClick;
        public event PropertyChangedEventHandler PropertyChanged;
        private CollectionViewSource _sortedUserRatingView;

        public UserRatedMediaPage()
        {
            this.InitializeComponent();
            _vm = new UserRatedMediaViewModel(this);
            UserRatingList = new ObservableCollection<UserRatingVObj>();
            _sortedUserRatingView = new CollectionViewSource
            {
                Source = UserRatingList
            };
            RatedMediaGrid.ItemsSource = _sortedUserRatingView.View;
            UserRatingList.CollectionChanged += UserRatingList_CollectionChanged;
        }

        private void UserRatingList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            CheckUserRating();
        }

        private void Page_Loaded(object sender, RoutedEventArgs args)
        {
            //MainScrollViewer.Height = Window.Current.Bounds.Height - 100;
            //Window.Current.SizeChanged += (s, e) =>
            //{
            //    MainScrollViewer.Height = e.Size.Height - 100;
            //};
        }
        
        private void CheckUserRating()
        {
            if (UserRatingList != null && UserRatingList.Count != 0)
            {
                EmptyMediaTb.Visibility = Visibility.Collapsed;
                SortButton.Visibility = Visibility.Visible;
            }
            else
            {
                EmptyMediaTb.Visibility = Visibility.Visible;
                SortButton.Visibility = Visibility.Collapsed;
            }
        }

        public void ReloadData()
        {
            GetUserRatedMedia();
        }

        private void GetUserRatedMedia()
        {
            _vm.GetUserRatedMedia();
        }

        public async Task UpdateRatedMediaList(List<UserRatingBObj> userRatingList)
        {
            if (userRatingList == null) return;

            await Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
            {
                var mediaDict = userRatingList.ToDictionary(m => m.MediaId);

                for (int i = UserRatingList.Count - 1; i >= 0; i--)
                {
                    var existingMedia = UserRatingList[i];
                    if (!mediaDict.ContainsKey(existingMedia.MediaId))
                    {
                        UserRatingList.RemoveAt(i);
                    }
                }

                foreach (var rating in userRatingList)
                {
                    var existingRating = UserRatingList.FirstOrDefault(r => r.MediaId == rating.MediaId);
                    if (existingRating != null)
                    {
                        existingRating.UserRating = rating.UserRating;
                    }
                    else
                    {
                        UserRatingList.Add(new UserRatingVObj(rating));
                    }
                }
            });
        }

        private void Image_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            if (sender is Image image)
            {
                image.Source = new BitmapImage(new Uri("ms-appx:///Assets/DefaultMediaImage.png"));
                image.Stretch = Windows.UI.Xaml.Media.Stretch.Uniform;
            }
        }

        private void RatingControl_ValueChanged(RatingControl sender, object args)
        {
            if(sender.DataContext is UserRatingVObj userRating)
            {
                _vm.ChangeUserRating(userRating.MediaId,(short)sender.Value);
            }
        }

        public async Task UpdatedMediaRating(Rating rating)
        {
            await Dispatcher.TryRunAsync(CoreDispatcherPriority.Normal, () =>
            {
                foreach (var item in UserRatingList)
                {
                    if (item.MediaId == rating.MediaId)
                    {
                        item.UserRating = rating.Score;
                        break;
                    }
                }
            });
        }

        private void LowToHigh_Click(object sender, RoutedEventArgs e)
        {
            ApplySorting(SortOrder.Ascending);
        }

        private void HighToLow_Click(object sender, RoutedEventArgs e)
        {
            ApplySorting(SortOrder.Descending);
        }


        private void ApplySorting(SortOrder order)
        {
            var sortedList = order == SortOrder.Ascending
                ? UserRatingList.OrderBy(item => item.UserRating).ToList()
                : UserRatingList.OrderByDescending(item => item.UserRating).ToList();

            UserRatingList.Clear();
            foreach (var item in sortedList)
            {
                UserRatingList.Add(item);
            }
        }

        private void RatedMediaGrid_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            var rating = e.ClickedItem as UserRatingVObj;
            MediaTileEventArgs eventArgs = new MediaTileEventArgs(rating);
            RatedMediaClick?.Invoke(this, eventArgs);
        }

        private void RatingControl_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            e.Handled = true;
        }
    }
}
