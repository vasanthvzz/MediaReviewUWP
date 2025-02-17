using CommonClassLibrary;
using MediaReviewClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediaReviewClassLibrary.Domain
{
    public class GetUserReviewUseCase : UseCaseBase<GetUserReviewResponse>
    {
        private GetUserReviewRequest _request;
        private IGetUserReviewDataManager _dm = MediaReviewDIServiceProvider.GetRequiredService<IGetUserReviewDataManager>();

        public GetUserReviewUseCase(GetUserReviewRequest request, ICallback<GetUserReviewResponse> callback) : base(callback)
        {
            _request = request;
        }

        public override void Action()
        {
            _dm.GetUserReview(_request, new GetUserReviewUseCaseCallback(this));
        }
    }

    public class GetUserReviewUseCaseCallback : ICallback<GetUserReviewResponse>
    {
        private GetUserReviewUseCase _uc;

        public GetUserReviewUseCaseCallback(GetUserReviewUseCase uc)
        {
            _uc = uc;
        }

        public void OnFailure(Exception exception)
        {
            _uc?.PresenterCallback?.OnFailure(exception);
        }

        public void OnSuccess(ZResponse<GetUserReviewResponse> response)
        {
            _uc?.PresenterCallback?.OnSuccess(response);
        }
    }

    public class GetUserReviewRequest
    {
        public long UserId { get; set; }

        public GetUserReviewRequest(long userId)
        {
            UserId = userId;
        }
    }

    public class GetUserReviewResponse
    {
        public List<UserReviewBObj> UserReviews { get; set; }

        public GetUserReviewResponse(List<UserReviewBObj> userReviews)
        {
            UserReviews = userReviews;
        }
    }

    public interface IGetUserReviewDataManager
    {
        Task GetUserReview(GetUserReviewRequest request, ICallback<GetUserReviewResponse> callback);
    }

    public interface IGetUserReviewPresenterCallback : ICallback<GetUserReviewResponse>
    { }
}