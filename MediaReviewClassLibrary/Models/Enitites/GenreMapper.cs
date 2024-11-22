using SQLite;

namespace MediaReviewClassLibrary.Models.Enitites
{
    [Table("genre_mapper")]
    public class GenreMapper
    {
        [Column("genre_id")]
        public long GenreId { get; set; }

        [Column("media_id")]
        public long MediaId { get; set; }

        public GenreMapper(int genreId, long mediaId)
        {
            GenreId = genreId;
            MediaId = mediaId;
        }

        public GenreMapper() { }
    }
}
