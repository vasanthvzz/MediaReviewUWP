using MediaReviewClassLibrary.Models.Enitites;

namespace MediaReviewUWP.View.Contract
{
    public interface ILoginUserView : IView
    {
        void PasswordMissmatch();
        void UsernameNotFound();
        void ValidationSuccess(UserDetail user);

    }
}
