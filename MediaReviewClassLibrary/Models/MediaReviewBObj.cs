using MediaReviewClassLibrary.Models.Enitites;
using System;

namespace MediaReviewClassLibrary.Models
{
    public class MediaReviewBObj
    {
        public long ReviewId { get; set; }
        public long UserId { get; set; }
        public long MediaId { get; set; }
        public string UserName { get; set; }
        public string ProfileImage { get; set; }
        public bool IsEdited { get; set; }
        public DateTime Timestamp { get; set; }
        public string Description { get; set; }
        public bool Following { get; set; }
        public short UserRating { get; set; }

        public MediaReviewBObj(Review review, UserDetail user, short rating, bool following)
        {
            ReviewId = review.ReviewId;
            UserId = review.UserId;
            MediaId = review.MediaId;
            IsEdited = review.IsEdited;
            Timestamp = review.Timestamp;
            Description = review.Description;
            UserName = user.UserName;
            ProfileImage = user.ProfilePicture;
            UserRating = rating;
            Following = following;
        }
    }
}