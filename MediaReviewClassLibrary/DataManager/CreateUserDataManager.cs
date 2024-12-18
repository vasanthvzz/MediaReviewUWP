using CommonClassLibrary;
using MediaReviewClassLibrary.Data.DataHandler.Contract;
using MediaReviewClassLibrary.Domain;
using MediaReviewClassLibrary.Models.Enitites;
using MediaReviewClassLibrary.Utlis;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace MediaReviewClassLibrary.DataManager
{
    public class CreateUserDataManager : ICreateUserDataManager
    {
        private IUserDataHandler _userDataHandler;
        private ISessionManager _sessionManager;

        public CreateUserDataManager()
        {
            _userDataHandler = MediaReviewDIServiceProvider.GetServiceProvider().GetRequiredService<IUserDataHandler>();
            _sessionManager = MediaReviewDIServiceProvider.GetServiceProvider().GetRequiredService<ISessionManager>();
        }

        public async Task CreateUserAccount(CreateUserRequest request, CreateUserUseCaseCallback callback)
        {
            try
            {
                string profilePicture = "";
                if (request.ProfilePicture == null || request.ProfilePicture == "")
                {
                    profilePicture =  await AvatarGenerator.GenerateImage(request.UserName);
                }
                UserDetail user = await _userDataHandler.CreateUser(request.UserName, request.Password,profilePicture);
                bool success = user != null;
                if (success)
                {
                    _sessionManager.SaveUserToStorage(user);
                }
                CreateUserResponse response = new CreateUserResponse(success, user);
                ZResponse<CreateUserResponse> zResponse = new ZResponse<CreateUserResponse>(response);
                callback.OnSuccess(zResponse);
            }
            catch (Exception ex)
            {
                callback?.OnFailure(ex);
            }
        }
    }
}
