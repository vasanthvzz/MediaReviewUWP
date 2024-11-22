using SQLite;

namespace MediaReviewClassLibrary.Models.Enitites
{
    [Table("genre")]
    public class Genre
    {
        [PrimaryKey]
        [Column("genre_id")]
        public int GenreId { get; set; }

        [Unique]
        [Column("genre_name")]
        public string GenreName { get; set; }

        public Genre(int tagId, string tagName)
        {
            GenreId = tagId;
            GenreName = tagName;
        }

        public Genre() { }
    }
}
