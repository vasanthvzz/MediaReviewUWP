using CommonClassLibrary;
using MediaReviewClassLibrary.Models.Enitites;
using Microsoft.Extensions.DependencyInjection;
using System;

using System.Collections.Generic;

namespace MediaReviewClassLibrary.Domain
{
    public class GetAllMediaUseCase : UseCaseBase<GetAllMediaResponse>
    {
        private IGetAllMediaDataManager _dm;

        public GetAllMediaUseCase(GetAllMediaRequest req, IGetAllMediaPresenterCallback callback) : base(callback)
        {
            _dm = MediaReviewDIServiceProvider.GetServiceProvider().GetRequiredService<IGetAllMediaDataManager>();
        }

        public override void Action()
        {
            _dm.GetAllMedia(new GetAllMediaUseCaseCallback(this));
        }
    }

    public class GetAllMediaUseCaseCallback : ICallback<GetAllMediaResponse>
    {
        private GetAllMediaUseCase _uc;

        public GetAllMediaUseCaseCallback(GetAllMediaUseCase uc)
        {
            _uc = uc;
        }

        public void OnFailure(Exception error)
        {
            _uc?.PresenterCallback?.OnFailure(error);
        }

        public void OnSuccess(ZResponse<GetAllMediaResponse> response)
        {
            _uc?.PresenterCallback?.OnSuccess(response);
        }
    }

    public interface IGetAllMediaDataManager
    {
        void GetAllMedia(ICallback<GetAllMediaResponse> callback);
    }

    public class GetAllMediaRequest { }

    public class GetAllMediaResponse
    {
        public List<Media> MediaList { get; }
        public GetAllMediaResponse(List<Media> mediaList)
        {
            MediaList = mediaList;
        }
    }

    public interface IGetAllMediaPresenterCallback : ICallback<GetAllMediaResponse> { }
}


