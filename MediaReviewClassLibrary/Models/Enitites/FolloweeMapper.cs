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
        public bool IsFollow { get; set; }

        public FolloweeMapper(long userId, long followeeId, bool isFollowing)
        {
            UserId = userId;
            FolloweeId = followeeId;
            IsFollow = isFollowing;
        }

        public FolloweeMapper(long userId, long followeeId)
        {
            UserId = userId;
            FolloweeId = followeeId;
            IsFollow = false;
        }

        public FolloweeMapper()
        { }
    }
}