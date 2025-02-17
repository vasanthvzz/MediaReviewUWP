namespace MediaReviewUWP.ViewObject
{
    public class UserVObj
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string ProfilePicture { get; set; }
        public bool IsAdmin { get; set; }

        public UserVObj(long userId, string userName, string profilePicture, bool isAdmin)
        {
            UserId = userId;
            UserName = userName;
            ProfilePicture = profilePicture;
            IsAdmin = isAdmin;
        }
    }
}