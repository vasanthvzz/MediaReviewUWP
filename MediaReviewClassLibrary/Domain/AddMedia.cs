using CommonClassLibrary;
using MediaReviewClassLibrary.Models;
using MediaReviewClassLibrary.Models.Enitites;
using System;
using System.Threading.Tasks;

namespace MediaReviewClassLibrary.Domain
{
    public class AddMediaUseCase : UseCaseBase<AddMediaResponse>
    {
        private AddMediaRequest _request;
        private IAddMediaDataManager _dm = MediaReviewDIServiceProvider.GetRequiredService<IAddMediaDataManager>();

        public AddMediaUseCase(AddMediaRequest request, ICallback<AddMediaResponse> callback) : base(callback)
        {
            _request = request;
        }

        public override void Action()
        {
            _dm.AddMedia(_request, new AddMediaUseCaseCallback(this));
        }
    }

    public class AddMediaUseCaseCallback : ICallback<AddMediaResponse>
    {
        private AddMediaUseCase _uc;

        public AddMediaUseCaseCallback(AddMediaUseCase uc)
        {
            _uc = uc;
        }

        public void OnFailure(Exception exception)
        {
            _uc?.PresenterCallback?.OnFailure(exception);
        }

        public void OnSuccess(ZResponse<AddMediaResponse> response)
        {
            _uc?.PresenterCallback?.OnSuccess(response);
        }
    }

    public class AddMediaRequest
    {
        public AddMediaBObj AddMedia { get; set; }

        public AddMediaRequest(AddMediaBObj addMedia)
        {
            AddMedia = addMedia;
        }
    }

    public class AddMediaResponse
    {
        public Media MediaDetail {  get; set; }
        public bool Success { get; set; }

        public AddMediaResponse(Media mediaDetail, bool success)
        {
            MediaDetail = mediaDetail;
            Success = success;
        }
    }

    public interface IAddMediaDataManager
    {
        Task AddMedia(AddMediaRequest request, ICallback<AddMediaResponse> addMediaUseCaseCallback);
    }

    public interface IAddMediaPresenterCallback : ICallback<AddMediaResponse>
    { }
}