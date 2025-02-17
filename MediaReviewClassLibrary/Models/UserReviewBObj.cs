using MediaReviewClassLibrary.Models.Enitites;
using System;

namespace MediaReviewClassLibrary.Models
{
    public class UserReviewBObj
    {
        public long MediaId { get; set; }
        public string MediaName { get; set; }
        public long ReviewId { get; set; }
        public DateTime ReviewDate { get; set; }
        public string Description { get; set; }
        public string MediaImagePath { get; set; }

        public UserReviewBObj(Media media, Review review)
        {
            MediaId = media.MediaId;
            MediaName = media.Title;
            MediaImagePath = media.ImagePath;
            ReviewId = review.ReviewId;
            ReviewDate = review.Timestamp;
            Description = review.Description;
        }
    }
}