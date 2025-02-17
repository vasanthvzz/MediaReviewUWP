using CommonClassLibrary;
using MediaReviewClassLibrary.Data.DataHandler.Contract;
using MediaReviewClassLibrary.Domain;
using MediaReviewClassLibrary.Models.Enitites;
using MediaReviewClassLibrary.Utility;
using System;
using System.Threading.Tasks;

namespace MediaReviewClassLibrary.DataManager
{
    public class AddMediaDataManager : IAddMediaDataManager
    {
        private IMediaDataHandler _mediaDataHandler = MediaReviewDIServiceProvider.GetRequiredService<IMediaDataHandler>();
        private IGenreDataHandler _genreDataHandler = MediaReviewDIServiceProvider.GetRequiredService<IGenreDataHandler>();

        public async Task AddMedia(AddMediaRequest request, ICallback<AddMediaResponse> callback)
        {
            try
            {
                var addMedia = request.AddMedia;
                bool success = false;
                long mediaId = IdentityManager.GenerateUniqueId();
                if (addMedia.Title != null && addMedia.ReleaseDate != null && ! _mediaDataHandler.IsMediaExist(addMedia.Title, addMedia.ReleaseDate.Date))
                {
                    Media media = new Media(mediaId, addMedia.Title, addMedia.Description, addMedia.TileImage, addMedia.PosterImage, addMedia.ReleaseDate, addMedia.Runtime);
                    await _mediaDataHandler.AddMedia(media);
                    success = _mediaDataHandler.GetMediaById(mediaId) != null;
                    if (success && addMedia.GenreList != null)
                    {
                        foreach (Genre genre in addMedia.GenreList)
                        {
                            GenreMapper mapper = new GenreMapper(genre.GenreId, mediaId);
                            _genreDataHandler.AddGenre(mapper);
                        }
                    }
                }
                ZResponse<AddMediaResponse> response = new ZResponse<AddMediaResponse>(new AddMediaResponse(await _mediaDataHandler.GetMediaById(mediaId),success));
                callback?.OnSuccess(response);
            }
            catch (Exception e)
            {
                callback?.OnFailure(e);
            }
        }
    }
}