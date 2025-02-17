using CommonClassLibrary;
using MediaReviewClassLibrary.Models.Constants;
using System;
using System.Threading.Tasks;

namespace MediaReviewClassLibrary.Domain
{
    public class RemovePersonalisedMediaUseCase : UseCaseBase<RemovePersonalisedMediaResponse>
    {
        private RemovePersonalisedMediaRequest _request;
        private IRemovePersonalisedMediaDataManager _dm = MediaReviewDIServiceProvider.GetRequiredService<IRemovePersonalisedMediaDataManager>();

        public RemovePersonalisedMediaUseCase(RemovePersonalisedMediaRequest request, ICallback<RemovePersonalisedMediaResponse> callback) : base(callback)
        {
            _request = request;
        }

        public override void Action()
        {
            RemovePersonalisedMediaUseCaseCallback callback = new RemovePersonalisedMediaUseCaseCallback(this);
            _dm.RemovePersonalisedMedia(_request, callback);
        }
    }

    public class RemovePersonalisedMediaUseCaseCallback : ICallback<RemovePersonalisedMediaResponse>
    {
        private RemovePersonalisedMediaUseCase _usecase;

        public RemovePersonalisedMediaUseCaseCallback(RemovePersonalisedMediaUseCase usecase)
        {
            _usecase = usecase;
        }

        public void OnFailure(Exception exception)
        {
            _usecase?.PresenterCallback?.OnFailure(exception);
        }

        public void OnSuccess(ZResponse<RemovePersonalisedMediaResponse> response)
        {
            _usecase?.PresenterCallback?.OnSuccess(response);
        }
    }

    public class RemovePersonalisedMediaRequest
    {
        public long UserId { get; set; }
        public long MediaId { get; set; }
        public PersonalMediaType PersonalisedMediaType { get; set; }

        public RemovePersonalisedMediaRequest(long userId, long mediaId, PersonalMediaType personalisedMediaType)
        {
            UserId = userId;
            MediaId = mediaId;
            PersonalisedMediaType = personalisedMediaType;
        }
    }

    public class RemovePersonalisedMediaResponse
    {
        public long MediaId { get; set; }
        public bool Success { get; set; }

        public RemovePersonalisedMediaResponse(long mediaId, bool success)
        {
            MediaId = mediaId;
            Success = success;
        }
    }

    public interface IRemovePersonalisedMediaDataManager
    {
        Task RemovePersonalisedMedia(RemovePersonalisedMediaRequest request, RemovePersonalisedMediaUseCaseCallback callback);
    }

    public interface IRemovePersonalisedMediaPresenterCallback : ICallback<RemovePersonalisedMediaResponse>
    { }
}