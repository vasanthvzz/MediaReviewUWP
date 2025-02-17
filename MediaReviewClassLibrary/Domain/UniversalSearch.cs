using CommonClassLibrary;
using MediaReviewClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediaReviewClassLibrary.Domain
{
    public class UniversalSearchUseCase : UseCaseBase<UniversalSearchResponse>
    {
        private UniversalSearchRequest _request;
        private IUniversalSearchDataManager _dm = MediaReviewDIServiceProvider.GetRequiredService<IUniversalSearchDataManager>();

        public UniversalSearchUseCase(UniversalSearchRequest request, ICallback<UniversalSearchResponse> callback) : base(callback)
        {
            _request = request;
        }

        public override void Action()
        {
            _dm.Search(_request, new UniversalSearchUseCaseCallback(this));
        }
    }

    public class UniversalSearchUseCaseCallback : ICallback<UniversalSearchResponse>
    {
        private UniversalSearchUseCase _uc;

        public UniversalSearchUseCaseCallback(UniversalSearchUseCase uc)
        {
            _uc = uc;
        }

        public void OnFailure(Exception exception)
        {
            _uc?.PresenterCallback?.OnFailure(exception);
        }

        public void OnSuccess(ZResponse<UniversalSearchResponse> response)
        {
            _uc.PresenterCallback?.OnSuccess(response);
        }
    }

    public class UniversalSearchRequest
    {
        public string SearchString { get; set; }

        public UniversalSearchRequest(string searchString)
        {
            SearchString = searchString;
        }
    }

    public class UniversalSearchResponse
    {
        public List<MediaBObj> MediaList { get; set; }

        public UniversalSearchResponse(List<MediaBObj> mediaList)
        {
            MediaList = mediaList;
        }
    }

    public interface IUniversalSearchDataManager
    {
        Task Search(UniversalSearchRequest request, ICallback<UniversalSearchResponse> callback);
    }

    public interface IUniversalSearchPresenterCallback : ICallback<UniversalSearchResponse>
    { }
}