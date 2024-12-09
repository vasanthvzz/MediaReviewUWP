namespace MediaReviewUWP.ViewObject
{
    public class UserFollowVObj
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string ProfilePicture {  get; set; }
        public string Following {  get; set; }

        public UserFollowVObj(long userId, string userName, string profilePicture, string following)
        {
            UserId = userId;
            UserName = userName;
            ProfilePicture = profilePicture;
            Following = following;
        }
    }
}
