using MediaReviewClassLibrary.Data.DataHandler.Contract;
using MediaReviewClassLibrary.Domain;
using MediaReviewClassLibrary.Models.Enitites;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MediaReviewClassLibrary.DataManager
{
    public class UpdatePersonalMediaDataManager : IUpdatePersonalMediaDataManager
    {
        private IPersonalMediaDataHandler _personalMediaDataHandler = MediaReviewDIServiceProvider.GetServiceProvider().GetService<IPersonalMediaDataHandler>();
        public async void UpdatePersonalMedia(UpdatePersonalMediaRequest request, UpdatePersonalMediaUseCaseCallback callback)
        {
            try
            {
                var personalMedia =await _personalMediaDataHandler.UpdatePersonalMedia(request.UserPersonalMedia);
                bool success = request.UserPersonalMedia == personalMedia;
                UpdatePersonalMediaResponse response = new UpdatePersonalMediaResponse(success,personalMedia);

            }
            catch (Exception e) 
            { 
                callback?.OnFailure(e);
            }
        }
    }
}
