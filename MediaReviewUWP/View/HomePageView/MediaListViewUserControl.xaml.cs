using CommunityToolkit.WinUI.Collections;
using MediaReviewUWP.Utility;
using MediaReviewUWP.View.MediaPageView;
using MediaReviewUWP.ViewObject;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace MediaReviewUWP.View.HomePageView
{
    public sealed partial class MediaListViewUserControl : UserControl
    {
        private bool ListViewSelected { get; set; } = false;
        private MediaPage _mediaPage { get; set; }
        public event EventHandler<MediaTileEventArgs> NewMediaTabRequested;
        public AdvancedCollectionView tempView { get; set; }

        public event Action ScrollViewerEnd;

        public MediaListViewUserControl()
        {
            this.InitializeComponent();
            this.SizeChanged += MediaListViewUserControl_SizeChanged;
        }

        private void MediaListViewUserControl_SizeChanged(object sender = null, SizeChangedEventArgs e = null)
        {
            if (this.ActualWidth > 1000)
            {
                BothFocused.IsActive = true;
                ListViewFocused.IsActive = false;
                PageContentFocused.IsActive = false;
                ListViewSelected = false;
            }
            else
            {
                BothFocused.IsActive = false;
                if (ListViewSelected)
                {
                    ListViewFocused.IsActive = true;
                    PageContentFocused.IsActive = false;
                }
                else
                {
                    ListViewFocused.IsActive = false;
                    PageContentFocused.IsActive = true;
                }
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_mediaPage == null)
            {
                _mediaPage = new MediaPage();
                CompactMediaPresenter.Content = _mediaPage;
            }
            var listView = sender as ListView;
            var tile = listView.SelectedItem as MediaTileVObj;
            _mediaPage.ScrollToTop();
            _ = Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.High , () =>
            {
                if(tile != null)_mediaPage.Init(tile.MediaId);
            });
            _mediaPage.MediaDetailControlComponent.MediaRatingChanged -= MediaDetailControlComponent_MediaRatingChanged;
            _mediaPage.MediaDetailControlComponent.MediaRatingChanged += MediaDetailControlComponent_MediaRatingChanged;
        }

        private async void MediaDetailControlComponent_MediaRatingChanged(object sender, MediaRatingChangeEventArgs e)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                var listItem = MediaListView.SelectedItem as MediaTileVObj;
                listItem.UpdateMediaRating(e.MediaRating);
            });
        }

        private void Image_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            if (sender is Image image)
            {
                image.Source = new BitmapImage(new Uri(ImageManager.GetDefaultTileImagePath()));
            }
        }

        private void ToggleViewButton_Click(object sender, RoutedEventArgs e)
        {
            ListViewSelected = true;
            MediaListViewUserControl_SizeChanged();
        }

        private void MediaListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            ListViewSelected = false;
            MediaListViewUserControl_SizeChanged();
        }

        private void ScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            var scrollViewer = sender as ScrollViewer;

            if (scrollViewer != null)
            {
                if (scrollViewer.VerticalOffset >= scrollViewer.ScrollableHeight)
                {
                    ScrollViewerEnd?.Invoke();
                }
            }
        }

        private void MediaListView_Loaded(object sender, RoutedEventArgs e)
        {
            var scrollViewer = FindScrollViewer(MediaListView);
            if (scrollViewer != null)
            {
                scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
                scrollViewer.ViewChanged -= ScrollViewer_ViewChanged;
                scrollViewer.ViewChanged += ScrollViewer_ViewChanged;
            }
        }

        private ScrollViewer FindScrollViewer(DependencyObject parent)
        {
            if (parent == null) return null;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is ScrollViewer scrollViewer)
                {
                    return scrollViewer;
                }
                else
                {
                    var result = FindScrollViewer(child);
                    if (result != null) return result;
                }
            }
            return null;
        }

        public void ReloadCurrentMedia()
        {
            _mediaPage?.ReloadPageContent();
        }

        private void NewTabBtn_Click(object sender, RoutedEventArgs e)
        {
            if(MediaListView.SelectedItem is MediaTileVObj item)
            {
                MediaTileEventArgs args = new MediaTileEventArgs(item);
                NewMediaTabRequested?.Invoke(null, args);
            }
        }
    }
}