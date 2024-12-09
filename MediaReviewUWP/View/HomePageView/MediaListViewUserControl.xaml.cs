using MediaReviewUWP.View.MediaPageView;
using MediaReviewUWP.ViewObject;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


namespace MediaReviewUWP.View.HomePageView
{
    public sealed partial class MediaListViewUserControl : UserControl 
    {

        private bool ListViewSelected { get; set; } = false;

        public ObservableCollection<MediaTileVObj> MediaList
        {
            get { return (ObservableCollection<MediaTileVObj>)GetValue(MediaListProperty); }
            set { SetValue(MediaListProperty, value);
                if (MediaList != null && MediaList.Any())
                {
                    MediaListView.SelectedItem = MediaList.First();
                }
            }
        }

        public static readonly DependencyProperty MediaListProperty =
            DependencyProperty.Register("MediaList", typeof(ObservableCollection<MediaTileVObj>), typeof(MediaListViewUserControl), new PropertyMetadata(null));



        public MediaListViewUserControl()
        {
            this.InitializeComponent();
            MediaList = new ObservableCollection<MediaTileVObj>();
            this.SizeChanged += MediaListViewUserControl_SizeChanged;
        }

        private void MediaListViewUserControl_SizeChanged(object sender = null, SizeChangedEventArgs e = null)
        {
            //Debug.WriteLine(ListViewSelected);
            if(this.ActualWidth > 1000)
            {
                BothFocused.IsActive = true;
                ListViewFocused.IsActive = false;
                PageContentFocused.IsActive = false ;
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

        private void ListView_SelectionChanged(object sender,  SelectionChangedEventArgs e)
        {
            var page = new MediaPage();
            CompactMediaPresenter.Content = page;
            var listView = sender as ListView;
            var tile = listView.SelectedItem as MediaTileVObj;
            page.MediaDetailControlComponent.MediaRatingChanged -= MediaDetailControlComponent_MediaRatingChanged;
            page.MediaDetailControlComponent.MediaRatingChanged += MediaDetailControlComponent_MediaRatingChanged;
            page.DataContextChanged -= MediaPageDataContext_Changed;
            page.DataContextChanged += MediaPageDataContext_Changed;
            page.Init(tile.MediaId);
        }

        private async void MediaDetailControlComponent_MediaRatingChanged(object sender, MediaRatingChangeEventArgs e)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                var listItem = MediaListView.SelectedItem as MediaTileVObj;
                listItem.UpdateMediaRating(e.MediaRating);
            });
        }

        private void MediaPageDataContext_Changed(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            if (CompactMediaPresenter.Content is MediaPage page)
            {
                var item = MediaListView.SelectedItem as MediaTileVObj;
                if (sender is MediaPage mediaPage)
                {
                    var rating = mediaPage?.MediaDetailControlComponent?.MediaDetail?.MediaRating;
                    if(rating != null)
                    {
                        item.MediaRating = rating.ToString();
                    }
                }
                page.ReloadData();
            }
        }

        private void Image_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

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
    }
}
