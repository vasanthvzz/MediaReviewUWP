using CommonClassLibrary;
using MediaReviewClassLibrary.Models.Enitites;
using System;
using System.Threading.Tasks;

namespace MediaReviewClassLibrary.Domain
{
    public class UpdateUserRatingUseCase : UseCaseBase<UpdateUserRatingResponse>
    {
        private UpdateUserRatingRequest _request;
        private IUpdateUserRatingDataManager _dm = MediaReviewDIServiceProvider.GetRequiredService<IUpdateUserRatingDataManager>();

        public UpdateUserRatingUseCase(UpdateUserRatingRequest request, ICallback<UpdateUserRatingResponse> callback) : base(callback)
        {
            _request = request;
        }

        public override void Action()
        {
            _dm.UpdateUserRating(_request, new UpdateUserRatingUsecaseCallback(this));
        }
    }

    public class UpdateUserRatingUsecaseCallback : ICallback<UpdateUserRatingResponse>
    {
        private UpdateUserRatingUseCase _usecase;

        public UpdateUserRatingUsecaseCallback(UpdateUserRatingUseCase usecase)
        {
            _usecase = usecase;
        }

        public void OnSuccess(ZResponse<UpdateUserRatingResponse> response)
        {
            _usecase?.PresenterCallback?.OnSuccess(response);
        }

        public void OnFailure(Exception exception)
        {
            _usecase?.PresenterCallback?.OnFailure(exception);
        }
    }

    public class UpdateUserRatingResponse
    {
        public bool Success { get; set; }
        public Rating UserRating { get; set; }

        public UpdateUserRatingResponse(bool success, Rating userRating)
        {
            Success = success;
            UserRating = userRating;
        }
    }

    public class UpdateUserRatingRequest
    {
        public Rating UserRating { get; set; }

        public UpdateUserRatingRequest(Rating userRating)
        {
            UserRating = userRating;
        }
    }

    public interface IUpdateUserRatingDataManager
    {
        Task UpdateUserRating(UpdateUserRatingRequest request, UpdateUserRatingUsecaseCallback updateUserRatingUsecaseCallback);
    }

    public interface IUpdateUserRatingPresenterCallback : ICallback<UpdateUserRatingResponse>
    { }
}