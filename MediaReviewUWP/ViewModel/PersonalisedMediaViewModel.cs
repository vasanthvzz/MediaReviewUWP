using CommonClassLibrary;
using MediaReviewClassLibrary;
using MediaReviewClassLibrary.Domain;
using MediaReviewClassLibrary.Models;
using MediaReviewClassLibrary.Models.Constants;
using MediaReviewClassLibrary.Models.Enitites;
using MediaReviewClassLibrary.Utlis;
using MediaReviewUWP.View.Contract;
using MediaReviewUWP.ViewModel.Contract;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MediaReviewUWP.ViewModel
{
    public class PersonalisedMediaViewModel : IPersonalisedMediaViewModel
    {
        private IPersonalisedMediaView _view;
        private ISessionManager _sessionManager = MediaReviewDIServiceProvider.GetServiceProvider().GetRequiredService<ISessionManager>();

        public PersonalisedMediaViewModel(IPersonalisedMediaView personalisedMediaControl)
        {
            this._view = personalisedMediaControl;
        }

        public void GetPersonlisedMedia(PersonalMediaType personalisedMediaType)
        {
            var userId = _sessionManager.RetriveUserFromStorage().UserId;
            GetPersonalisedMediaRequest request = new GetPersonalisedMediaRequest(userId, personalisedMediaType);

            GetPersonlisedMediaPresenterCallback callback = new GetPersonlisedMediaPresenterCallback(this);
            GetPersonalisedMediaUseCase uc = new GetPersonalisedMediaUseCase(request, callback);
            uc.Execute();
        }

        public void RemovePersonalisedMedia(long mediaId,PersonalMediaType personalisedMediaType) 
        {
            var userId = _sessionManager.RetriveUserFromStorage().UserId;
            RemovePersonalisedMediaRequest request = new RemovePersonalisedMediaRequest(userId,mediaId, personalisedMediaType);
            RemovePersonalisedMediaPresenterCallback callback = new RemovePersonalisedMediaPresenterCallback(this);
            RemovePersonalisedMediaUseCase uc = new RemovePersonalisedMediaUseCase(request, callback);
            uc.Execute();

        }

        public void SendData(List<MediaBObj> mediaList)
        {
            _view.UpdateMedia(mediaList);
        }

        public void RemoveFromMediaList(long mediaId) 
        {
            _view.RemoveMedia(mediaId);
        }
    }

    public class GetPersonlisedMediaPresenterCallback : IGetPersonalisedMediaPresenterCallback
    {
        private IPersonalisedMediaViewModel _vm;
        public GetPersonlisedMediaPresenterCallback(IPersonalisedMediaViewModel vm) 
        {
            _vm = vm;   
        }

        public void OnFailure(Exception exception)
        {
            Debug.WriteLine(exception);
        }

        public void OnSuccess(ZResponse<GetPersonalisedMediaResponse> response)
        {
            _vm.SendData(response.Data.MediaList);
        }
    }

    public class RemovePersonalisedMediaPresenterCallback : IRemovePersonalisedMediaPresenterCallback
    {
        private IPersonalisedMediaViewModel _vm;
        private PersonalisedMediaViewModel personalisedMediaViewModel;

        public RemovePersonalisedMediaPresenterCallback(IPersonalisedMediaViewModel vm)
        {
            _vm = vm;
        }

        public void OnFailure(Exception exception)
        {
            Debug.WriteLine(exception);
        }

        public void OnSuccess(ZResponse<RemovePersonalisedMediaResponse> response)
        {
            if (response.Data != null && response.Data.Success)
            {
                _vm.RemoveFromMediaList(response.Data.MediaId);
            }
        }
    }
}
