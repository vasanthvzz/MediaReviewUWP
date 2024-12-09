using CommonClassLibrary;
using MediaReviewClassLibrary.Data.DataHandler.Contract;
using MediaReviewClassLibrary.Domain;
using System;

namespace MediaReviewClassLibrary.DataManager
{
    public class GetMediaRatingDataManager : IGetMediaRatingDataManager
    {
        private IRatingDataHandler _ratingDataHandler = MediaReviewDIServiceProvider.GetRequiredService<IRatingDataHandler>();  

        public async void GetMediaRating(GetMediaRatingRequest request , ICallback<GetMediaRatingResponse> callback)
        {
            try
            {
                float ratingScore = await _ratingDataHandler.GetAverageRating(request.MediaId);
                long ratedUserCount = await _ratingDataHandler.GetRatedUserCount(request.MediaId);
                GetMediaRatingResponse response = new GetMediaRatingResponse(request.MediaId, ratingScore, ratedUserCount);
                ZResponse<GetMediaRatingResponse> zResponse = new ZResponse<GetMediaRatingResponse>(response);
                callback?.OnSuccess(zResponse);
            }
            catch (Exception e)
            {
                callback?.OnFailure(e);
            }
        }
    }
}
