using MediaReviewClassLibrary.Models;
using MediaReviewClassLibrary.Models.Constants;
using System.ComponentModel;

namespace MediaReviewUWP.ViewObject
{
    public class UserFollowVObj : INotifyPropertyChanged
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string ProfilePicture { get; set; }

        private bool _isFollow;
        public bool IsFollow
        {
            get => _isFollow;
            set
            {
                if (_isFollow != value)
                {
                    _isFollow = value;
                    OnPropertyChanged(nameof(IsFollow));
                }
            }
        }

        public FollowType UserFollow { get; set; }

        public UserFollowVObj(long userId, string userName, string profilePicture, bool following,FollowType followType)
        {
            UserId = userId;
            UserName = userName;
            ProfilePicture = profilePicture;
            IsFollow = following;
            UserFollow = followType;
        }

        public UserFollowVObj(UserFollowBObj userFollow)
        {
            UserId = userFollow.UserId;
            UserName = userFollow.UserName;
            ProfilePicture = userFollow.ProfilePicture;
            IsFollow = userFollow.IsFollow;
            UserFollow = userFollow.FollowType;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}