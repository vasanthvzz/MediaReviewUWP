using MediaReviewClassLibrary.Models.Enitites;
using System.Collections.Generic;

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
        public List<Genre> GenreList { get; set; }

        public MediaDetailBObj(long userId, Media media, PersonalMedia personalMedia,Rating userRating, List<Genre> genres)
        {
            UserId = userId;
            Media = media;
            UserPersonalMedia = personalMedia;
            UserRating = userRating;
            GenreList = genres;
        }
    }
}
