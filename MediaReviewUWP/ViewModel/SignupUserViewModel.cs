using CommonClassLibrary;
using MediaReviewClassLibrary.Domain;
using MediaReviewClassLibrary.Models.Enitites;
using MediaReviewUWP.View.Contract;
using MediaReviewUWP.ViewModel.Contract;
using System;

namespace MediaReviewUWP.ViewModel
{
    public class SignupUserViewModel : ISignupUserViewModel
    {
        private ISignupUserView _view;

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
            _view.RedirectOnSuccess(user);
        }

        public void LoginFail()
        {
            _view.ShowErrorMessage();
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
