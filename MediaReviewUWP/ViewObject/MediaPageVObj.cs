using MediaReviewClassLibrary.Models;
using System.ComponentModel;

namespace MediaReviewUWP.ViewObject
{
    public class MediaPageVObj : INotifyPropertyChanged
    {
        public long MediaId { get; set; }

        private string _posterPath;

        public string PosterPath
        {
            get => _posterPath;
            set
            {
                if (_posterPath != value)
                {
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
            PosterPath = mediaDetailBObj.MediaDetail.PosterPath;
        }

        public MediaPageVObj(long mediaId)
        {
            MediaId = mediaId;
        }
    }
}