using SQLite;

namespace MediaReviewClassLibrary.Models.Enitites
{
    [Table("artist")]
    public class Artist
    {
        [PrimaryKey]
        [Column("artist_id")]
        public long ArtistId { get; set; }

        [Column("artist_name")]
        public string ArtistName { get; set; }

        public Artist(long artistId, string artistName)
        {
            ArtistId = artistId;
            ArtistName = artistName;
        }

        public Artist() { }
    }
}
