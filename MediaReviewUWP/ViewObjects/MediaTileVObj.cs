using MediaReviewClassLibrary.Models.Enitites;
using System;

namespace MediaReviewUWP.ViewObjects
{
    public class MediaTileVObj
    {
        public long MediaId { get; set; }
        public string Title { get; set; }
        public string ReleaseDate { get; set; }
        public string ImagePath { get; set; }

        public MediaTileVObj(long mediaId, string title, DateTime releaseDate, string imagePath)
        {
            MediaId = mediaId;
            Title = title;
            ReleaseDate = releaseDate.ToShortDateString();
            ImagePath = imagePath;
        }

        public MediaTileVObj(Media media)
        {
            MediaId = media.MediaId;
            Title = media.Title;
            ReleaseDate = media.ReleaseDate.ToShortDateString();
            ImagePath = media.ImagePath;
        }
    }
}
