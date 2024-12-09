using CommonClassLibrary;
using MediaReviewClassLibrary.Data.DataHandler.Contract;
using MediaReviewClassLibrary.Domain;
using MediaReviewClassLibrary.Models;
using MediaReviewClassLibrary.Models.Enitites;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;


namespace MediaReviewClassLibrary.DataManager
{
    public class GetPersonalisedMediaDataManager : IGetPersonalisedMediaDataManager
    {
        private IPersonalMediaDataHandler _personalMediaDataHandler = MediaReviewDIServiceProvider.GetServiceProvider().GetRequiredService<IPersonalMediaDataHandler>();
        private IMediaDataHandler _mediaDataHandler = MediaReviewDIServiceProvider.GetServiceProvider().GetRequiredService<IMediaDataHandler>();
        private IRatingDataHandler _ratingDataHandler = MediaReviewDIServiceProvider.GetServiceProvider().GetRequiredService<IRatingDataHandler>(); 

        public async void GetPersonalisedMedia(GetPersonalisedMediaRequest request, PersonalisedMediaUseCaseCallback callback)
        {
            try
            {
                List<PersonalMedia> personalMediaList = await _personalMediaDataHandler.GetPersonalisedMedia(request.UserId, request.PersonalisedListType);
                List<MediaBObj> mediaList = new List<MediaBObj>();
                foreach (var personalMedia in personalMediaList) 
                {
                    float avgRating = await _ratingDataHandler.GetAverageRating(personalMedia.MediaId);
                    mediaList.Add(new MediaBObj( await _mediaDataHandler.GetMediaById(personalMedia.MediaId),avgRating));
                }
                GetPersonalisedMediaResponse response = new GetPersonalisedMediaResponse(mediaList);
                ZResponse<GetPersonalisedMediaResponse> zResponse = new ZResponse<GetPersonalisedMediaResponse>(response);
                callback?.OnSuccess(zResponse); 
            }
            catch(Exception e)
            {
                callback?.OnFailure(e);
            }
        }
    }
}
