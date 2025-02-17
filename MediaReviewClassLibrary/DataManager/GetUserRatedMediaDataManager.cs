using CommonClassLibrary;
using MediaReviewClassLibrary.Data.DataHandler.Contract;
using MediaReviewClassLibrary.Domain;
using MediaReviewClassLibrary.Models;
using MediaReviewClassLibrary.Models.Enitites;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediaReviewClassLibrary.DataManager
{
    public class GetUserRatedMediaDataManager : IGetUserRatedMediaDataManager
    {
        private IRatingDataHandler _ratingDataHandler = MediaReviewDIServiceProvider.GetRequiredService<IRatingDataHandler>();
        private IMediaDataHandler _mediaDataHandler = MediaReviewDIServiceProvider.GetRequiredService<IMediaDataHandler>();

        public async Task GetUserRatedMedia(GetUserRatedMediaRequest request, GetUserRatedMediaUseCaseCallback callback)
        {
            try
            {
                List<Rating> userRatingList = await _ratingDataHandler.GetAllUserRating(request.UserId);
                List<UserRatingBObj> userRatings = new List<UserRatingBObj>();
                foreach (Rating rating in userRatingList)
                {
                    Media media = await _mediaDataHandler.GetMediaById(rating.MediaId);
                    if (media != null) userRatings.Add(new UserRatingBObj(media, rating.Score));
                }
                ZResponse<GetUserRatedMediaResponse> response = new ZResponse<GetUserRatedMediaResponse>(new GetUserRatedMediaResponse(userRatings));
                callback?.OnSuccess(response);
            }
            catch (Exception e)
            {
                callback?.OnFailure(e);
            }
        }
    }
}