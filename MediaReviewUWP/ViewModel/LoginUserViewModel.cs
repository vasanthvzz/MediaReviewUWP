using CommonClassLibrary;
using MediaReviewClassLibrary.Domain;
using MediaReviewClassLibrary.Models.Enitites;
using MediaReviewUWP.View.Contract;
using MediaReviewUWP.ViewModel.Contract;
using System;
using Windows.UI.Core;

namespace MediaReviewUWP.ViewModel
{
    public class LoginUserViewModel : ILoginUserViewModel
    {
        private ILoginUserView _view;

        public CoreDispatcher Dispatcher { get; set; }

        public LoginUserViewModel(ILoginUserView loginUserControl)
        {
            this._view = loginUserControl;
        }


        public void LoginUser(string username, string password)
        {
            ILoginUserPresenterCallback callback = new LoginUserPresenterCallback(this);
            LoginUserRequest request = new LoginUserRequest(username, password);
            LoginUserUseCase usecase = new LoginUserUseCase(request, callback);
            usecase.Execute();
        }

        private void UserNotExist()
        {
            _view.UsernameNotFound();
        }

        private void UserExist()
        {
            _view.PasswordMissmatch();
        }

        private void ValidationSuccess(UserDetail user)
        {
            _view.ValidationSuccess(user);
        }

        public class LoginUserPresenterCallback : ILoginUserPresenterCallback
        {
            private LoginUserViewModel _presenter;
            public LoginUserPresenterCallback(LoginUserViewModel presenter)
            {
                _presenter = presenter;
            }

            public void OnFailure(Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }

            public void OnSuccess(ZResponse<LoginUserResponse> response)
            {
                var data = response.Data;
                if (data.Success)
                {
                    _presenter.ValidationSuccess(data.User);
                }
                else if (data.UsernameExist)
                {
                    _presenter.UserExist();
                }
                else
                {
                    _presenter.UserNotExist();
                }
            }
        }
    }
}
