using MediaReviewClassLibrary.Data.DataHandler.Contract;
using MediaReviewClassLibrary.Domain;
using System;
using System.Threading.Tasks;

namespace MediaReviewClassLibrary.DataManager
{
    public class UpdatePersonalMediaDataManager : IUpdatePersonalMediaDataManager
    {
        private IPersonalMediaDataHandler _personalMediaDataHandler = MediaReviewDIServiceProvider.GetRequiredService<IPersonalMediaDataHandler>();

        public async Task UpdatePersonalMedia(UpdatePersonalMediaRequest request, UpdatePersonalMediaUseCaseCallback callback)
        {
            try
            {
                var personalMedia = await _personalMediaDataHandler.UpdatePersonalMedia(request.UserPersonalMedia);
                bool success = request.UserPersonalMedia == personalMedia;
                UpdatePersonalMediaResponse response = new UpdatePersonalMediaResponse(success, personalMedia);
            }
            catch (Exception e)
            {
                callback?.OnFailure(e);
            }
        }
    }
}