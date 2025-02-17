using MediaReviewClassLibrary.Models.Enitites;
using System.Threading.Tasks;

namespace MediaReviewUWP.View.Contract
{
    public interface ISignupUserView : IView
    {
        void AccountCreatedSuccess(UserDetail user);

        Task AccountCreationFailed();
    }
}