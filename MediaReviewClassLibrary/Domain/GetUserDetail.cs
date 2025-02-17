using CommonClassLibrary;
using MediaReviewClassLibrary.Models.Enitites;
using System;
using System.Threading.Tasks;

namespace MediaReviewClassLibrary.Domain
{
    public class GetUserDetailUseCase : UseCaseBase<GetUserDetailResponse>
    {
        private GetUserDetailRequest _request;
        private IGetUserDetailDataManager _dm = MediaReviewDIServiceProvider.GetRequiredService<IGetUserDetailDataManager>();

        public GetUserDetailUseCase(GetUserDetailRequest request, ICallback<GetUserDetailResponse> callback) : base(callback)
        {
            _request = request;
        }

        public override void Action()
        {
            _dm.GetUserDetail(_request, new GetUserDetailUseCaseCallback(this));
        }
    }

    public class GetUserDetailRequest
    {
        public long UserId { get; set; }

        public GetUserDetailRequest(long userId)
        {
            UserId = userId;
        }
    }

    public class GetUserDetailResponse
    {
        public UserDetail User { get; set; }

        public GetUserDetailResponse(UserDetail user)
        {
            User = user;
        }
    }

    public interface IGetUserDetailDataManager
    {
        Task GetUserDetail(GetUserDetailRequest request, GetUserDetailUseCaseCallback callback);
    }

    public class GetUserDetailUseCaseCallback : ICallback<GetUserDetailResponse>
    {
        private GetUserDetailUseCase _uc;

        public GetUserDetailUseCaseCallback(GetUserDetailUseCase uc)
        {
            _uc = uc;
        }

        public void OnFailure(Exception exception)
        {
            _uc?.PresenterCallback?.OnFailure(exception);
        }

        public void OnSuccess(ZResponse<GetUserDetailResponse> response)
        {
            _uc?.PresenterCallback?.OnSuccess(response);
        }
    }

    public interface IGetUserDetailPresenterCallback : ICallback<GetUserDetailResponse> { }
}
