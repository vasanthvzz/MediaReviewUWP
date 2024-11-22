using SQLite;

namespace MediaReviewClassLibrary.Models.Enitites
{
    [Table("artist_mapper")]
    public class ActorMapper
    {
        [Column("artist_id")]
        public long ArtistId { get; set; }

        [Column("media_id")]
        public long MediaId { get; set; }

        public ActorMapper(long actorId, long mediaId)
        {
            ArtistId = actorId;
            MediaId = mediaId;
        }

        public ActorMapper() { }
    }
}
