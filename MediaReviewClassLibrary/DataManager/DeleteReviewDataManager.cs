using CommonClassLibrary;
using MediaReviewClassLibrary.Data.DataHandler.Contract;
using MediaReviewClassLibrary.Domain;
using System;
using System.Threading.Tasks;

namespace MediaReviewClassLibrary.DataManager
{
    public class DeleteReviewDataManager : IDeleteReviewDataManager
    {
        private IReviewDataHandler _reviewDataHandler = MediaReviewDIServiceProvider.GetRequiredService<IReviewDataHandler>();

        public async Task DeleteReview(DeleteReviewRequest request, DeleteReviewUseCaseCallback callback)
        {
            try
            {
                var review = await _reviewDataHandler.GetReviewById(request.ReviewId);
                bool success = false;
                if (review?.UserId == request.UserId)
                {
                    await _reviewDataHandler.DeleteReview(review.ReviewId);
                    success = true;
                }
                DeleteReviewResponse response = new DeleteReviewResponse(success, review);
                ZResponse<DeleteReviewResponse> zResponse = new ZResponse<DeleteReviewResponse>(response);
                callback?.OnSuccess(zResponse);
            }
            catch (Exception e)
            {
                callback?.OnFailure(e);
            }
        }
    }
}