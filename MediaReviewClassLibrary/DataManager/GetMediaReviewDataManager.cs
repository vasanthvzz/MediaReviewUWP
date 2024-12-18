using CommonClassLibrary;
using MediaReviewClassLibrary.Data.DataHandler.Contract;
using MediaReviewClassLibrary.Domain;
using MediaReviewClassLibrary.Models;
using MediaReviewClassLibrary.Models.Enitites;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediaReviewClassLibrary.DataManager
{
    public class GetMediaReviewDataManager : IGetMediaReviewDataManager
    {
        private IReviewDataHandler _reviewDataHandler = MediaReviewDIServiceProvider.GetRequiredService<IReviewDataHandler>(); 
        private IUserDataHandler _userDataHandler = MediaReviewDIServiceProvider.GetRequiredService<IUserDataHandler>();
        private IRatingDataHandler _ratingDataHandler = MediaReviewDIServiceProvider.GetRequiredService<IRatingDataHandler>();
        private IUpdateFollowDataManager _followDataManager = MediaReviewDIServiceProvider.GetRequiredService<IUpdateFollowDataManager>();

        public async Task<MediaReviewBObj> GetReviewBObj(Review review,long userId)
        {
            var reviewedUser = await _userDataHandler.GetUserById(review.UserId);
            var rating = await _ratingDataHandler.GetUserRating(review.UserId,review.MediaId);
            bool following = await _followDataManager.GetFollowingStatus(userId,reviewedUser.UserId);
            return new MediaReviewBObj(review, reviewedUser,rating.Score,following);
        }

        public async Task GetMediaReviews(GetMediaReviewRequest request, GetMediaReviewUseCaseCallback callback)
        {
            try
            {
                var reviewList = await _reviewDataHandler.GetReviewsByMedia(request.MediaId);
                List<MediaReviewBObj> mediaReview = new List<MediaReviewBObj>();
                foreach (var review in reviewList) 
                {
                    mediaReview.Add(await GetReviewBObj(review,request.UserId));
                }
                GetMediaReviewResponse response = new GetMediaReviewResponse(mediaReview);
                ZResponse<GetMediaReviewResponse> zResponse = new ZResponse<GetMediaReviewResponse>(response);
                callback?.OnSuccess(zResponse);
            }
            catch(Exception e)
            {
                callback?.OnFailure(e);
            }
        }
    }
}
