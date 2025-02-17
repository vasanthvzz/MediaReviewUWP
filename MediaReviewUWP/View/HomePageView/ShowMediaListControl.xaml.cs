using CommunityToolkit.WinUI.Collections;
using MediaReviewClassLibrary.Models;
using MediaReviewUWP.Utility;
using MediaReviewUWP.View.Contract;
using MediaReviewUWP.ViewModel;
using MediaReviewUWP.ViewModel.Contract;
using MediaReviewUWP.ViewObject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace MediaReviewUWP.View.HomePageView
{
    public sealed partial class ShowMediaListControl : Page, IShowMediaListView, ITabItemContent
    {
        private IShowMediaListViewModel _vm;

        public event EventHandler<MediaTileEventArgs> TileClicked;

        public event EventHandler<ListReachedEndArgs> ListReachedEnd;
        public AdvancedCollectionView MediaCollectionView {  get; private set; }

        public ObservableCollection<MediaTileVObj> MediaList
        {
            get { return (ObservableCollection<MediaTileVObj>)GetValue(mediaListProperty); }
            set { SetValue(mediaListProperty, value); }
        }

        public static readonly DependencyProperty mediaListProperty =
            DependencyProperty.Register("MediaList", typeof(ObservableCollection<MediaTileVObj>), typeof(ShowMediaListControl), new PropertyMetadata(null));

        public ShowMediaListControl()
        {
            MediaList = new ObservableCollection<MediaTileVObj>();
            MediaCollectionView = new AdvancedCollectionView(MediaList);
            MediaList.CollectionChanged += MediaList_CollectionChanged;
            GlobalEventManager.OnMediaAdded += GlobalEventManager_OnMediaAdded;
            _vm = new ShowMediaListViewModel(this);
            this.InitializeComponent();
        }

        private void GlobalEventManager_OnMediaAdded(object sender, MediaAddedEventArgs e)
        {
            var medialist = new List<MediaTileVObj>(MediaList);
            var minReleaseDate = medialist.Min(media => media.ReleaseDate);
            if(minReleaseDate != null && minReleaseDate <= e.ReleaseDate)
            {
                _vm.GetMedia(e.MediaId);
            }
        }

        private void MediaList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
        }

        private void Page_Loaded(object sender, RoutedEventArgs args)
        {
            MediaCollectionView.SortDescriptions.Clear();
            MediaCollectionView.SortDescriptions.Add(new SortDescription("ReleaseDate", SortDirection.Descending));
            MediaCollectionView.Refresh();
        }

        public void UpdateMedia(List<MediaBObj> mediaList)
        {
            foreach (var media in mediaList)
            {
                var existingMedia = MediaList.FirstOrDefault(m => m.MediaId == media.MediaId);
                if (existingMedia == null)
                {
                    MediaList.Add(new MediaTileVObj(media));
                }
                else
                {
                    existingMedia.UpdateFrom(media);
                }
            }
        }

        private void DisplayModeButton_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainContentPresenter != null)
            {
                MainContentPresenter.ContentTemplate = (DataTemplate)Resources[
                DisplayModeButton.SelectedIndex == 0 ? "GridViewTemplate" : "ListViewTemplate"];
            }
        }

        private void MediaTileClick(object sender, ItemClickEventArgs e)
        {
            var item = e.ClickedItem;

            MediaTileVObj media = (MediaTileVObj)item;
            MediaTileEventArgs eventArgs = new MediaTileEventArgs(media);
            TileClicked?.Invoke(this, eventArgs);
        }

        private void Image_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            if (sender is Image image)
            {
                image.Source = new BitmapImage(new Uri("ms-appx:///Assets/DefaultMediaImage.png"));
                image.Stretch = Windows.UI.Xaml.Media.Stretch.Uniform;
            }
        }
        
        public void ReloadPageContent()
        {
            if (MainContentPresenter.ContentTemplate == (DataTemplate)Resources["ListViewTemplate"])
            {
                var mediaControl = FindChild<MediaListViewUserControl>(MainContentPresenter);
                mediaControl?.ReloadCurrentMedia();
            }
        }

        private T FindChild<T>(DependencyObject parent) where T : DependencyObject
        {
            int childCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T foundChild)
                {
                    return foundChild;
                }

                var childOfChild = FindChild<T>(child);
                if (childOfChild != null)
                {
                    return childOfChild;
                }
            }
            return null;
        }

        private void MediaGridViewUserControl_TileClicked(object sender, MediaTileEventArgs e)
        {
            TileClicked?.Invoke(this, e);
        }

        private void ScrollViewerReachedEnd()
        {
            ListReachedEndArgs args = new ListReachedEndArgs(MediaList.Count);
            ListReachedEnd?.Invoke(this, args);
        }

        public void AddMedia(MediaDetailBObj mediaDetails)
        {
            MediaTileVObj mediaTile = new MediaTileVObj(mediaDetails);
            _ = Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.High, () =>
            {
                MediaList.Add(mediaTile);
                MediaCollectionView.Refresh();
            });
        }
    }

    public class MediaTileEventArgs : EventArgs
    {
        public long MediaId { get; set; }
        public string Title { get; set; }

        public MediaTileEventArgs(MediaTileVObj media)
        {
            MediaId = media.MediaId;
            Title = media.Title;
        }

        public MediaTileEventArgs(UserReviewVObj review)
        {
            MediaId = review.MediaId;
            Title = review.MediaName;
        }

        public MediaTileEventArgs(UserRatingVObj media)
        {
            MediaId = media.MediaId;
            Title = media.MediaName;
        }
    }

    public class ListReachedEndArgs : EventArgs
    {
        public long ExistingItemCount { get; set; }

        public ListReachedEndArgs(long existingItemCount)
        {
            ExistingItemCount = existingItemCount;
        }
    }
}