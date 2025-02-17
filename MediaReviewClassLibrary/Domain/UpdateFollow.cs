using CommonClassLibrary;
using MediaReviewClassLibrary.Models.Enitites;
using System;
using System.Threading.Tasks;

namespace MediaReviewClassLibrary.Domain
{
    public class UpdateFollowUseCase : UseCaseBase<UpdateFollowResponse>
    {
        private UpdateFollowRequest _request;
        private IUpdateFollowDataManager _dm = MediaReviewDIServiceProvider.GetRequiredService<IUpdateFollowDataManager>();

        public UpdateFollowUseCase(UpdateFollowRequest request, ICallback<UpdateFollowResponse> callback) : base(callback)
        {
            _request = request;
        }

        public override void Action()
        {
            _dm.UpdateFollow(_request, new UpdateFollowUseCaseCallback(this));
        }
    }

    public class UpdateFollowUseCaseCallback : ICallback<UpdateFollowResponse>
    {
        private UpdateFollowUseCase _usecase;

        public UpdateFollowUseCaseCallback(UpdateFollowUseCase usecase)
        {
            _usecase = usecase;
        }

        public void OnFailure(Exception exception)
        {
            _usecase?.PresenterCallback?.OnFailure(exception);
        }

        public void OnSuccess(ZResponse<UpdateFollowResponse> response)
        {
            _usecase.PresenterCallback?.OnSuccess(response);
        }
    }

    public class UpdateFollowRequest
    {
        public long UserId { get; set; }
        public long FolloweeId { get; set; }
        public bool IsFollowing { get; set; }

        public UpdateFollowRequest(long userId, long followingId, bool isFollowing)
        {
            UserId = userId;
            FolloweeId = followingId;
            IsFollowing = isFollowing;
        }
    }

    public class UpdateFollowResponse
    {
        public FolloweeMapper UpdatedFollow { get; set; }

        public UpdateFollowResponse(FolloweeMapper updatedFollow)
        {
            UpdatedFollow = updatedFollow;
        }
    }

    public interface IUpdateFollowDataManager
    {
        Task UpdateFollow(UpdateFollowRequest request, UpdateFollowUseCaseCallback callback);

        Task<bool> GetFollowingStatus(long userId, long followeeId);
    }

    public interface IUpdateFollowPresenterCallback : ICallback<UpdateFollowResponse>
    { }
}