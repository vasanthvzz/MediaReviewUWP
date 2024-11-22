using CommonClassLibrary;
using MediaReviewClassLibrary.Data.DataHandler.Contract;
using MediaReviewClassLibrary.Domain;
using MediaReviewClassLibrary.Models.Enitites;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace MediaReviewClassLibrary.DataManager
{
    public class GetAllMediaDataManager : IGetAllMediaDataManager
    {
        public IMediaDataHandler _mediaDataHandler = MediaReviewDIServiceProvider.GetServiceProvider().GetRequiredService<IMediaDataHandler>();

        public void GetAllMedia(ICallback<GetAllMediaResponse> callback)
        {
            try
            {
                List<Media> mediaList = _mediaDataHandler.GetAllMedia().Result;
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
