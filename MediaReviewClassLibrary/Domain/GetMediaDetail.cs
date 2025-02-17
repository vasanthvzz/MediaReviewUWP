using CommonClassLibrary;
using MediaReviewClassLibrary.Models;
using System;
using System.Threading.Tasks;
namespace MediaReviewClassLibrary.Domain
{
    public class GetMediaDetailUseCase : UseCaseBase<GetMediaDetailResponse>
    {
        private GetMediaDetailRequest _request;
        private IGetMediaDetailDataManager _dm = MediaReviewDIServiceProvider.GetRequiredService<IGetMediaDetailDataManager>();

        public GetMediaDetailUseCase(GetMediaDetailRequest request, IGetMediaDetailPresenterCallback callback) : base(callback)
        {
            _request = request;
        }

        public override void Action()
        {
            _dm.GetMediaDetail(_request, new GetMediaDetailUseCaseCallback(this));
        }
    }

    public class GetMediaDetailRequest
    {
        public long MediaId { get; }
        public long UserId { get; }

        public GetMediaDetailRequest(long userId, long mediaId)
        {
            UserId = userId;
            MediaId = mediaId;
        }
    }

    public class GetMediaDetailResponse
    {
        public MediaDetailBObj MediaDetails { get; }

        public GetMediaDetailResponse(MediaDetailBObj mediaDetails)
        {
            MediaDetails = mediaDetails;
        }
    }

    public interface IGetMediaDetailDataManager
    {
        Task GetMediaDetail(GetMediaDetailRequest request, GetMediaDetailUseCaseCallback callback);
    }

    public class GetMediaDetailUseCaseCallback : ICallback<GetMediaDetailResponse>
    {
        private GetMediaDetailUseCase _uc;

        public GetMediaDetailUseCaseCallback(GetMediaDetailUseCase useCase)
        {
            _uc = useCase;
        }

        public void OnSuccess(ZResponse<GetMediaDetailResponse> response)
        {
            _uc?.PresenterCallback?.OnSuccess(response);
        }

        public void OnFailure(Exception exception)
        {
            _uc?.PresenterCallback?.OnFailure(exception);
        }
    }

    public interface IGetMediaDetailPresenterCallback : ICallback<GetMediaDetailResponse>
    { }
}