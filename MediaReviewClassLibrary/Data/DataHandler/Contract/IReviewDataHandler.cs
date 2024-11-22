using MediaReviewClassLibrary.Models.Enitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaReviewClassLibrary.Data.DataHandler.Contract
{
    public interface IReviewDataHandler
    {
        void AddReview(Review review);
        Task<List<Review>> GetReviewsByMedia(long mediaId);
    }
}
