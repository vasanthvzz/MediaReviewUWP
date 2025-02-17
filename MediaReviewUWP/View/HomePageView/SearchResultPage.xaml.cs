using MediaReviewClassLibrary.Models;
using MediaReviewUWP.Utility;
using MediaReviewUWP.View.Contract;
using MediaReviewUWP.ViewModel;
using MediaReviewUWP.ViewModel.Contract;
using MediaReviewUWP.ViewObject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace MediaReviewUWP.View.HomePageView
{
    public sealed partial class SearchResultPage : Page, ISearchPageView
    {
        public event EventHandler<MediaTileEventArgs> SearchResultClick;

        private ISearchPageViewModel _vm;

        public ObservableCollection<MediaTileVObj> MediaList { get; set; }

        public SearchResultPage()
        {
            _vm = new SearchPageViewModel(this);
            MediaList = new ObservableCollection<MediaTileVObj>();
            this.InitializeComponent();
        }

        public void Init(string searchText)
        {
            _vm.UniversalSearch(searchText);
            SearchedTextTb.Text = searchText;
        }

        public void UpdateSearch(List<MediaBObj> mediaList)
        {
            _ = Dispatcher.TryRunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                MediaList.Clear();
                foreach (var item in mediaList)
                {
                    MediaTileVObj mediaTile = new MediaTileVObj(item);
                    MediaList.Add(mediaTile);
                }
            });
        }

        private void MediaTileClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is MediaTileVObj mediaTile)
            {
                MediaTileEventArgs args = new MediaTileEventArgs(mediaTile);
                SearchResultClick?.Invoke(null, args);
            }
        }

        private void Image_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            if (sender is Image image)
            {
                image.Source = new BitmapImage(new Uri(ImageManager.GetDefaultTileImagePath()));
            }
        }
    }
}