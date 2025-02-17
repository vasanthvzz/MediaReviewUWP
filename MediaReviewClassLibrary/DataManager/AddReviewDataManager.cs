using CommonClassLibrary;
using MediaReviewClassLibrary.Data.DataHandler.Contract;
using MediaReviewClassLibrary.Domain;
using MediaReviewClassLibrary.Models;
using MediaReviewClassLibrary.Models.Enitites;
using MediaReviewClassLibrary.Utility;
using System;
using System.Threading.Tasks;

namespace MediaReviewClassLibrary.DataManager
{
    public class AddReviewDataManager : IAddReviewDataManager
    {
        private IReviewDataHandler _dataHandler = MediaReviewDIServiceProvider.GetRequiredService<IReviewDataHandler>();
        private IGetMediaReviewDataManager getMediaReviewDataManager = MediaReviewDIServiceProvider.GetRequiredService<IGetMediaReviewDataManager>();

        public async Task AddReview(AddReviewRequest request, AddReviewUseCaseCallback callback)
        {
            try
            {
                long reviewId = IdentityManager.GenerateUniqueId();
                Review review = new Review(reviewId, request.UserId, request.MediaId, request.Description, DateTime.Now);
                _dataHandler.AddReview(review);
                MediaReviewBObj mediaReview = await getMediaReviewDataManager.GetReviewBObj(review, request.UserId);
                AddReviewResponse response = new AddReviewResponse(true, mediaReview);
                ZResponse<AddReviewResponse> zResponse = new ZResponse<AddReviewResponse>(response);
                callback?.OnSuccess(zResponse);
            }
            catch (Exception e)
            {
                callback?.OnFailure(e);
            }
        }
    }
}