using MediaReviewClassLibrary.Models;
using MediaReviewUWP.View.Contract;
using MediaReviewUWP.View.MediaPageView;
using MediaReviewUWP.ViewModel;
using MediaReviewUWP.ViewModel.Contract;
using MediaReviewUWP.ViewObject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;


namespace MediaReviewUWP.View.HomePageView
{
    public sealed partial class ShowMediaListControl : Page,IShowMediaListView
    {

        public event EventHandler<MediaTileEventArgs> TileClicked;
        public ObservableCollection<MediaTileVObj> MediaList
        {
            get { return (ObservableCollection<MediaTileVObj>)GetValue(mediaListProperty); }
            set { SetValue(mediaListProperty, value); }
        }
        private IShowMediaListViewModel _vm;

        public static readonly DependencyProperty mediaListProperty =
            DependencyProperty.Register("MediaList", typeof(ObservableCollection<MediaTileVObj>), typeof(ShowMediaListControl), new PropertyMetadata(null));

        public ShowMediaListControl()
        {
            this.InitializeComponent();
            MediaList = new ObservableCollection<MediaTileVObj>();
            _vm = new ShowMediaListViewModel(this);
        }

        private void Page_Loaded(object sender, RoutedEventArgs args)
        {

        }

        public void UpdateMedia(List<MediaBObj> mediaList)
        {
            var mediaDict = mediaList.ToDictionary(m => m.MediaId);

            for (int i = MediaList.Count - 1; i >= 0; i--)
            {
                var existingMedia = MediaList[i];
                if (!mediaDict.ContainsKey(existingMedia.MediaId))
                {
                    MediaList.RemoveAt(i);
                }
            }

            foreach (var media in mediaList)
            {
                var existingMedia = MediaList.FirstOrDefault(m => m.MediaId == media.MediaId);
                if (existingMedia != null)
                {
                    existingMedia.UpdateFrom(media);
                }
                else
                {
                    MediaList.Add(new MediaTileVObj(media));
                }
            }
        }

        private void DisplayModeButton_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DisplayModeButton.SelectedIndex == 0)
            {
                MainContentPresenter.ContentTemplate = (DataTemplate)Resources["GridViewTemplate"];
            }
            else
            {
                MainContentPresenter.ContentTemplate = (DataTemplate)Resources["CompactListViewTemplate"];
            }
        }


        private void MediaTileClick(object sender, ItemClickEventArgs e)
        {
            var item = e.ClickedItem;

            MediaTileVObj media = (MediaTileVObj)item;
            MediaTileEventArgs eventArgs = new MediaTileEventArgs(media);
            TileClicked?.Invoke(this, eventArgs);
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

            public MediaTileEventArgs(UserRatingVObj media)
            {
                MediaId = media.MediaId;
                Title = media.MediaName;
            }
        }

        private void Image_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            if (sender is Image image)
            {
                image.Source = new BitmapImage(new Uri("ms-appx:///Assets/DefaultMediaImage.png"));
                image.Stretch =  Windows.UI.Xaml.Media.Stretch.Uniform;
            }
        }



        public void ReloadData()
        {
            _vm.GetPresentMediaDetails();
        }
    }
}
