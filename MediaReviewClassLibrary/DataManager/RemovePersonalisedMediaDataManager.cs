using CommonClassLibrary;
using MediaReviewClassLibrary.Data.DataHandler.Contract;
using MediaReviewClassLibrary.Domain;
using MediaReviewClassLibrary.Models.Constants;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MediaReviewClassLibrary.DataManager
{
    public class RemovePersonalisedMediaDataManager : IRemovePersonalisedMediaDataManager
    {
        private IPersonalMediaDataHandler _personalMediaDataHandler = MediaReviewDIServiceProvider.GetServiceProvider().GetRequiredService<IPersonalMediaDataHandler>();
        public async void RemovePersonalisedMedia(RemovePersonalisedMediaRequest request, RemovePersonalisedMediaUseCaseCallback callback)
        {
            try
            {
                long mediaId = 0;
                switch (request.PersonalisedMediaType)
                {
                    case PersonalMediaType.FAVOURITE : 
                    {
                        mediaId = await  _personalMediaDataHandler.RemoveFromFavourite(request.UserId,request.MediaId);
                        break;
                    }
                    case PersonalMediaType.HAS_WATCHED :
                        {
                            mediaId = await _personalMediaDataHandler.RemoveFromHasWatched(request.UserId, request.MediaId);
                            break;
                    }
                    case PersonalMediaType.WATCHLIST :
                        {
                            mediaId = await _personalMediaDataHandler.RemoveFromWatchList(request.UserId, request.MediaId);
                            break;
                    }
                    default:
                        {
                            break;
                        }
                }
                bool success = mediaId != 0;
                RemovePersonalisedMediaResponse response = new RemovePersonalisedMediaResponse( mediaId,success);
                ZResponse<RemovePersonalisedMediaResponse> zResponse = new ZResponse<RemovePersonalisedMediaResponse>(response);
                callback?.OnSuccess(zResponse);
            }
            catch(Exception e)
            {
                callback?.OnFailure(e);
            }
        }
    }
}
