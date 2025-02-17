using CommonClassLibrary;
using MediaReviewClassLibrary.Models.Enitites;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediaReviewClassLibrary.Domain
{
    public class GetAllGenreUseCase : UseCaseBase<GetAllGenreResponse>
    {
        private IGetAllGenreDataManager _dm = MediaReviewDIServiceProvider.GetRequiredService<IGetAllGenreDataManager>();

        public GetAllGenreUseCase(IGetAllGenrePresenterCallback callback) : base(callback) { }

        public override void Action()
        {
            _dm.GetAllGenre(new GetAllGenreUseCaseCallback(this));
        }
    }

    public class GetAllGenreResponse
    {
        public List<Genre> GenreList { get; set; }

        public GetAllGenreResponse(List<Genre> genreList)
        {
            GenreList = genreList;
        }
    }

    public class GetAllGenreUseCaseCallback : ICallback<GetAllGenreResponse>
    {
        private GetAllGenreUseCase _uc;

        public GetAllGenreUseCaseCallback(GetAllGenreUseCase uc)
        {
            _uc = uc;
        }

        public void OnFailure(Exception exception)
        {
            _uc?.PresenterCallback?.OnFailure(exception);
        }

        public void OnSuccess(ZResponse<GetAllGenreResponse> response)
        {
            _uc?.PresenterCallback?.OnSuccess(response);
        }
    }

    public interface IGetAllGenreDataManager
    {
        Task GetAllGenre(ICallback<GetAllGenreResponse> callback);
    }

    public interface IGetAllGenrePresenterCallback : ICallback<GetAllGenreResponse>
    { }
}