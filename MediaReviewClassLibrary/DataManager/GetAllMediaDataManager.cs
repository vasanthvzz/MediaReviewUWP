using CommonClassLibrary;
using MediaReviewClassLibrary.Data.DataHandler;
using MediaReviewClassLibrary.Data.DataHandler.Contract;
using MediaReviewClassLibrary.Domain;
using MediaReviewClassLibrary.Models;
using MediaReviewClassLibrary.Models.Enitites;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace MediaReviewClassLibrary.DataManager
{
    public class GetAllMediaDataManager : IGetAllMediaDataManager
    {
        public IMediaDataHandler _mediaDataHandler = MediaReviewDIServiceProvider.GetServiceProvider().GetRequiredService<IMediaDataHandler>();
        public IRatingDataHandler _ratingDataHandler = MediaReviewDIServiceProvider.GetServiceProvider().GetRequiredService<IRatingDataHandler>();

        public async void GetAllMedia(ICallback<GetAllMediaResponse> callback)
        {
            try
            {
                List<MediaBObj> mediaList = new List<MediaBObj>();
                foreach (var media in await _mediaDataHandler.GetAllMedia())
                {
                    float avgRating = await _ratingDataHandler.GetAverageRating(media.MediaId);
                    mediaList.Add(new MediaBObj(await _mediaDataHandler.GetMediaById(media.MediaId), avgRating));
                }
                GetAllMediaResponse request = new GetAllMediaResponse(mediaList);
                ZResponse<GetAllMediaResponse> zResponse = new ZResponse<GetAllMediaResponse>(request);
                callback?.OnSuccess(zResponse);
            }
            catch (Exception e)
            {
                callback?.OnFailure(e);
            }
        }
    }
}
