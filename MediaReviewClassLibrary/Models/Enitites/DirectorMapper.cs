using SQLite;

namespace MediaReviewClassLibrary.Models.Enitites
{
    [Table("director_mapper")]
    public class DirectorMapper
    {
        [Column("media_id")]
        [PrimaryKey]
        public long MediaId { get; set; }

        [Column("director_id")]
        public long DirectorId { get; set; }

        public DirectorMapper(long mediaId, long directorId)
        {
            MediaId = mediaId;
            DirectorId = directorId;
        }

        public DirectorMapper() { }
    }
}

