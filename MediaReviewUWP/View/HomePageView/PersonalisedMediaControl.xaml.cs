using MediaReviewClassLibrary.Models;
using MediaReviewClassLibrary.Models.Constants;
using MediaReviewUWP.View.Contract;
using MediaReviewUWP.ViewModel;
using MediaReviewUWP.ViewModel.Contract;
using MediaReviewUWP.ViewObject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace MediaReviewUWP.View.HomePageView
{
    public sealed partial class PersonalisedMediaControl : Page ,IPersonalisedMediaView , ITabItemContent
    {
        public PersonalMediaType PersonalisedMediaType { get; private set; }
        private IPersonalisedMediaViewModel _vm;
        public event EventHandler<MediaTileEventArgs> PersonalisedMediaTileClicked;

        public ObservableCollection<MediaTileVObj> MediaList
        {
            get { return (ObservableCollection<MediaTileVObj>)GetValue(mediaListProperty); }
            set { SetValue(mediaListProperty, value);
                MediaList.CollectionChanged += MediaList_CollectionChanged;
            }
        }

        public static readonly DependencyProperty mediaListProperty =
            DependencyProperty.Register("MediaList", typeof(ObservableCollection<MediaTileVObj>), typeof(ShowMediaListControl), new PropertyMetadata(null));

        public PersonalisedMediaControl()
        {
            _vm = new PersonalisedMediaViewModel(this);
            this.InitializeComponent();
            MediaList = new ObservableCollection<MediaTileVObj>();
        }
        
        public void Init(PersonalMediaType personalMediaType)
        {
            PersonalisedMediaType = personalMediaType;
        }

        private void MediaList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            CheckMedia();
        }

        private void CheckMedia()
        {
            if (MediaList != null && MediaList.Count != 0)
            {
                EmptyMediaTb.Visibility = Visibility.Collapsed;
                DisplayModeButton.Visibility = Visibility.Visible;
            }
            else
            {
                EmptyMediaTb.Visibility = Visibility.Visible;
                DisplayModeButton.Visibility = Visibility.Collapsed;
            }
        }

        public void ReloadData() 
        {
            _vm.GetPersonlisedMedia(PersonalisedMediaType);
        }
        

        public void UpdateMedia(List<MediaBObj> mediaList)
        {
           _ = Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                MediaList.Clear();
                foreach (var media in mediaList)
                {
                    MediaList.Add(new MediaTileVObj(media));
                }
            });
        }

        private void DisplayModeButton_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(MainContentPresenter == null)
            {
                return;
            }

            if (DisplayModeButton.SelectedIndex == 0)
            {
                MainContentPresenter.ContentTemplate = (DataTemplate)Resources["GridViewTemplate"];
            }
            else
            {
                MainContentPresenter.ContentTemplate = (DataTemplate)Resources["ListViewTemplate"];
            }
        }

        private void MediaTileClick(object sender, ItemClickEventArgs e)
        {
            var item = e.ClickedItem;
            MediaTileVObj media = (MediaTileVObj)item;
            MediaTileEventArgs eventArgs = new MediaTileEventArgs(media);
            PersonalisedMediaTileClicked?.Invoke(this, eventArgs);
        }

        private void Page_Loaded(object sender, RoutedEventArgs args)
        {
            MainScrollViewer.Height = Window.Current.Bounds.Height - 100;
            Window.Current.SizeChanged += (s, e) =>
            {
                MainScrollViewer.Height = e.Size.Height - 100;
            };
        }

        private void MediaItemPointerEntered(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            if (sender is Grid grid)
            {
                var deleteButton = grid.FindName("DeleteButton") as Button;
                if (deleteButton != null)
                {
                    deleteButton.Visibility = Visibility.Visible;
                }
            }
        }

        private void MediaItemPointerExited(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            if (sender is Grid grid)
            {
                var deleteButton = grid.FindName("DeleteButton") as Button;
                if (deleteButton != null)
                {
                    deleteButton.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is MediaTileVObj mediaDetail)
            {
                _vm.RemovePersonalisedMedia(mediaDetail.MediaId,PersonalisedMediaType);
            }
        }

        public void RemoveMedia(long mediaId)
        {
            _ = Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                var itemToRemove = MediaList.FirstOrDefault(media => media.MediaId == mediaId);
                if (itemToRemove != null)
                {
                    MediaList.Remove(itemToRemove);
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

        private void myListButton_Click(Microsoft.UI.Xaml.Controls.SplitButton sender, Microsoft.UI.Xaml.Controls.SplitButtonClickEventArgs args)
        {

        }
    }
}
