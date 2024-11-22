using MediaReviewClassLibrary.Models.Enitites;

namespace MediaReviewClassLibrary.Models
{
    public class MediaDetailBObj
    {
        public long UserId { get; set; }
        public Media Media { get; set; }
        public PersonalMedia UserPersonalMedia { get; set; }
        public Rating UserRating { get; set; }
        public float TotalRating {  get; set; }
        public int RatedUserCount {  get; set; }

        public MediaDetailBObj(long userId, Media media, PersonalMedia personalMedia,Rating userRating)
        {
            UserId = userId;
            Media = media;
            UserPersonalMedia = personalMedia;
            UserRating = userRating;
        }
    }
}
