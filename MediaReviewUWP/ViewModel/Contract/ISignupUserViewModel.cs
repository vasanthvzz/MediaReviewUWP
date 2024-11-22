using MediaReviewClassLibrary.Models.Enitites;

namespace MediaReviewUWP.ViewModel.Contract
{
    public interface ISignupUserViewModel
    {
        void CreateUser(string username, string password, string profilePicture = "");
        void LoginFail();
        void LoginSuccess(UserDetail user);
    }
}
