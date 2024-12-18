using MediaReviewClassLibrary.Models.Enitites;

namespace MediaReviewUWP.View.Contract
{
    public interface ISignupUserView : IView
    {
        void AccountCreatedSuccess(UserDetail user);
        void AccountCreationFailed();
    }
}
