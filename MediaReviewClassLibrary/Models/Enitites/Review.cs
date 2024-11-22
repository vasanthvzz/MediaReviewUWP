using SQLite;
using System;

namespace MediaReviewClassLibrary.Models.Enitites
{
    [Table("review")]
    public class Review
    {
        [PrimaryKey]
        [Column("review_id")]
        public long ReviewId { get; set; }

        [Column("user_id")]
        public long UserId { get; set; }

        [Column("media_id")]
        public long MediaId { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("timestamp")]
        public DateTime Timestamp { get; set; }

        [Column("is_edited")]
        public bool IsEdited { get; set; }

        public Review(long reviewId, long userId, long mediaId, string description, DateTime timestamp)
        {
            ReviewId = reviewId;
            UserId = userId;
            MediaId = mediaId;
            Description = description;
            Timestamp = timestamp;
        }

        public Review() { }
    }
}
