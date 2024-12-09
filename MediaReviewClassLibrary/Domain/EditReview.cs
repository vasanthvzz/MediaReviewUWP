using CommonClassLibrary;
using MediaReviewClassLibrary.Models.Enitites;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MediaReviewClassLibrary.Domain
{
    public class EditReviewUseCase : UseCaseBase<EditReviewResponse>
    {
        private EditReviewRequest _request;
        private IEditReviewDataManager _dataManager = MediaReviewDIServiceProvider.GetRequiredService<IEditReviewDataManager>();
        
        public EditReviewUseCase(EditReviewRequest request, ICallback<EditReviewResponse> callback) : base(callback)
        {
            _request = request;
        }

        public override void Action()
        {
            _dataManager.EditReview(_request, new EditReviewUseCaseCallback(this));
        }
    }

    public class EditReviewUseCaseCallback : ICallback<EditReviewResponse>
    {
        private EditReviewUseCase _uc;
        public EditReviewUseCaseCallback(EditReviewUseCase uc)
        {
            _uc = uc;
        }

        public void OnFailure(Exception exception)
        {
            _uc.PresenterCallback?.OnFailure(exception);
        }

        public void OnSuccess(ZResponse<EditReviewResponse> response)
        {
            _uc?.PresenterCallback?.OnSuccess(response);
        }
    }

    public class EditReviewResponse
    {
        public Review UpdatedReview { get; set; }
        public EditReviewResponse(Review review)
        {
            UpdatedReview = review;
        }
    }

    public class EditReviewRequest
    {
        public long ReviewId { get; set; }
        public long UserId {  get; set; }
        public string ReviewContent { get; set; }

        public EditReviewRequest(long reviewId,long userId, string content)
        {
            ReviewId = reviewId;
            UserId = userId;
            ReviewContent = content;
        }
    }
    
    public interface IEditReviewDataManager 
    {
        void EditReview(EditReviewRequest request , ICallback<EditReviewResponse> callback);    
    }

    public interface IEditReviewPresenterCallback : ICallback<EditReviewResponse> { }
}
