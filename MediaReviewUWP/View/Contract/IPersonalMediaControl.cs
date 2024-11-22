using MediaReviewClassLibrary.Models.Enitites;

namespace MediaReviewUWP.View.Contract
{
    public interface IPersonalMediaControl : IView
    {
        void UpdatePersonalMedia(PersonalMedia personalMedia);
    }
}
