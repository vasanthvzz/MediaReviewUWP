using System.Threading.Tasks;

namespace MediaReviewUWP.View.Contract
{
    public interface ILoginUserView : IView
    {
        Task LoginFailure();

        void LoginSuccess();
    }
}