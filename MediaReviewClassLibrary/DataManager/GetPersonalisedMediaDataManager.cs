using CommonClassLibrary;
using MediaReviewClassLibrary.Data.DataHandler.Contract;
using MediaReviewClassLibrary.Domain;
using MediaReviewClassLibrary.Models;
using MediaReviewClassLibrary.Models.Enitites;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediaReviewClassLibrary.DataManager
{
    public class GetPersonalisedMediaDataManager : IGetPersonalisedMediaDataManager
    {
        private IPersonalMediaDataHandler _personalMediaDataHandler = MediaReviewDIServiceProvider.GetRequiredService<IPersonalMediaDataHandler>();
        private IMediaDataHandler _mediaDataHandler = MediaReviewDIServiceProvider.GetRequiredService<IMediaDataHandler>();
        private IRatingDataHandler _ratingDataHandler = MediaReviewDIServiceProvider.GetRequiredService<IRatingDataHandler>();

        public async Task GetPersonalisedMedia(GetPersonalisedMediaRequest request, PersonalisedMediaUseCaseCallback callback)
        {
            try
            {
                List<PersonalMedia> personalMediaList = await _personalMediaDataHandler.GetPersonalisedMedia(request.UserId, request.PersonalisedListType);
                List<MediaBObj> mediaList = new List<MediaBObj>();
                foreach (var personalMedia in personalMediaList)
                {
                    float avgRating = await _ratingDataHandler.GetAverageRating(personalMedia.MediaId);
                    var media = await _mediaDataHandler.GetMediaById(personalMedia.MediaId);
                    if (media != null) mediaList.Add(new MediaBObj(media, avgRating));
                }
                GetPersonalisedMediaResponse response = new GetPersonalisedMediaResponse(mediaList);
                ZResponse<GetPersonalisedMediaResponse> zResponse = new ZResponse<GetPersonalisedMediaResponse>(response);
                callback?.OnSuccess(zResponse);
            }
            catch (Exception e)
            {
                callback?.OnFailure(e);
            }
        }
    }
}