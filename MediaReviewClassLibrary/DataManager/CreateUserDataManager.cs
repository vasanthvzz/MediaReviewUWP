using CommonClassLibrary;
using MediaReviewClassLibrary.Data;
using MediaReviewClassLibrary.Data.DataHandler.Contract;
using MediaReviewClassLibrary.Domain;
using MediaReviewClassLibrary.Models.Enitites;
using System;
using System.Threading.Tasks;

namespace MediaReviewClassLibrary.DataManager
{
    public class CreateUserDataManager : ICreateUserDataManager
    {
        private IUserDataHandler _userDataHandler = MediaReviewDIServiceProvider.GetRequiredService<IUserDataHandler>();

        public async Task CreateUserAccount(CreateUserRequest request, CreateUserUseCaseCallback callback)
        {
            try
            {
                string profilePicture = "";
                if (request.ProfilePicture != null)
                {
                    profilePicture = request.ProfilePicture;
                }
                UserDetail user = await _userDataHandler.CreateUser(request.UserName, request.Password, profilePicture);
                bool success = user != null;
                if (success)
                {
                    SessionManager.SaveUserToStorage(user);
                }
                CreateUserResponse response = new CreateUserResponse(success, user);
                ZResponse<CreateUserResponse> zResponse = new ZResponse<CreateUserResponse>(response);
                callback?.OnSuccess(zResponse);
            }
            catch (Exception ex)
            {
                callback?.OnFailure(ex);
            }
        }
    }
}