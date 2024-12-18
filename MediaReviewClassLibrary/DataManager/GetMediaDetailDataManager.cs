using CommonClassLibrary;
using MediaReviewClassLibrary.Data.DataHandler.Contract;
using MediaReviewClassLibrary.Domain;
using MediaReviewClassLibrary.Models;
using MediaReviewClassLibrary.Models.Enitites;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediaReviewClassLibrary.DataManager
{
    public class GetMediaDetailDataManager : IGetMediaDetailDataManager
    {

        private IMediaDataHandler _mediaDataHandler = MediaReviewDIServiceProvider.GetServiceProvider().GetRequiredService<IMediaDataHandler>();
        private IPersonalMediaDataHandler _personalmediaDataHandler = MediaReviewDIServiceProvider.GetServiceProvider().GetRequiredService<IPersonalMediaDataHandler>();
        private IRatingDataHandler _ratingDataHandler = MediaReviewDIServiceProvider.GetServiceProvider().GetRequiredService<IRatingDataHandler>();
        private IGenreDataHandler _genreDataHandler = MediaReviewDIServiceProvider.GetServiceProvider().GetRequiredService<IGenreDataHandler>();


        public async Task GetMediaDetail(GetMediaDetailRequest request, GetMediaDetailUseCaseCallback callback)
        {
            try
            {
                Media media = await _mediaDataHandler.GetMediaById(request.MediaId); 
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

                List<Genre> genres = await _genreDataHandler.GetGenreByMediaId(request.MediaId);
                float mediaRating = await _ratingDataHandler.GetAverageRating(request.MediaId);
                long ratedUser = await _ratingDataHandler.GetRatedUserCount(request.MediaId);
                MediaDetailBObj mediaDetail = new MediaDetailBObj(request.UserId, media, userRating,personalMedia,genres,mediaRating,ratedUser);
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