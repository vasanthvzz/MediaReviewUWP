using SQLite;
using System;

namespace MediaReviewClassLibrary.Models.Enitites
{
    [Table("media")]
    public class Media
    {
        private string tileImage;
        private string posterImage;

        [PrimaryKey]
        [Column("media_id")]
        public long MediaId { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("imagepath")]
        public string ImagePath { get; set; }

        [Column("posterpath")]
        public string PosterPath { get; set; }

        [Column("homepage_url")]
        public string HomepageUrl { get; set; }

        [Column("release_date")]
        public DateTimeOffset ReleaseDate { get; set; }

        [Column("runtime")]
        public int Runtime { get; set; }

        [Column("tagline")]
        public string Tagline { get; set; }

        public Media(long mediaId, string title, string description, string imagePath, string posterPath, string homepageUrl, DateTime releaseDate, int runtime, string tagline)
        {
            MediaId = mediaId;
            Title = title;
            Description = description;
            ImagePath = imagePath;
            PosterPath = posterPath;
            HomepageUrl = homepageUrl;
            ReleaseDate = releaseDate;
            Runtime = runtime;
            Tagline = tagline;
        }

        public Media()
        { }

        public Media(long mediaId, string title, string description, string imagePath, string posterPath, string homepageUrl, DateTime releaseDate, int runtime)
        {
            MediaId = mediaId;
            Title = title;
            Description = description;
            ImagePath = imagePath;
            PosterPath = posterPath;
            HomepageUrl = homepageUrl;
            ReleaseDate = releaseDate;
            Runtime = runtime;
        }

        public Media(long mediaId, string title, string description, string tileImage, string posterImage, DateTimeOffset releaseDate, int runtime)
        {
            MediaId = mediaId;
            Title = title;
            Description = description;
            ImagePath = tileImage;
            PosterPath = posterImage;
            ReleaseDate = releaseDate;
            Runtime = runtime;
        }
    }
}