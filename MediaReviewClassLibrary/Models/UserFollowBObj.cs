using MediaReviewClassLibrary.Models.Enitites;

namespace MediaReviewClassLibrary.Models
{
    public class UserFollowBObj
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string ProfilePicture { get; set; }
        public bool Following { get; set; }

        public UserFollowBObj(long userId, string userName, string profilePicture, bool following)
        {
            UserId = userId;
            UserName = userName;
            ProfilePicture = profilePicture;
            Following = following;
        }

        public UserFollowBObj(UserDetail user, FolloweeMapper follow)
        {
            UserId = user.UserId;
            UserName = user.UserName;
            ProfilePicture = user.ProfilePicture;
            Following = follow.IsFollowing;
        }
    }
}
