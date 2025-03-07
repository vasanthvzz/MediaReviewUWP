﻿using CommonClassLibrary;
using MediaReviewClassLibrary.Data.DataHandler.Contract;
using MediaReviewClassLibrary.Domain;
using MediaReviewClassLibrary.Models;
using MediaReviewClassLibrary.Models.Enitites;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediaReviewClassLibrary.DataManager
{
    public class GetUserReviewDataManager : IGetUserReviewDataManager
    {
        private IMediaDataHandler _mediaDataHandler = MediaReviewDIServiceProvider.GetRequiredService<IMediaDataHandler>();
        private IReviewDataHandler _reviewDataHandler = MediaReviewDIServiceProvider.GetRequiredService<IReviewDataHandler>();

        public async Task GetUserReview(GetUserReviewRequest request, ICallback<GetUserReviewResponse> callback)
        {
            try
            {
                var reviewList = await _reviewDataHandler.GetAllUserReviews(request.UserId);
                List<UserReviewBObj> userReviewList = new List<UserReviewBObj>();

                foreach (Review review in reviewList)
                {
                    var media = await _mediaDataHandler.GetMediaById(review.MediaId);
                    if (media != null) userReviewList.Add(new UserReviewBObj(media, review));
                }
                ZResponse<GetUserReviewResponse> response = new ZResponse<GetUserReviewResponse>(new GetUserReviewResponse(userReviewList));
                callback?.OnSuccess(response);
            }
            catch (Exception e)
            {
                callback?.OnFailure(e);
            }
        }
    }
}