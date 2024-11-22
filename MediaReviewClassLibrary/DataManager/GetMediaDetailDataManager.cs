using CommonClassLibrary;
using MediaReviewClassLibrary.Data.DataHandler;
using MediaReviewClassLibrary.Data.DataHandler.Contract;
using MediaReviewClassLibrary.Domain;
using MediaReviewClassLibrary.Models;
using MediaReviewClassLibrary.Models.Enitites;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MediaReviewClassLibrary.DataManager
{
    public class GetMediaDetailDataManager : IGetMediaDetailDataManager
    {

        private IMediaDataHandler _mediaDataHandler = MediaReviewDIServiceProvider.GetServiceProvider().GetRequiredService<IMediaDataHandler>();
        private IPersonalMediaDataHandler _personalmediaDataHandler = MediaReviewDIServiceProvider.GetServiceProvider().GetRequiredService<IPersonalMediaDataHandler>();
        private IRatingDataHandler _ratingDataHandler = MediaReviewDIServiceProvider.GetServiceProvider().GetRequiredService<IRatingDataHandler>();


        public async void GetMediaDetail(GetMediaDetailRequest request, GetMediaDetailUseCaseCallback callback)
        {
            try
            {
                Media media = _mediaDataHandler.GetMediaById(request.MediaId).Result; 
                PersonalMedia personalMedia = await _personalmediaDataHandler.GetPersonalMedia(request.MediaId, request.UserId);
                Rating userRating = await _ratingDataHandler.GetUserRating(request.UserId,request.MediaId);

                if (personalMedia == null)
                {
                    personalMedia = new PersonalMedia(request.UserId, request.MediaId);
                    await _personalmediaDataHandler.UpdatePersonalMedia(personalMedia);
                }

                if(userRating == null)
                {
                    userRating = new Rating(request.UserId, request.MediaId);
                    await _ratingDataHandler.UpdateUserRating(userRating);
                }

                MediaDetailBObj mediaDetail = new MediaDetailBObj(request.UserId, media,personalMedia,userRating);
                GetMediaDetailResponse response = new GetMediaDetailResponse(mediaDetail);
                ZResponse<GetMediaDetailResponse> zResponse = new ZResponse<GetMediaDetailResponse>(response);
                callback?.OnSuccess(zResponse);
            }
            catch (Exception e)
            {
                callback?.OnFailure(e);
            }
        }
    }
}