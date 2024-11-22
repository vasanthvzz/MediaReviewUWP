using MediaReviewClassLibrary.Models;
using MediaReviewClassLibrary.Models.Enitites;
using MediaReviewUWP.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaReviewUWP.ViewObjects
{
    public class MediaReviewVObj
    {
        public long ReviewId { get; set; }
        public long UserId { get; set; }
        public long MediaId { get; set; }
        public string UserProfilePicture {  get; set; } //Yet to do
        public string UserName { get; set; }
        public bool IsEdited { get; set; }
        public string Timestamp { get; set; }
        public string Description { get; set; }
        public bool Following { get; set; }

        public MediaReviewVObj(MediaReviewBObj review)
        {
            ReviewId = review.ReviewId;
            UserId = review.UserId;
            MediaId = review.MediaId;
            IsEdited = review.IsEdited;
            Timestamp = DateManager.RelativeToCurrent(review.Timestamp);
            Description = review.Description;
            UserName = "@"+review.UserName;
            UserProfilePicture = review.ProfileImage;
        }
    }
}
