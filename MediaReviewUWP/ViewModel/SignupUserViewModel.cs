using CommonClassLibrary;
using MediaReviewClassLibrary;
using MediaReviewClassLibrary.Domain;
using MediaReviewClassLibrary.Models.Enitites;
using MediaReviewClassLibrary.Utlis;
using MediaReviewUWP.View.Contract;
using MediaReviewUWP.ViewModel.Contract;
using System;

namespace MediaReviewUWP.ViewModel
{
    public class SignupUserViewModel : ISignupUserViewModel
    {
        private ISignupUserView _view;
        private ISessionManager _sessionManager = MediaReviewDIServiceProvider.GetRequiredService<ISessionManager>();   

        public SignupUserViewModel(ISignupUserView view)
        {
            _view = view;
        }

        public void CreateUser(string username, string password,string profilePicture = "")
        {
            ICreateUserPresenterCallback presenter = new CreateUserPresenterCallback(this);
            CreateUserRequest request = new CreateUserRequest(username, password, profilePicture);
            CreateUserUseCase usecase = new CreateUserUseCase(request, presenter);
            usecase.Execute();
        }

        public void LoginSuccess(UserDetail user)
        {
            _sessionManager.SaveUserToStorage(user);
            _view.AccountCreatedSuccess(user);
        }

        public void LoginFail()
        {
            _view.AccountCreationFailed();
        }
    }

    public class CreateUserPresenterCallback : ICreateUserPresenterCallback
    {
        ISignupUserViewModel _presenter;
        public CreateUserPresenterCallback(ISignupUserViewModel view)
        {
            _presenter = view;
        }
        public void OnFailure(Exception exception)
        {
            Console.WriteLine(exception.ToString());
        }

        public void OnSuccess(ZResponse<CreateUserResponse> response)
        {

            var data = response.Data;
            if (data.Success)
            {
                _presenter?.LoginSuccess(data.User);
            }
            else
            {
                _presenter?.LoginFail();
            }
        }
    }
}
