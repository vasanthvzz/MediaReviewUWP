using CommonClassLibrary;
using MediaReviewClassLibrary.Data;
using MediaReviewClassLibrary.Data.DataHandler.Contract;
using MediaReviewClassLibrary.Domain;
using MediaReviewClassLibrary.Models;
using MediaReviewClassLibrary.Models.Enitites;
using MediaReviewClassLibrary.Utlis;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaReviewClassLibrary.DataManager
{
    public class AddReviewDataManager : IAddReviewDataManager
    {
        private IReviewDataHandler _dataHandler = MediaReviewDIServiceProvider.GetServiceProvider().GetRequiredService<IReviewDataHandler>();
        private IGetMediaReviewDataManager getMediaReviewDataManager = MediaReviewDIServiceProvider.GetServiceProvider().GetRequiredService<IGetMediaReviewDataManager>();

        public async void AddReview(AddReviewRequest request, AddReviewUseCaseCallback callback)
        {
            try
            {
                long reviewId = IdentityManager.GenerateUniqueId();
                Review review = new Review(reviewId, request.UserId, request.MediaId, request.Description, DateTime.Now);
                _dataHandler.AddReview(review);
                MediaReviewBObj mediaReview = await getMediaReviewDataManager.GetReviewBObj(review);
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
