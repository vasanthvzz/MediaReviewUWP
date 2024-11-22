using SQLite;

namespace MediaReviewClassLibrary.Models.Enitites
{
    [Table("follower_mapper")]
    public class FollowerMappper
    {
        [Column("user_id")]
        public long UserId { get; set; }

        [Column("follower_id")]
        public long FollowerId { get; set; }

        [Column("is_following")]
        public bool IsFollowing { get; set; }

        public FollowerMappper(long userId, long followerId, bool isFollowing)
        {
            UserId = userId;
            FollowerId = followerId;
            IsFollowing = isFollowing;
        }

        public FollowerMappper(long userId, long followerId)
        {
            UserId = userId;
            FollowerId = followerId;
            IsFollowing = false;
        }

        public FollowerMappper() { }
    }
}
