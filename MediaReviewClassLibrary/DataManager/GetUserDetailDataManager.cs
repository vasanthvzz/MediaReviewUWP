using CommonClassLibrary;
using MediaReviewClassLibrary.Data.DataHandler.Contract;
using MediaReviewClassLibrary.Domain;
using System;
using System.Threading.Tasks;

namespace MediaReviewClassLibrary.DataManager
{
    public class GetUserDetailDataManager : IGetUserDetailDataManager
    {
        private IUserDataHandler _userDataHandler = MediaReviewDIServiceProvider.GetRequiredService<IUserDataHandler>();

        public async Task GetUserDetail(GetUserDetailRequest request, GetUserDetailUseCaseCallback callback)
        {
            try
            {
                var user = await _userDataHandler.GetUserById(request.UserId);
                ZResponse<GetUserDetailResponse> response = new ZResponse<GetUserDetailResponse>(new GetUserDetailResponse(user));
                callback?.OnSuccess(response);
            }
            catch (Exception e)
            {
                callback?.OnFailure(e);
            }
        }
    }
}
