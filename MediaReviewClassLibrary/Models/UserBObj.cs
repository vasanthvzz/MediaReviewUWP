namespace MediaReviewClassLibrary.Models
{
    public class UserBObj
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string ProfilePicture { get; set; }
        public bool IsAdmin { get; set; }

        public UserBObj(long userId, string userName, string profilePicture, bool isAdmin)
        {
            UserId = userId;
            UserName = userName;
            ProfilePicture = profilePicture;
            IsAdmin = isAdmin;
        }
    }
}