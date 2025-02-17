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
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace MediaReviewUWP.View.HomePageView
{
    
    public sealed partial class FilteredMediaPage : Page , IFilteredMediaPage
    {
        private IFilteredMediaViewModel _vm;
        public event EventHandler<MediaTileEventArgs> TileClicked;
        public ObservableCollection<Genre> GenreList { get; private set; }
        public ObservableCollection<MediaTileVObj> MediaList { get; private set; }
        public FilteredMediaPage()
        {
            _vm = new FilteredMediaViewModel(this);
            MediaList = new ObservableCollection<MediaTileVObj>();
            GenreList = new ObservableCollection<Genre>();
            this.InitializeComponent();
        }

        public void Init(List<Genre> genreList)
        {
            _ = Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                GenreList.Clear();
                foreach (var genre in genreList)
                {
                    GenreList.Add(genre);
                }
            });
            _vm.GetFilteredMedia(genreList);
        }

        public void UpdateMediaList(List<MediaBObj> filteredMediaList)
        {
            _ = Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                MediaList.Clear();
                foreach (var media in filteredMediaList)
                {
                    if (media != null)
                    {
                        MediaList.Add(new MediaTileVObj(media));
                    }
                }
            });
        }

        private void MediaTileClick(object sender, ItemClickEventArgs e)
        
        {
            MediaTileEventArgs args = new MediaTileEventArgs(e.ClickedItem as MediaTileVObj);
            TileClicked?.Invoke(null, args);
        }

        private void Image_ImageFailed(object sender, Windows.UI.Xaml.ExceptionRoutedEventArgs e)
        {
            if (sender is Image image)
            {
                image.Source = new BitmapImage(new Uri(ImageManager.GetDefaultTileImagePath()));
            }
        }
    }
}
