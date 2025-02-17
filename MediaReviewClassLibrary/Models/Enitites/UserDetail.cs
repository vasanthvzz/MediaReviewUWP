using SQLite;

namespace MediaReviewClassLibrary.Models.Enitites
{
    [Table("user")]
    public class UserDetail
    {
        [PrimaryKey]
        [Column("user_id")]
        public long UserId { get; set; }

        [Unique]
        [Column("username")]
        public string UserName { get; set; }

        [Column("profile_picture")]
        public string ProfilePicture { get; set; }

        [Column("is_admin")]
        public bool IsAdmin { get; set; }

        public UserDetail(long userId, string userName)
        {
            UserId = userId;
            UserName = userName;
            ProfilePicture = "";
        }

        public UserDetail(long userId, string userName, string profilePicture)
        {
            UserId = userId;
            UserName = userName;
            ProfilePicture = profilePicture;
        }

        public UserDetail(long userId, string userName, string profilePicture, bool isAdmin)
        {
            UserId = userId;
            UserName = userName;
            ProfilePicture = profilePicture;
            IsAdmin = isAdmin;
        }

        public UserDetail()
        { }
    }
}