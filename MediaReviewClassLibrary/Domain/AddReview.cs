using CommonClassLibrary;
using MediaReviewClassLibrary.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace MediaReviewClassLibrary.Domain
{
    public class AddReviewUseCase : UseCaseBase<AddReviewResponse>
    {
        private AddReviewRequest _request;
        private IAddReviewDataManager _dm = MediaReviewDIServiceProvider.GetRequiredService<IAddReviewDataManager>();

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
        private AddReviewUseCase _uc;

        public AddReviewUseCaseCallback(AddReviewUseCase usecase)
        {
            _uc = usecase;
        }

        public void OnFailure(Exception exception)
        {
            _uc?.PresenterCallback?.OnFailure(exception);
        }

        public void OnSuccess(ZResponse<AddReviewResponse> response)
        {
            _uc?.PresenterCallback?.OnSuccess(response);
        }
    }

    public interface IAddReviewDataManager
    {
        Task AddReview(AddReviewRequest request, AddReviewUseCaseCallback callback);
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

    public interface IAddReviewPresenterCallback : ICallback<AddReviewResponse>
    { }
}