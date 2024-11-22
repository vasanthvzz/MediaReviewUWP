using CommonClassLibrary;
using MediaReviewClassLibrary.Models;
using MediaReviewClassLibrary.Models.Enitites;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MediaReviewClassLibrary.Domain
{
    public class AddReviewUseCase : UseCaseBase<AddReviewResponse>
    {
        private AddReviewRequest _request;
        private IAddReviewDataManager _dm = MediaReviewDIServiceProvider.GetServiceProvider().GetRequiredService<IAddReviewDataManager>();

        public AddReviewUseCase(AddReviewRequest request, ICallback<AddReviewResponse> callback) : base(callback)
        {
            _request = request;
        }

        public override void Action()
        {
            _dm.AddReview(_request, new AddReviewUseCaseCallback(this));   
        }
    }

    public class AddReviewUseCaseCallback : ICallback<AddReviewResponse>
    {
        private AddReviewUseCase _usecase;

        public AddReviewUseCaseCallback(AddReviewUseCase usecase)
        {
            _usecase = usecase;
        }

        public void OnFailure(Exception exception)
        {
            _usecase?.PresenterCallback?.OnFailure(exception);
        }

        public void OnSuccess(ZResponse<AddReviewResponse> response)
        {
            _usecase?.PresenterCallback?.OnSuccess(response);
        }
    }

    public interface IAddReviewDataManager 
    {
        void AddReview(AddReviewRequest request, AddReviewUseCaseCallback callback);
    }

    public class AddReviewRequest 
    {
        public long UserId { get; set; }
        public long MediaId { get; set; }
        public string Description { get; set; }
        public AddReviewRequest(long userId, long mediaId, string description)
        {
            UserId = userId;
            MediaId = mediaId;
            Description = description;
        }
    }

    public class AddReviewResponse
    {
        public bool IsSuccess { get; set; }
        public MediaReviewBObj UserReview { get; set; }

        public AddReviewResponse(bool isSuccess, MediaReviewBObj userReview)
        {
            IsSuccess = isSuccess;
            UserReview = userReview;
        }
    }

    public interface IAddReviewPresenterCallback : ICallback<AddReviewResponse>  { }
}
