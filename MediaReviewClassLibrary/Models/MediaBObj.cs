using MediaReviewClassLibrary.Models.Enitites;
using System;

namespace MediaReviewClassLibrary.Models
{
    public class MediaBObj
    {
        public long MediaId { get; set; }
        public string Title { get; set; }
        public float MediaRating { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }

        public MediaBObj(Media media , float mediaRating)
        {
            MediaId = media.MediaId;
            Title = media.Title;
            MediaRating = mediaRating;
            ReleaseDate = media.ReleaseDate;
            ImagePath = media.ImagePath;
            Description = media.Description;
        }
    }
}
