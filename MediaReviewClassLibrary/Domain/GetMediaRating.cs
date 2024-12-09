using CommonClassLibrary;
using System;

namespace MediaReviewClassLibrary.Domain
{
    public class GetMediaRatingUseCase : UseCaseBase<GetMediaRatingResponse>
    {
        private GetMediaRatingRequest _request;
        private IGetMediaRatingDataManager _dataManager = MediaReviewDIServiceProvider.GetRequiredService<IGetMediaRatingDataManager>();

        public GetMediaRatingUseCase(GetMediaRatingRequest request , ICallback<GetMediaRatingResponse> callback) : base(callback)
        {
            _request = request;
        }

        public override void Action()
        {
            _dataManager.GetMediaRating(_request, new GetMediaRatingUseCaseCallback(this));
        }
    }

    public class GetMediaRatingUseCaseCallback : ICallback<GetMediaRatingResponse>
    {
        private GetMediaRatingUseCase _uc;

        public GetMediaRatingUseCaseCallback(GetMediaRatingUseCase uc)
        {
            _uc = uc;
        }

        public void OnFailure(Exception exception)
        {
            _uc?.PresenterCallback?.OnFailure(exception);
        }

        public void OnSuccess(ZResponse<GetMediaRatingResponse> response)
        {
            _uc?.PresenterCallback?.OnSuccess(response);
        }
    }

    public class GetMediaRatingRequest 
    { 
        public long MediaId {  get; set; }
        
        public GetMediaRatingRequest(long mediaId)
        {
            MediaId = mediaId;
        }
    }

    public class GetMediaRatingResponse
    {
        public long MediaId { get; set; }
        public long RatedUserCount { get; set; }
        public float RatingScore { get; set; }

        public GetMediaRatingResponse(long mediaId, float ratingScore, long ratedUserCount)
        {
            MediaId= mediaId;
            RatedUserCount = ratedUserCount;
            RatingScore = ratingScore;
        }
    }

    public interface IGetMediaRatingDataManager 
    {
        void GetMediaRating(GetMediaRatingRequest request, ICallback<GetMediaRatingResponse> callback);
    }

    public interface IGetMediaRatingPresenterCallback : ICallback<GetMediaRatingResponse> { }
}
