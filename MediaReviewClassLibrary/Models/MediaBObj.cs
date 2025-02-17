using MediaReviewClassLibrary.Models.Enitites;
using System;

namespace MediaReviewClassLibrary.Models
{
    public class MediaBObj : Media
    {
        public float MediaRating { get; set; }

        public MediaBObj(Media media, float mediaRating)
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