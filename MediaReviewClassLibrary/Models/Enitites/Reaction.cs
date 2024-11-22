using MediaReviewClassLibrary.Models.Constants;
using SQLite;

namespace MediaReviewClassLibrary.Models.Enitites
{
    [Table("reaction")]
    public class Reaction
    {
        [Column("user_id")]
        public long UserId { get; set; }

        [Column("media_id")]
        public long MediaId { get; set; }

        [Column("reaction_type")]
        public ReactionType MediaReaction { get; set; }

        public Reaction(long userId, long mediaId)
        {
            UserId = userId;
            MediaId = mediaId;
            MediaReaction = ReactionType.NEUTRAL;
        }

        public Reaction(long userId, long mediaId, ReactionType reaction)
        {
            UserId = userId;
            MediaId = mediaId;
            MediaReaction = reaction;
        }

        public Reaction() { }
    }
}
