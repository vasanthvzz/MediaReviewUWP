using MediaReviewUWP.View.Contract;
using MediaReviewUWP.ViewModel.Contract;

namespace MediaReviewUWP.ViewModel
{
    public class ShowMediaListViewModel : IShowMediaListViewModel
    {
        private IShowMediaListView _view;
        public ShowMediaListViewModel(IShowMediaListView view)
        {
            _view = view;   
        }

        public void GetPresentMediaDetails()
        {

        }
    }
}
