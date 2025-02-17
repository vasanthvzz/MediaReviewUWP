using MediaReviewClassLibrary.Models;
using MediaReviewUWP.Utility;
using System;
using System.ComponentModel;

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

        public DateTime ReleaseDate { get; set; }
        private string _imagePath;

        public string ImagePath
        {
            get => _imagePath;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    _imagePath = ImageManager.GetDefaultTileImagePath();
                }
                else
                {
                    _imagePath = value;
                }
            }
        }

        public string Description { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public MediaTileVObj(MediaBObj media)
        {
            MediaId = media.MediaId;
            Title = media.Title;
            ReleaseDate = media.ReleaseDate.Date;
            ImagePath = string.IsNullOrWhiteSpace(media.ImagePath) ? ImageManager.GetDefaultTileImagePath() : media.ImagePath;
            Description = media.Description;
            MediaRating = media.MediaRating == 0 ? "" : (media.MediaRating % 1 == 0 ? $"{(int)media.MediaRating}" : $"{media.MediaRating:F1}") + "/5";
        }

        public MediaTileVObj(MediaDetailBObj media)
        {
            MediaId = media.MediaId;
            Title = media.MediaDetail.Title;
            ReleaseDate = media.MediaDetail.ReleaseDate.Date;
            ImagePath = string.IsNullOrWhiteSpace(media.MediaDetail.ImagePath) ? ImageManager.GetDefaultTileImagePath() : media.MediaDetail.ImagePath;
            Description = media.MediaDetail.Description;
            MediaRating = media.MediaRating == 0 ? "" : (media.MediaRating % 1 == 0 ? $"{(int)media.MediaRating}" : $"{media.MediaRating:F1}") + "/5";
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void UpdateFrom(MediaBObj newMedia)
        {
            if (MediaId == newMedia.MediaId)
            {
                Title = newMedia.Title;
                ReleaseDate = newMedia.ReleaseDate.Date;
                ImagePath = string.IsNullOrWhiteSpace(newMedia.ImagePath) ? ImageManager.GetDefaultTileImagePath() : newMedia.ImagePath;
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