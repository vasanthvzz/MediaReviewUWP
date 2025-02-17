using CommonClassLibrary;
using MediaReviewClassLibrary.Models;
using MediaReviewClassLibrary.Models.Enitites;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediaReviewClassLibrary.Domain
{
    public class GetMediaReviewUseCase : UseCaseBase<GetMediaReviewResponse>
    {
        private GetMediaReviewRequest _request;
        private IGetMediaReviewDataManager _dm = MediaReviewDIServiceProvider.GetRequiredService<IGetMediaReviewDataManager>();

        public GetMediaReviewUseCase(GetMediaReviewRequest request, ICallback<GetMediaReviewResponse> callback) : base(callback)
        {
            _request = request;
        }

        public override void Action()
        {
            _dm.GetMediaReviews(_request, new GetMediaReviewUseCaseCallback(this));
        }
    }

    public class GetMediaReviewUseCaseCallback : ICallback<GetMediaReviewResponse>
    {
        private GetMediaReviewUseCase _uc;

        public GetMediaReviewUseCaseCallback(GetMediaReviewUseCase uc)
        {
            _uc = uc;
        }

        public void OnFailure(Exception exception)
        {
            _uc?.PresenterCallback?.OnFailure(exception);
        }

        public void OnSuccess(ZResponse<GetMediaReviewResponse> response)
        {
            _uc?.PresenterCallback?.OnSuccess(response);
        }
    }

    public class GetMediaReviewResponse
    {
        public List<MediaReviewBObj> MediaReviews { get; set; }

        public GetMediaReviewResponse(List<MediaReviewBObj> mediaReviews)
        {
            MediaReviews = mediaReviews;
        }
    }

    public class GetMediaReviewRequest
    {
        public long MediaId { get; set; }
        public long UserId { get; set; }

        public GetMediaReviewRequest(long mediaId, long userId)
        {
            MediaId = mediaId;
            UserId = userId;
        }
    }

    public interface IGetMediaReviewDataManager
    {
        Task GetMediaReviews(GetMediaReviewRequest request, GetMediaReviewUseCaseCallback callback);

        Task<MediaReviewBObj> GetReviewBObj(Review review, long userId);
    }

    public interface IGetMediaReviewPresenterCallback : ICallback<GetMediaReviewResponse>
    { }
}