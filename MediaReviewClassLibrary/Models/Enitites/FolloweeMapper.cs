using SQLite;

namespace MediaReviewClassLibrary.Models.Enitites
{
    [Table("followee_mapper")]
    public class FolloweeMapper
    {
        [Column("user_id")]
        public long UserId { get; set; }

        [Column("followee_id")]
        public long FolloweeId { get; set; }

        [Column("is_following")]
        public bool IsFollowing { get; set; }

        public FolloweeMapper(long userId, long followeeId, bool isFollowing)
        {
            UserId = userId;
            FolloweeId = followeeId;
            IsFollowing = isFollowing;
        }

        public FolloweeMapper(long userId, long followeeId)
        {
            UserId = userId;
            FolloweeId = followeeId;
            IsFollowing = false;
        }

        public FolloweeMapper() { }
    }
}
