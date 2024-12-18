using CommonClassLibrary;
using MediaReviewClassLibrary.Models.Enitites;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace MediaReviewClassLibrary.Domain
{
    public class CreateUserUseCase : UseCaseBase<CreateUserResponse>
    {
        private CreateUserRequest _request;
        private ICreateUserDataManager _dm;
        public CreateUserUseCase(CreateUserRequest request, ICallback<CreateUserResponse> callback) : base(callback)
        {
            _request = request;
            _dm = MediaReviewDIServiceProvider.GetServiceProvider().GetRequiredService<ICreateUserDataManager>();
        }

        public override void Action()
        {
            _dm.CreateUserAccount(_request, new CreateUserUseCaseCallback(this));
        }
    }

    public class CreateUserRequest
    {
        public string UserName { get; }
        public string ProfilePicture {  get; }
        public string Password { get; }
        public CreateUserRequest(string userName , string password,string profilePicture = "")
        {
            UserName = userName;
            Password = password;
            ProfilePicture = profilePicture;
        }
    }

    public class CreateUserResponse
    {
        public bool Success { get; }
        public UserDetail User { get; }
        public CreateUserResponse(bool success, UserDetail user)
        {
            Success = success;
            User = user;
        }
    }

    public class CreateUserUseCaseCallback : ICallback<CreateUserResponse>
    {
        private CreateUserUseCase _usecase;
        public CreateUserUseCaseCallback(CreateUserUseCase usecase)
        {
            _usecase = usecase;
        }

        public void OnFailure(Exception exception)
        {
            _usecase?.PresenterCallback?.OnFailure(exception);
        }

        public void OnSuccess(ZResponse<CreateUserResponse> response)
        {
            _usecase?.PresenterCallback?.OnSuccess(response);
        }
    }

    public interface ICreateUserDataManager
    {
        Task CreateUserAccount(CreateUserRequest request, CreateUserUseCaseCallback callback);
    }

    public interface ICreateUserPresenterCallback : ICallback<CreateUserResponse> { }
}
