using CommonClassLibrary;
using MediaReviewClassLibrary.Data.DataHandler.Contract;
using MediaReviewClassLibrary.Domain;
using MediaReviewClassLibrary.Models;
using MediaReviewClassLibrary.Models.Enitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaReviewClassLibrary.DataManager
{
    public class GetFilteredMediaDataManager : IGetFilteredMediaDataManager
    {
        private IGetAllMediaDataManager _getAllMediaDataManager = MediaReviewDIServiceProvider.GetRequiredService<IGetAllMediaDataManager>();
        private IGenreDataHandler _genreDataHandler = MediaReviewDIServiceProvider.GetRequiredService<IGenreDataHandler>();

        public async Task GetFilteredMedia(GetFilteredMediaRequest request, GetFilteredMediaUseCaseCallback callback)
        {
            try
            {
                List<Genre> genreList = request.GenreList;
                List<long> mediaIds = new List<long>();
                foreach (Genre genre in genreList)
                {
                    var mediaIdsForGenre = await _genreDataHandler.GetMediaIdsByGenreId(genre.GenreId);
                    mediaIds.AddRange(mediaIdsForGenre ?? new List<long>());
                }
                var filterMediaIds = mediaIds.Distinct();
                var resultMedia = new List<MediaBObj>();
                foreach( long mediaId in filterMediaIds)
                {
                    resultMedia.Add(await _getAllMediaDataManager.GetMediaBObj(mediaId));
                }
                ZResponse<GetFilteredMediaResponse> response = new ZResponse<GetFilteredMediaResponse>(new GetFilteredMediaResponse(resultMedia));
                callback?.OnSuccess(response);
            }
            catch (Exception e)
            {
                callback?.OnFailure(e);
            }
        }
    }
}
