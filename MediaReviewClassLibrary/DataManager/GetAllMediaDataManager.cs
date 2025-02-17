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
    public class GetAllMediaDataManager : IGetAllMediaDataManager
    {
        private IMediaDataHandler _mediaDataHandler = MediaReviewDIServiceProvider.GetRequiredService<IMediaDataHandler>();
        private IRatingDataHandler _ratingDataHandler = MediaReviewDIServiceProvider.GetRequiredService<IRatingDataHandler>();

        public async Task<MediaBObj> GetMediaBObj(Media media)
        {
            if (media == null) return null;
            float avgRating = await _ratingDataHandler.GetAverageRating(media.MediaId);
            return new MediaBObj(media, avgRating);
        }

        public async Task<MediaBObj> GetMediaBObj(long mediaId)
        {
            Media media = await _mediaDataHandler.GetMediaById(mediaId);
            return await GetMediaBObj(media);
        }

        public async Task GetAllMedia(GetAllMediaRequest request, ICallback<GetAllMediaResponse> callback)
        {
            try
            {
                List<MediaBObj> mediaList = new List<MediaBObj>();

                foreach (var media in await _mediaDataHandler.GetAllMedia(request.CurrentMediaCount, request.RequiredMediaCount))
                {
                    media.ReleaseDate = media.ReleaseDate.ToLocalTime();
                    mediaList.Add(await GetMediaBObj(media));
                }
                GetAllMediaResponse response = new GetAllMediaResponse(mediaList);
                ZResponse<GetAllMediaResponse> zResponse = new ZResponse<GetAllMediaResponse>(response);
                callback?.OnSuccess(zResponse);
            }
            catch (Exception e)
            {
                callback?.OnFailure(e);
            }
        }
    }
}