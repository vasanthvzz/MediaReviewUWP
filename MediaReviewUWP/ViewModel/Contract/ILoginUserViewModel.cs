using Windows.UI.Core;

namespace MediaReviewUWP.ViewModel.Contract
{
    public interface ILoginUserViewModel
    {
        CoreDispatcher Dispatcher { get; set; }
        void LoginUser(string username, string password);
    }
}
