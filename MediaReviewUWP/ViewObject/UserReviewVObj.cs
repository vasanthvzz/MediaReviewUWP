using MediaReviewClassLibrary.Models;
using MediaReviewClassLibrary.Models.Enitites;
using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace MediaReviewUWP.ViewObject
{
    public class UserReviewVObj : INotifyPropertyChanged
    {
        public long MediaId {  get; set; }
        public string MediaName { get; set; }
        public long ReviewId {  get; set; }
        public string ReviewDate {  get; set; }

        public string _description;
        //public short UserRating
        //{
        //    get => _userRating;
        //    set
        //    {
        //        if (_userRating != value)
        //        {
        //            _userRating = value;
        //            OnPropertyChanged(nameof(UserRating));
        //        }
        //    }
        //}
        public string Description {
            get => _description;
            set
            {
                if(_description != value)
                {
                    _description = value;
                    OnPropertyChanged(nameof(Description)); 
                }
            }
        }
        public string MediaImagePath { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public UserReviewVObj(Media media,Review review)
        {
            MediaId = media.MediaId;
            MediaName = media.Title;
            MediaImagePath = media.ImagePath;
            ReviewId = review.ReviewId;
            ReviewDate = review.Timestamp.ToString("DD/MMM/YY");
            Description = review.Description;
        }

        public UserReviewVObj(long mediaId,string mediaName,string imagePath,long reviewId,DateTime date,string description)
        {
            MediaId = mediaId;
            MediaName = mediaName;
            MediaImagePath = imagePath;
            ReviewId = reviewId;
            ReviewDate = date.ToString("dd-MMM-yy");
            Description = description;
        }

        public UserReviewVObj(UserReviewBObj review)
        {
            MediaId = review.MediaId;
            MediaName = review.MediaName;
            MediaImagePath = review.MediaImagePath;
            ReviewId = review.ReviewId;
            ReviewDate = review.ReviewDate.ToString("dd-MMM-yy");
            Description = review.Description;
        }

        public void UpdateFrom(UserReviewBObj review)
        {
            if (ReviewId == review.ReviewId) 
            { 
                Description = review.Description;
            }
        }
    }
}
