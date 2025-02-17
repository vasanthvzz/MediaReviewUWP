using MediaReviewClassLibrary.Models.Enitites;

namespace MediaReviewClassLibrary.Models
{
    public class UserRatingBObj
    {
        public long MediaId { get; set; }
        public string MediaName { get; set; }
        public string ImagePath { get; set; }
        public short UserRating { get; set; }

        public UserRatingBObj(Media media, short userRating)
        {
            MediaId = media.MediaId;
            MediaName = media.Title;
            ImagePath = media.ImagePath;
            UserRating = userRating;
        }
    }
}