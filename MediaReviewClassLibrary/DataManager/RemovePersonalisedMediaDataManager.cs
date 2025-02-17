using CommonClassLibrary;
using MediaReviewClassLibrary.Data.DataHandler.Contract;
using MediaReviewClassLibrary.Domain;
using MediaReviewClassLibrary.Models.Constants;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace MediaReviewClassLibrary.DataManager
{
    public class RemovePersonalisedMediaDataManager : IRemovePersonalisedMediaDataManager
    {
        private IPersonalMediaDataHandler _personalMediaDataHandler = MediaReviewDIServiceProvider.GetRequiredService<IPersonalMediaDataHandler>();

        public async Task RemovePersonalisedMedia(RemovePersonalisedMediaRequest request, RemovePersonalisedMediaUseCaseCallback callback)
        {
            try
            {
                long mediaId = 0;
                mediaId = await _personalMediaDataHandler.RemoveFromPersonalisedList(request.UserId, request.MediaId,request.PersonalisedMediaType);
                bool success = mediaId != 0;
                RemovePersonalisedMediaResponse response = new RemovePersonalisedMediaResponse(mediaId, success);
                ZResponse<RemovePersonalisedMediaResponse> zResponse = new ZResponse<RemovePersonalisedMediaResponse>(response);
                callback?.OnSuccess(zResponse);
            }
            catch (Exception e)
            {
                callback?.OnFailure(e);
            }
        }
    }
}