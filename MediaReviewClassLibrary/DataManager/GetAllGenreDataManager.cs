using CommonClassLibrary;
using MediaReviewClassLibrary.Data.DataHandler.Contract;
using MediaReviewClassLibrary.Domain;
using System;
using System.Threading.Tasks;

namespace MediaReviewClassLibrary.DataManager
{
    public class GetAllGenreDataManager : IGetAllGenreDataManager
    {
        private IGenreDataHandler _genreDataHandler = MediaReviewDIServiceProvider.GetRequiredService<IGenreDataHandler>();

        public async Task GetAllGenre(ICallback<GetAllGenreResponse> callback)
        {
            try
            {
                ZResponse<GetAllGenreResponse> response = new ZResponse<GetAllGenreResponse>(new GetAllGenreResponse(await _genreDataHandler.GetAllGenre()));
                callback?.OnSuccess(response);
            }
            catch (Exception ex)
            {
                callback?.OnFailure(ex);
            }
        }
    }
}