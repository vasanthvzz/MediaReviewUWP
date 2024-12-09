using MediaReviewClassLibrary.Models;
using System.ComponentModel;
using Windows.Media.MediaProperties;

namespace MediaReviewUWP.ViewObject
{
    public class MediaTileVObj : INotifyPropertyChanged
    {
        public long MediaId { get; set; }
        public string Title { get; set; }

        private string _mediaRating;
        public string MediaRating
        {
            get { return _mediaRating; }
            set
            {
                if (_mediaRating != value)
                {
                    _mediaRating = value;
                    OnPropertyChanged(nameof(MediaRating));
                }
            }
        }

        public string ReleaseDate { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public MediaTileVObj(MediaBObj media)
        {
            MediaId = media.MediaId;
            Title = media.Title;
            ReleaseDate = media.ReleaseDate.ToShortDateString();
            ImagePath = media.ImagePath;
            Description = media.Description;
            MediaRating = media.MediaRating == 0 ? "" : (media.MediaRating % 1 == 0 ? $"{(int)media.MediaRating}": $"{media.MediaRating:F1}")  + "/5";
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void UpdateFrom(MediaBObj newMedia)
        {
            if(MediaId == newMedia.MediaId)
            {
                Title = newMedia.Title;
                ReleaseDate = newMedia.ReleaseDate.ToShortDateString();
                ImagePath = newMedia.ImagePath;
                Description = newMedia.Description;
                UpdateMediaRating(newMedia.MediaRating);
            }
        }

        public void UpdateMediaRating(float mediaRating)
        {
            MediaRating = mediaRating == 0 ? "" : (mediaRating % 1 == 0 ? $"{(int)mediaRating}" : $"{mediaRating:F1}") + "/5";
        }
    }
}
