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
        private IReviewDataHandler _reviewDataHandler = MediaReviewDIServiceProvider.GetServiceProvider().GetRequiredService<IReviewDataHandler>(); 
        private IUserDataHandler _userDataHandler = MediaReviewDIServiceProvider.GetServiceProvider().GetRequiredService<IUserDataHandler>();
        
        public async Task<MediaReviewBObj> GetReviewBObj(Review review)
        {
            var user = await _userDataHandler.GetUserById(review.UserId);
            return new MediaReviewBObj(review, user);
        }

        public async void GetMediaReviews(GetMediaReviewRequest request, GetMediaReviewUseCaseCallback callback)
        {
            try
            {
                var reviewList = _reviewDataHandler.GetReviewsByMedia(request.MediaId).Result;
                List<MediaReviewBObj> mediaReview = new List<MediaReviewBObj>();
                foreach (var review in reviewList) 
                {
                    var user = await _userDataHandler.GetUserById(review.UserId);
                    mediaReview.Add(new MediaReviewBObj(review, user));
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
