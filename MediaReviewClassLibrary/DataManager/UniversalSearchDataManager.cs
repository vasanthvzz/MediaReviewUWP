using CommonClassLibrary;
using MediaReviewClassLibrary.Data.DataHandler.Contract;
using MediaReviewClassLibrary.Domain;
using MediaReviewClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediaReviewClassLibrary.DataManager
{
    public class UniversalSearchDataManager : IUniversalSearchDataManager
    {
        private IMediaDataHandler _mediaDataHandler = MediaReviewDIServiceProvider.GetRequiredService<IMediaDataHandler>();
        private IGetAllMediaDataManager _mediaDataManager = MediaReviewDIServiceProvider.GetRequiredService<IGetAllMediaDataManager>();

        public async Task Search(UniversalSearchRequest request, ICallback<UniversalSearchResponse> callback)
        {
            try
            {
                var mediaList = await _mediaDataHandler.SearchMediaByName(request?.SearchString);
                var resultList = new List<MediaBObj>();
                foreach (var item in mediaList)
                {
                    resultList.Add(await _mediaDataManager.GetMediaBObj(item));
                }
                ZResponse<UniversalSearchResponse> response = new ZResponse<UniversalSearchResponse>(new UniversalSearchResponse(resultList));
                callback?.OnSuccess(response);
            }
            catch (Exception e)
            {
                callback?.OnFailure(e);
                callback?.OnFailure(e.InnerException);
            }
        }
    }
}