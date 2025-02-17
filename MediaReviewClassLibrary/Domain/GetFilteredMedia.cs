using CommonClassLibrary;
using MediaReviewClassLibrary.Models;
using MediaReviewClassLibrary.Models.Enitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaReviewClassLibrary.Domain
{
    public class GetFilteredMediaUseCase : UseCaseBase<GetFilteredMediaResponse>
    {
        private GetFilteredMediaRequest _request;
        private IGetFilteredMediaDataManager _dm = MediaReviewDIServiceProvider.GetRequiredService<IGetFilteredMediaDataManager>();
      
        public GetFilteredMediaUseCase(GetFilteredMediaRequest request, ICallback<GetFilteredMediaResponse> callback) : base(callback)
        {
            _request = request;
        }

        public override void Action()
        {
            _dm.GetFilteredMedia(_request, new GetFilteredMediaUseCaseCallback(this));
        }
    }

    public interface IGetFilteredMediaDataManager
    {
        Task GetFilteredMedia(GetFilteredMediaRequest request, GetFilteredMediaUseCaseCallback getFilteredMediaUseCaseCallback);
    }


    public class GetFilteredMediaUseCaseCallback : ICallback<GetFilteredMediaResponse>
    {
        private GetFilteredMediaUseCase _uc;

        public GetFilteredMediaUseCaseCallback(GetFilteredMediaUseCase uc)
        {
            _uc = uc;
        }

        public void OnFailure(Exception exception)
        {
            _uc?.PresenterCallback?.OnFailure(exception);
        }

        public void OnSuccess(ZResponse<GetFilteredMediaResponse> response)
        {
            _uc?.PresenterCallback?.OnSuccess(response);
        }
    }

    public class GetFilteredMediaRequest
    {
        public List<Genre> GenreList {  get; set; }

        public GetFilteredMediaRequest(List<Genre> genreList)
        {
            GenreList = genreList;
        }
    }

    public class GetFilteredMediaResponse
    {
        public List<MediaBObj> FilteredMediaList {  get; set; } 

        public GetFilteredMediaResponse(List<MediaBObj> filteredMediaList)
        {
            FilteredMediaList = filteredMediaList;
        }
    }

    public interface IGetFilteredMediaPresenterCallback : ICallback<GetFilteredMediaResponse> { }

}
