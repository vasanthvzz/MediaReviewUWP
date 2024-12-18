using MediaReviewClassLibrary.Models.Enitites;

namespace MediaReviewUWP.View.Contract
{
    public interface ILoginUserView : IView
    {
        void LoginFailure();
        void LoginSuccess();

    }
}
