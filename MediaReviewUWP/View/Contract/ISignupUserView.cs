using MediaReviewClassLibrary.Models.Enitites;

namespace MediaReviewUWP.View.Contract
{
    public interface ISignupUserView : IView
    {
        void RedirectOnSuccess(UserDetail user);
        void ShowErrorMessage();
    }
}
