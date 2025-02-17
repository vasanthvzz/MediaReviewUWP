using MediaReviewClassLibrary.Models;
using MediaReviewUWP.Utility;
using System.ComponentModel;

namespace MediaReviewUWP.ViewObject
{
    public class UserRatingVObj : INotifyPropertyChanged
    {
        public long MediaId { get; set; }
        public string MediaName { get; set; }
        public string ImagePath { get; set; }
        private short _userRating;

        public short UserRating
        {
            get => _userRating;
            set
            {
                _userRating = value;
                OnPropertyChanged(nameof(UserRating));
            }
        }

        public UserRatingVObj(long mediaId, string mediaName, string imagePath, short userRating)
        {
            MediaId = mediaId;
            MediaName = mediaName;
            ImagePath = string.IsNullOrWhiteSpace(imagePath) ? ImageManager.GetDefaultTileImagePath() : imagePath;
            ImagePath = imagePath;
            UserRating = userRating;
        }

        public UserRatingVObj(UserRatingBObj rating)
        {
            MediaId = rating.MediaId;
            MediaName = rating.MediaName;
            ImagePath = string.IsNullOrWhiteSpace(rating.ImagePath) ? ImageManager.GetDefaultTileImagePath() : rating.ImagePath;
            UserRating = rating.UserRating;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}