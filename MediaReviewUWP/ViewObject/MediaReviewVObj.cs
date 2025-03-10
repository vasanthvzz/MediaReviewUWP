﻿using MediaReviewClassLibrary.Models;
using MediaReviewUWP.Components;
using MediaReviewUWP.Utility;
using System.ComponentModel;
using static MediaReviewUWP.Components.UserProfilePicturePresenter;

namespace MediaReviewUWP.ViewObject
{
    public class MediaReviewVObj : INotifyPropertyChanged
    {
        public long ReviewId { get; set; }
        public long UserId { get; set; }
        public long MediaId { get; set; }
        public UserDTBObj UserDT { get; set; }
        public string ProfilePicture { get; set; }
        public string UserName { get; set; }
        public string Timestamp { get; set; }

        private bool _isEdited;

        public bool IsEdited
        {
            get => _isEdited;
            set
            {
                if (_isEdited != value)
                {
                    _isEdited = value;
                    OnPropertyChanged(nameof(IsEdited));
                }
            }
        }

        private bool _following;

        public bool Following
        {
            get => _following;
            set
            {
                if (_following != value)
                {
                    _following = value;
                    OnPropertyChanged(nameof(Following));
                }
            }
        }

        private string _description;

        public string Description
        {
            get => _description;
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged(nameof(Description));
                }
            }
        }

        private short _userRating;

        public short UserRating
        {
            get => _userRating;
            set
            {
                if (_userRating != value)
                {
                    _userRating = value;
                    OnPropertyChanged(nameof(UserRating));
                }
            }
        }

        public MediaReviewVObj(MediaReviewBObj review)
        {
            ReviewId = review.ReviewId;
            UserId = review.UserId;
            MediaId = review.MediaId;
            IsEdited = review.IsEdited;
            Timestamp = DateManager.GetDateRelativeToCurrent(review.Timestamp);
            Description = review.Description;
            UserName = review.UserName;
            ProfilePicture = review.ProfileImage;
            UserRating = review.UserRating;
            Following = review.Following;
            UserDT = new UserDTBObj(UserName, ProfilePicture);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void UpdateFrom(MediaReviewBObj review)
        {
            if (ReviewId == review.ReviewId)
            {
                Description = review.Description;
                Following = review.Following;
            }
        }
    }
}