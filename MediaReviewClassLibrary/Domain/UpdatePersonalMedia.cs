using CommonClassLibrary;
using MediaReviewClassLibrary.Models.Enitites;
using System;
using System.Threading.Tasks;

namespace MediaReviewClassLibrary.Domain
{
    public class UpdatePersonalMediaUseCase : UseCaseBase<UpdatePersonalMediaResponse>
    {
        private UpdatePersonalMediaRequest _request;
        private IUpdatePersonalMediaDataManager _dm = MediaReviewDIServiceProvider.GetRequiredService<IUpdatePersonalMediaDataManager>();

        public UpdatePersonalMediaUseCase(UpdatePersonalMediaRequest request, ICallback<UpdatePersonalMediaResponse> callback) : base(callback)
        {
            _request = request;
        }

        public override void Action()
        {
            _dm.UpdatePersonalMedia(_request, new UpdatePersonalMediaUseCaseCallback(this));
        }
    }

    public class UpdatePersonalMediaUseCaseCallback : ICallback<UpdatePersonalMediaResponse>
    {
        private UpdatePersonalMediaUseCase _uc;

        public UpdatePersonalMediaUseCaseCallback(UpdatePersonalMediaUseCase usecase)
        {
            _uc = usecase;
        }

        public void OnSuccess(ZResponse<UpdatePersonalMediaResponse> response)
        {
            _uc?.PresenterCallback?.OnSuccess(response);
        }

        public void OnFailure(Exception exception)
        {
            _uc?.PresenterCallback?.OnFailure(exception);
        }
    }

    public class UpdatePersonalMediaRequest
    {
        public PersonalMedia UserPersonalMedia { get; }

        public UpdatePersonalMediaRequest(PersonalMedia personalMedia)
        {
            UserPersonalMedia = personalMedia;
        }
    }

    public class UpdatePersonalMediaResponse
    {
        public bool Success { get; set; }
        public PersonalMedia UpdatedPersonalMedia { get; set; }

        public UpdatePersonalMediaResponse(bool success, PersonalMedia updatedPersonalMedia)
        {
            Success = success;
            UpdatedPersonalMedia = updatedPersonalMedia;
        }
    }

    public interface IUpdatePersonalMediaDataManager
    {
        Task UpdatePersonalMedia(UpdatePersonalMediaRequest request, UpdatePersonalMediaUseCaseCallback usecase);
    }

    public interface IUpdatePersonalMediaPresenterCallback : ICallback<UpdatePersonalMediaResponse>
    { }
}