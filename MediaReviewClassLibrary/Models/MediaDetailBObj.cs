using MediaReviewClassLibrary.Models.Enitites;
using System.Collections.Generic;

namespace MediaReviewClassLibrary.Models
{
    public class MediaDetailBObj
    {
        public long MediaId {  get; set; }
        public long UserId { get; set; }
        public Media MediaDetail { get; set; }
        public Rating UserRating { get; set; }
        public PersonalMedia UserPersonalMedia { get; set; }
        public List<Genre> Genres { get; set; }
        public float MediaRating { get; set; }
        public long RatedUsers { get; set; }

        public MediaDetailBObj(long mediaId,long userId, Media mediaDetail, Rating userRating, PersonalMedia userPersonalMedia, List<Genre> genres, float mediaRating, long ratedUsers)
        {
            MediaId = mediaId;
            UserId = userId;
            MediaDetail = mediaDetail;
            UserRating = userRating;
            UserPersonalMedia = userPersonalMedia;
            Genres = genres;
            MediaRating = mediaRating;
            RatedUsers = ratedUsers;
        }
    }
}