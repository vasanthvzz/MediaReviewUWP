using CommunityToolkit.WinUI.Collections;
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

namespace MediaReviewUWP.View.HomePageView
{
    public sealed partial class UserRatedMediaPage : Page, IUserRatedMediaPage, INotifyPropertyChanged, ITabItemContent
    {
        private IUserRatedMediaViewModel _vm;

        public event EventHandler<MediaTileEventArgs> RatedMediaClick;

        public event PropertyChangedEventHandler PropertyChanged;

        public AdvancedCollectionView MediaCollectionView {  get; private set; }
        public ObservableCollection<UserRatingVObj> UserRatingList { get; private set; }

        public UserRatedMediaPage()
        {
            UserRatingList = new ObservableCollection<UserRatingVObj>();
            MediaCollectionView = new AdvancedCollectionView(UserRatingList);
            this.InitializeComponent();
            _vm = new UserRatedMediaViewModel(this);
            UserRatingList.CollectionChanged += UserRatingList_CollectionChanged;
        }

        private void Page_Loaded(object sender, RoutedEventArgs args)
        {
            //CheckUserRatingList();
        }

        private void UserRatingList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            //CheckUserRatingList();
        }

        private void CheckUserRatingList()
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

        public void ReloadPageContent()
        {
            GetUserRatedMedia();
        }

        private void GetUserRatedMedia()
        {
            _vm.GetUserRatedMedia();
        }

        public async Task UpdateRatedMediaList(List<UserRatingBObj> userRatingList)
        {
            if (userRatingList == null)
            {
                return;
            }

            await Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
            {
                var mediaDict = userRatingList.GroupBy(m => m.MediaId)
                              .ToDictionary(g => g.Key, g => g.ToList());

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
                CheckUserRatingList();
            });
        }

        private void Image_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            if (sender is Image image)
            {
                image.Source = new BitmapImage(new Uri(ImageManager.GetDefaultTileImagePath()));
            }
        }

        private void RatingControl_ValueChanged(RatingControl sender, object args)
        {
            if (sender.DataContext is UserRatingVObj userRating)
            {
                _vm.ChangeUserRating(userRating.MediaId, (short)sender.Value);
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
            MediaCollectionView.SortDescriptions.Clear();
            if(order == SortOrder.Ascending)
            {
                MediaCollectionView.SortDescriptions.Add(new SortDescription("UserRating", SortDirection.Ascending));
            }
            else
            {
                MediaCollectionView.SortDescriptions.Add(new SortDescription("UserRating", SortDirection.Descending));
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