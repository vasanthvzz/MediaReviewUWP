using CommonClassLibrary;
using MediaReviewClassLibrary.Models.Enitites;
using System;
using System.Threading.Tasks;

namespace MediaReviewClassLibrary.Domain
{
    public class DeleteReviewUseCase : UseCaseBase<DeleteReviewResponse>
    {
        private DeleteReviewRequest _request;
        private IDeleteReviewDataManager _dm = MediaReviewDIServiceProvider.GetRequiredService<IDeleteReviewDataManager>();

        public DeleteReviewUseCase(DeleteReviewRequest request, ICallback<DeleteReviewResponse> callback) : base(callback)
        {
            _request = request;
        }

        public override void Action()
        {
            _dm.DeleteReview(_request, new DeleteReviewUseCaseCallback(this));
        }
    }

    public class DeleteReviewUseCaseCallback : ICallback<DeleteReviewResponse>
    {
        private DeleteReviewUseCase _uc;

        public DeleteReviewUseCaseCallback(DeleteReviewUseCase uc)
        {
            _uc = uc;
        }

        public void OnFailure(Exception exception)
        {
            _uc?.PresenterCallback?.OnFailure(exception);
        }

        public void OnSuccess(ZResponse<DeleteReviewResponse> response)
        {
            _uc?.PresenterCallback?.OnSuccess(response);
        }
    }

    public class DeleteReviewResponse
    {
        public bool Success { get; set; }
        public Review DeletedReview { get; set; }

        public DeleteReviewResponse(bool success, Review deletedReview)
        {
            Success = success;
            DeletedReview = deletedReview;
        }
    }

    public class DeleteReviewRequest
    {
        public long ReviewId { get; set; }
        public long UserId { get; set; }

        public DeleteReviewRequest(long reviewId, long userId)
        {
            ReviewId = reviewId;
            UserId = userId;
        }
    }

    public interface IDeleteReviewPresenterCallback : ICallback<DeleteReviewResponse>
    { }

    public interface IDeleteReviewDataManager
    {
        Task DeleteReview(DeleteReviewRequest request, DeleteReviewUseCaseCallback deleteReviewUseCaseCallback);
    }
}