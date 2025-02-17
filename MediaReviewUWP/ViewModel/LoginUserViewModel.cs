using CommonClassLibrary;
using MediaReviewClassLibrary.Data;
using MediaReviewClassLibrary.Domain;
using MediaReviewUWP.View.Contract;
using MediaReviewUWP.ViewModel.Contract;
using System;
using Windows.UI.Core;

namespace MediaReviewUWP.ViewModel
{
    public class LoginUserViewModel : ILoginUserViewModel
    {
        private ILoginUserView _view;

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

        private void ValidationFailure()
        {
            _view.LoginFailure();
        }

        private void ValidationSuccess()
        {
            _view.LoginSuccess();
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
                    SessionManager.User = data.User;
                    _presenter.ValidationSuccess();
                }
                else
                {
                    _presenter.ValidationFailure();
                }
            }
        }
    }
}