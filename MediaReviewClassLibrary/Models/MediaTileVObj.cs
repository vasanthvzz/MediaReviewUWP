using MediaReviewClassLibrary.Models.Enitites;
using MediaReviewClassLibrary.Utlis;
using System;

namespace MediaReviewClassLibrary.Models
{
    public class MediaTileVObj
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string ReleaseDate { get; set; }
        public string ImagePath { get; set; }

        public MediaTileVObj(long id, string title, DateTime releaseDate, string imagePath)
        {
            Id = id;
            Title = title;
            ReleaseDate = releaseDate.ToShortDateString();
            ImagePath = imagePath;
        }

        public MediaTileVObj(Media media)
        {
            Id = media.MediaId;
            Title = media.Title;
            ReleaseDate = media.ReleaseDate.ToShortDateString();
            ImagePath = media.ImagePath;
        }
    }
}
