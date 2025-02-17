using MediaReviewClassLibrary.Models.Enitites;

namespace MediaReviewUWP.ViewModel.Contract
{
    public interface IPersonalMediaViewModel
    {
        void UpdatePersonalMedia(PersonalMedia userPersonalMedia);

        void SendUpdatedData(PersonalMedia userPersonalMedia);
    }
}