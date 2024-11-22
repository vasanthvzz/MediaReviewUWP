using Windows.UI.Core;

namespace MediaReviewUWP.View.Contract
{
    public interface IView
    {
        CoreDispatcher Dispatcher { get; }
    }
}
