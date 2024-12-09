using CommonClassLibrary;
using MediaReviewClassLibrary.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;



namespace MediaReviewClassLibrary.Domain
{
    public class GetUserRatedMediaUseCase : UseCaseBase<GetUserRatedMediaResponse>
    {
        private GetUserRatedMediaRequest _request;
        private IGetUserRatedMediaDataManager _dm = MediaReviewDIServiceProvider.GetRequiredService<IGetUserRatedMediaDataManager>();

        public GetUserRatedMediaUseCase(GetUserRatedMediaRequest request, ICallback<GetUserRatedMediaResponse> callback) : base(callback)
        {
            _request = request;
            _dm = MediaReviewDIServiceProvider.GetServiceProvider().GetRequiredService<IGetUserRatedMediaDataManager>();
        }

        public override void Action()
        {
            _dm.GetUserRatedMedia(_request, new GetUserRatedMediaUseCaseCallback(this));
        }
    }

    public class GetUserRatedMediaUseCaseCallback : ICallback<GetUserRatedMediaResponse>
    {
        private GetUserRatedMediaUseCase _uc;

        public GetUserRatedMediaUseCaseCallback(GetUserRatedMediaUseCase useCase)
        {
            _uc = useCase;
        }

        public void OnSuccess(ZResponse<GetUserRatedMediaResponse> response)
        {
            _uc?.PresenterCallback?.OnSuccess(response);
        }

        public void OnFailure(Exception exception)
        {
            _uc?.PresenterCallback?.OnFailure(exception);
        }
    }

    public class GetUserRatedMediaResponse
    {
     
        public List<UserRatingBObj> RatedMedia { get; set; }

        public GetUserRatedMediaResponse(List<UserRatingBObj> ratedMedia)
        {
            RatedMedia = ratedMedia;
        }
    }

    public class GetUserRatedMediaRequest
    {
        public long UserId { get; set; }

        public GetUserRatedMediaRequest(long userId)
        {
            UserId = userId;
        }
    }

    public interface IGetUserRatedMediaDataManager
    {
        void GetUserRatedMedia(GetUserRatedMediaRequest request, GetUserRatedMediaUseCaseCallback callback);
    }

    public interface IGetUserRatedMediaPresenterCallback : ICallback<GetUserRatedMediaResponse> { }
}
