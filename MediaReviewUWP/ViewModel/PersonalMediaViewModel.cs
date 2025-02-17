using CommonClassLibrary;
using MediaReviewClassLibrary.Domain;
using MediaReviewClassLibrary.Models.Enitites;
using MediaReviewUWP.View.Contract;
using MediaReviewUWP.ViewModel.Contract;
using System;
using System.Diagnostics;

namespace MediaReviewUWP.ViewModel
{
    public class PersonalMediaViewModel : IPersonalMediaViewModel
    {
        private IPersonalMediaControl _view;

        public PersonalMediaViewModel(IPersonalMediaControl view)
        {
            _view = view;
        }

        public void UpdatePersonalMedia(PersonalMedia userPersonalMedia)
        {
            IUpdatePersonalMediaPresenterCallback callback = new UpdatePersonalMediaPresenterCallback(this);
            UpdatePersonalMediaRequest request = new UpdatePersonalMediaRequest(userPersonalMedia);
            UpdatePersonalMediaUseCase usecase = new UpdatePersonalMediaUseCase(request, callback);
            usecase.Execute();
        }

        public void SendUpdatedData(PersonalMedia userPersonalMedia)
        {
            UpdatePersonalMedia(userPersonalMedia);
        }
    }

    public class UpdatePersonalMediaPresenterCallback : IUpdatePersonalMediaPresenterCallback
    {
        private IPersonalMediaViewModel _vm;

        public UpdatePersonalMediaPresenterCallback(IPersonalMediaViewModel vm)
        {
            _vm = vm;
        }

        public void OnSuccess(ZResponse<UpdatePersonalMediaResponse> response)
        {
            _vm.SendUpdatedData(response.Data.UpdatedPersonalMedia);
        }

        public void OnFailure(Exception exception)
        {
            Debug.WriteLine(exception);
        }
    }
}