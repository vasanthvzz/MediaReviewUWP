using CommonClassLibrary;
using MediaReviewClassLibrary.Models;
using MediaReviewClassLibrary.Models.Constants;
using MediaReviewClassLibrary.Models.Enitites;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace MediaReviewClassLibrary.Domain
{
    public class GetPersonalisedMediaUseCase : UseCaseBase<GetPersonalisedMediaResponse>
    {
        private GetPersonalisedMediaRequest _request;
        private IGetPersonalisedMediaDataManager _dm;

        public GetPersonalisedMediaUseCase(GetPersonalisedMediaRequest request, ICallback<GetPersonalisedMediaResponse> callback) : base(callback)
        {
            _request = request;
            _dm = MediaReviewDIServiceProvider.GetServiceProvider().GetRequiredService<IGetPersonalisedMediaDataManager>();
        }

        public override void Action()
        {
            PersonalisedMediaUseCaseCallback callback = new PersonalisedMediaUseCaseCallback(this);
            _dm.GetPersonalisedMedia(_request, callback);
        }
    }

    public class PersonalisedMediaUseCaseCallback : ICallback<GetPersonalisedMediaResponse>
    {
        private GetPersonalisedMediaUseCase _usecase;

        public PersonalisedMediaUseCaseCallback(GetPersonalisedMediaUseCase usecase)
        {
            _usecase = usecase;
        }

        public void OnSuccess(ZResponse<GetPersonalisedMediaResponse> response)
        {
            _usecase?.PresenterCallback?.OnSuccess(response);
        }

        public void OnFailure(Exception exception)
        {
            _usecase?.PresenterCallback?.OnFailure(exception);
        }
    }

    public class GetPersonalisedMediaRequest
    {
        public long UserId { get; set; }
        public PersonalMediaType PersonalisedListType { get; set; }

        public GetPersonalisedMediaRequest(long userId, PersonalMediaType personalisedListType)
        {
            UserId = userId;
            PersonalisedListType = personalisedListType;
        }
    }

    public class GetPersonalisedMediaResponse
    {
        public List<MediaBObj> MediaList { get; set; }

        public GetPersonalisedMediaResponse(List<MediaBObj> media)
        {
            MediaList = media;
        }
    }

    public interface IGetPersonalisedMediaDataManager
    {
        void GetPersonalisedMedia(GetPersonalisedMediaRequest request, PersonalisedMediaUseCaseCallback callback);
    }

    public interface IGetPersonalisedMediaPresenterCallback : ICallback<GetPersonalisedMediaResponse> { }
}
