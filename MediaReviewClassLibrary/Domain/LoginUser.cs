using CommonClassLibrary;
using MediaReviewClassLibrary.Models.Enitites;
using System;
using System.Threading.Tasks;

namespace MediaReviewClassLibrary.Domain
{
    public class LoginUserUseCase : UseCaseBase<LoginUserResponse>
    {
        private LoginUserRequest _request;
        private ILoginUserDataManager _dm = MediaReviewDIServiceProvider.GetRequiredService<ILoginUserDataManager>();

        public LoginUserUseCase(LoginUserRequest request, ILoginUserPresenterCallback callback) : base(callback)
        {
            _request = request;
        }

        public override void Action()
        {
            _dm.ValidateUser(_request, new LoginUserUseCaseCallback(this));
        }
    }

    public class LoginUserUseCaseCallback : ICallback<LoginUserResponse>
    {
        private LoginUserUseCase _uc;

        public LoginUserUseCaseCallback(LoginUserUseCase uc)
        {
            _uc = uc;
        }

        public void OnFailure(Exception error)
        {
            _uc?.PresenterCallback?.OnFailure(error);
        }

        public void OnSuccess(ZResponse<LoginUserResponse> response)
        {
            _uc?.PresenterCallback?.OnSuccess(response);
        }
    }

    public interface ILoginUserDataManager
    {
        Task ValidateUser(LoginUserRequest request, LoginUserUseCaseCallback useCaseCallback);
    }

    public class LoginUserRequest
    {
        public string UserName { get; }
        public string Password { get; }

        public LoginUserRequest(string username, string password)
        {
            UserName = username;
            Password = password;
        }
    }

    public class LoginUserResponse
    {
        public bool IsAdmin { get; }
        public bool Success { get; }
        public bool UsernameExist { get; }
        public UserDetail User { get; }

        public LoginUserResponse(bool usernameExist, bool success, UserDetail user, bool isAdmin = false)
        {
            UsernameExist = usernameExist;
            Success = success;
            User = user;
            IsAdmin = isAdmin;
        }
    }

    public interface ILoginUserPresenterCallback : ICallback<LoginUserResponse>
    { }
}