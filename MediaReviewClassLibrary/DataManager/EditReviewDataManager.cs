﻿using CommonClassLibrary;
using MediaReviewClassLibrary.Data.DataHandler.Contract;
using MediaReviewClassLibrary.Domain;
using MediaReviewClassLibrary.Models.Enitites;
using System;
using System.Threading.Tasks;

namespace MediaReviewClassLibrary.DataManager
{
    public class EditReviewDataManager : IEditReviewDataManager
    {
        private IReviewDataHandler _reviewDataHandler = MediaReviewDIServiceProvider.GetRequiredService<IReviewDataHandler>();

        public async Task EditReview(EditReviewRequest request, ICallback<EditReviewResponse> callback)
        {
            try
            {
                Review review = await _reviewDataHandler.GetReviewById(request.ReviewId);
                if (review.UserId == request.UserId)
                {
                    review = await _reviewDataHandler.UpdateReview(request.ReviewId, request.ReviewContent.Trim());
                }
                EditReviewResponse response = new EditReviewResponse(review);
                ZResponse<EditReviewResponse> zResponse = new ZResponse<EditReviewResponse>(response);
                callback?.OnSuccess(zResponse);
            }
            catch (Exception e)
            {
                callback?.OnFailure(e);
            }
        }
    }
}