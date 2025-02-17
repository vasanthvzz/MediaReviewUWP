using MediaReviewClassLibrary.Models.Constants;
using MediaReviewClassLibrary.Models.Enitites;

namespace MediaReviewClassLibrary.Models
{
    public class UserFollowBObj
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string ProfilePicture { get; set; }
        public bool IsFollow { get; set; }
        public FollowType FollowType { get; set; }

        public UserFollowBObj(long userId, string userName, string profilePicture, bool following,FollowType followType)
        {
            UserId = userId;
            UserName = userName;
            ProfilePicture = profilePicture;
            IsFollow = following;
            FollowType = followType;
        }

        public UserFollowBObj(UserDetail user, FolloweeMapper follow,FollowType followType)
        {
            UserId = user.UserId;
            UserName = user.UserName;
            ProfilePicture = user.ProfilePicture;
            IsFollow = follow.IsFollow;
            FollowType = followType;
        }
    }
}