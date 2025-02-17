using CommonClassLibrary;
using MediaReviewClassLibrary.Data;
using MediaReviewClassLibrary.Data.DataHandler.Contract;
using MediaReviewClassLibrary.Domain;
using MediaReviewClassLibrary.Models.Enitites;
using System;
using System.Threading.Tasks;

namespace MediaReviewClassLibrary.DataManager
{
    public class LoginUserDataManager : ILoginUserDataManager
    {
        private IUserDataHandler _userDataHandler = MediaReviewDIServiceProvider.GetRequiredService<IUserDataHandler>();

        public async Task ValidateUser(LoginUserRequest request, LoginUserUseCaseCallback callback)
        {
            try
            {
                bool userExist = await _userDataHandler.IsUserExist(request.UserName);
                bool success = false;
                UserDetail user = null;
                if (userExist)
                {
                    success = await _userDataHandler.IsValidCredential(request.UserName, request.Password);
                    if (success)
                    {
                        user = await _userDataHandler.GetUserByName(request.UserName);
                        SessionManager.SaveUserToStorage(user);
                    }
                }
                LoginUserResponse response = new LoginUserResponse(userExist, success, user);
                ZResponse<LoginUserResponse> zResponse = new ZResponse<LoginUserResponse>(response);
                callback?.OnSuccess(zResponse);
            }
            catch (Exception e)
            {
                callback?.OnFailure(e);
            }
        }
    }
}