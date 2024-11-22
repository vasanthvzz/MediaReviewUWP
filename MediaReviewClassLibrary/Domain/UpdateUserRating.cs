using CommonClassLibrary;
using MediaReviewClassLibrary.Models.Enitites;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MediaReviewClassLibrary.Domain
{
    public class UpdateUserRatingUseCase : UseCaseBase<UpdateUserRatingResponse>
    {
        private UpdateUserRatingRequest _request;
        private IUpdateUserRatingDataManager _dm = MediaReviewDIServiceProvider.GetServiceProvider().GetRequiredService<IUpdateUserRatingDataManager>();

        public UpdateUserRatingUseCase(UpdateUserRatingRequest request, ICallback<UpdateUserRatingResponse> callback) : base(callback)
        {
            _request = request;
        }

        public override void Action()
        {
            _dm.UpdateUserRating(_request, new UpdateUserRatingUsecaseCallback(this));
        }
    }

    public class UpdateUserRatingUsecaseCallback : ICallback<UpdateUserRatingResponse>
    {
        private UpdateUserRatingUseCase _usecase;
        
        public UpdateUserRatingUsecaseCallback(UpdateUserRatingUseCase usecase)
        {
            _usecase = usecase;
        }

        public void OnSuccess(ZResponse<UpdateUserRatingResponse> response)
        {
            _usecase?.PresenterCallback?.OnSuccess(response);
        }

        public void OnFailure(Exception exception)
        {
            _usecase?.PresenterCallback?.OnFailure(exception);
        }
    }

    public class UpdateUserRatingResponse 
    { 
        public bool Success {  get; set; }
        public Rating UserRating { get; set; }

        public UpdateUserRatingResponse(bool success, Rating userRating)
        {
            Success = success;
            UserRating = userRating;
        }
    }

    public class UpdateUserRatingRequest 
    {
        public Rating UserRating { get; set;}

        public UpdateUserRatingRequest(Rating userRating)
        {
            UserRating = userRating;
        }
    }

    public interface IUpdateUserRatingDataManager
    {
        void UpdateUserRating(UpdateUserRatingRequest request, UpdateUserRatingUsecaseCallback updateUserRatingUsecaseCallback);
    }

    public interface IUpdateUserRatingPresenterCallback : ICallback<UpdateUserRatingResponse> { }
}
