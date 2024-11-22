using MediaReviewClassLibrary.Models;
using MediaReviewClassLibrary.Models.Enitites;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


namespace MediaReviewUWP.View.HomePageView
{
    public sealed partial class MediaTileGrid : Page
    {
        public event EventHandler<MediaTileEventArgs> TileClicked;

        public ObservableCollection<MediaTileVObj> MediaTileList
        {
            get { return (ObservableCollection<MediaTileVObj>)GetValue(mediaTileListProperty); }
            set { SetValue(mediaTileListProperty, value); }
        }

        public static readonly DependencyProperty mediaTileListProperty =
            DependencyProperty.Register("MediaTileList", typeof(ObservableCollection<MediaTileVObj>), typeof(MediaTileGrid), new PropertyMetadata(null));

        public MediaTileGrid()
        {
            this.InitializeComponent();
            Window.Current.SizeChanged += (s, e) =>
            {
                GridScrollViewer.Height = e.Size.Height - 100;
            };
            MediaTileList = new ObservableCollection<MediaTileVObj>();
        }


        public void UpdateMedia(List<Media> mediaList)
        {
            foreach (Media media in mediaList)
            {
                MediaTileList.Add(new MediaTileVObj(media));
            }
        }

        private void MediaGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var item = e.ClickedItem; //This returns mediaTileVObj
            MediaTileVObj media = (MediaTileVObj)item;
            MediaTileEventArgs eventArgs = new MediaTileEventArgs(media);
            TileClicked?.Invoke(this, eventArgs);
        }
    }

    public class MediaTileEventArgs : EventArgs
    {
        public MediaTileVObj Media { get; }

        public MediaTileEventArgs(MediaTileVObj media)
        {
            Media = media;
        }
    }
}