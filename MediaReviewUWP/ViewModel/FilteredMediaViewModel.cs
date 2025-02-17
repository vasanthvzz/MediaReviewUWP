using CommonClassLibrary;
using MediaReviewClassLibrary.Domain;
using MediaReviewClassLibrary.Models;
using MediaReviewClassLibrary.Models.Enitites;
using MediaReviewUWP.View.Contract;
using MediaReviewUWP.ViewModel.Contract;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaReviewUWP.ViewModel
{
    public class FilteredMediaViewModel : IFilteredMediaViewModel
    {
        private IFilteredMediaPage _view;
        
        public FilteredMediaViewModel(IFilteredMediaPage view)
        {
            _view = view;
        }

        public void GetFilteredMedia(List<Genre> genreList)
        {
            GetFilteredMediaPresenterCallback callback = new GetFilteredMediaPresenterCallback(this);
            GetFilteredMediaRequest request =new GetFilteredMediaRequest(genreList);
            GetFilteredMediaUseCase uc = new GetFilteredMediaUseCase(request, callback);
            uc.Execute();
        }

        public void FilteredMediaFetched(List<MediaBObj> filteredMediaList)
        {
            _view.UpdateMediaList(filteredMediaList);
        }

        private class GetFilteredMediaPresenterCallback : IGetFilteredMediaPresenterCallback
        {
            private IFilteredMediaViewModel _vm;

            public GetFilteredMediaPresenterCallback(IFilteredMediaViewModel vm)
            {
                _vm = vm;
            }

            public void OnFailure(Exception exception)
            {
                Debug.WriteLine(exception);
            }

            public void OnSuccess(ZResponse<GetFilteredMediaResponse> response)
            {
                _vm.FilteredMediaFetched(response?.Data?.FilteredMediaList);
            }
        }
    }
}
