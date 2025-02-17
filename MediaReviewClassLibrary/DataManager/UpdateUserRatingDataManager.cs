using CommonClassLibrary;
using MediaReviewClassLibrary.Data.DataHandler.Contract;
using MediaReviewClassLibrary.Domain;
using System;
using System.Threading.Tasks;

namespace MediaReviewClassLibrary.DataManager
{
    public class UpdateUserRatingDataManager : IUpdateUserRatingDataManager
    {
        private IRatingDataHandler _ratingDataHandler = MediaReviewDIServiceProvider.GetRequiredService<IRatingDataHandler>();

        public async Task UpdateUserRating(UpdateUserRatingRequest request, UpdateUserRatingUsecaseCallback callback)
        {
            try
            {
                var UserRating = await _ratingDataHandler.UpdateUserRating(request.UserRating);
                bool success = UserRating.Equals(request.UserRating);
                ZResponse<UpdateUserRatingResponse> response = new ZResponse<UpdateUserRatingResponse>(new UpdateUserRatingResponse(success, UserRating));
                callback?.OnSuccess(response);
            }
            catch (Exception e)
            {
                callback?.OnFailure(e);
            }
        }
    }
}