using SQLite;

namespace MediaReviewClassLibrary.Models.Enitites
{
    [Table("personal_media")]
    public class PersonalMedia
    {
        [Column("user_id")]
        public long UserId { get; set; }

        [Column("media_id")]
        public long MediaId { get; set; }

        [Column("is_favourite")]
        public bool IsFavourite { get; set; }

        [Column("has_watched")]
        public bool HasWatched { get; set; }

        [Column("watchlist")]
        public bool WatchList { get; set; }

        public PersonalMedia(long userId, long mediaId)
        {
            UserId = userId;
            MediaId = mediaId;
        }

        public PersonalMedia(long userId, long mediaId, bool isFavourite, bool watchList, bool hasWatched)
        {
            UserId = userId;
            MediaId = mediaId;
            IsFavourite = isFavourite;
            WatchList = watchList;
            HasWatched = hasWatched;
        }

        public PersonalMedia()
        { }
    }
}