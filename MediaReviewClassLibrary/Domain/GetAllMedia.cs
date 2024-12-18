using CommonClassLibrary;
using MediaReviewClassLibrary.Models;
using Microsoft.Extensions.DependencyInjection;
using System;

using System.Collections.Generic;

namespace MediaReviewClassLibrary.Domain
{
    public class GetAllMediaUseCase : UseCaseBase<GetAllMediaResponse>
    {
        private IGetAllMediaDataManager _dm;
        private GetAllMediaRequest _request;

        public GetAllMediaUseCase(GetAllMediaRequest request, IGetAllMediaPresenterCallback callback) : base(callback)
        {
            _dm = MediaReviewDIServiceProvider.GetServiceProvider().GetRequiredService<IGetAllMediaDataManager>();
            _request = request;
        }

        public override void Action()
        {
            _dm.GetAllMedia(_request, new GetAllMediaUseCaseCallback(this));
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
        void GetAllMedia(GetAllMediaRequest request, ICallback<GetAllMediaResponse> callback);
    }

    public class GetAllMediaRequest 
    {
        public long CurrentMediaCount { get; set; }
        public long RequiredMediaCount {  get; set; }
        
        public GetAllMediaRequest(long currentMediaCount, long requiredMediaCount)
        {
            CurrentMediaCount = currentMediaCount;
            RequiredMediaCount = requiredMediaCount;
        }
    }

    public class GetAllMediaResponse
    {
        public List<MediaBObj> MediaList { get; }
        public GetAllMediaResponse(List<MediaBObj> mediaList)
        {
            MediaList = mediaList;
        }
    }

    public interface IGetAllMediaPresenterCallback : ICallback<GetAllMediaResponse> { }
}


