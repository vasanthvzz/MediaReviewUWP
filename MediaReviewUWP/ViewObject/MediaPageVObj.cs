using MediaReviewClassLibrary.Models;
using System;
using System.ComponentModel;
using Windows.UI.Xaml.Media.Imaging;

namespace MediaReviewUWP.ViewObject
{
    public class MediaPageVObj : INotifyPropertyChanged
    {
        public long MediaId {  get; set; }

        private BitmapImage _posterPath;
        public BitmapImage PosterPath
        {
            get =>  _posterPath;
            set
            {
                if (_posterPath != value) { 
                _posterPath = value;
                OnPropertyChanged(nameof(PosterPath));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void UpdateFrom(MediaDetailBObj mediaDetailBObj)
        {
            if(mediaDetailBObj.MediaDetail.PosterPath != null)
            {
                PosterPath = new BitmapImage(new Uri(mediaDetailBObj.MediaDetail.PosterPath, UriKind.Absolute));
            }
        }

        public MediaPageVObj(long mediaId)
        {
            MediaId = mediaId;
            PosterPath = new BitmapImage();
        }
    }
}
