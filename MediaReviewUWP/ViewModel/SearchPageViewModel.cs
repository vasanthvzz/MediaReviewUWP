using CommonClassLibrary;
using MediaReviewClassLibrary.Domain;
using MediaReviewClassLibrary.Models;
using MediaReviewUWP.View.Contract;
using MediaReviewUWP.ViewModel.Contract;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MediaReviewUWP.ViewModel
{
    public class SearchPageViewModel : ISearchPageViewModel
    {
        private ISearchPageView _view;

        public SearchPageViewModel(ISearchPageView view)
        {
            _view = view;
        }

        public void UniversalSearch(string searchText)
        {
            UniversalSearchRequest request = new UniversalSearchRequest(searchText);
            UniversalSearchUseCase uc = new UniversalSearchUseCase(request, new UniversalSearchPresenterCallback(this));
            uc.Execute();
        }

        public void SearchOnSuccess(List<MediaBObj> mediaList)
        {
            _view.UpdateSearch(mediaList);
        }
    }

    public class UniversalSearchPresenterCallback : IUniversalSearchPresenterCallback
    {
        private ISearchPageViewModel _vm;

        public UniversalSearchPresenterCallback(ISearchPageViewModel vm)
        {
            _vm = vm;
        }

        public void OnFailure(Exception exception)
        {
            Debug.WriteLine(exception);
        }

        public void OnSuccess(ZResponse<UniversalSearchResponse> response)
        {
            _vm.SearchOnSuccess(response.Data.MediaList);
        }
    }
}