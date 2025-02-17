using MediaReviewClassLibrary.Models;
using MediaReviewClassLibrary.Models.Constants;
using System.Collections.Generic;

namespace MediaReviewUWP.ViewModel.Contract
{
    public interface IPersonalisedMediaViewModel
    {
        void GetPersonalisedMedia(PersonalMediaType personalisedMediaType);

        void SendData(List<MediaBObj> mediaList);

        void RemoveFromMediaList(long mediaId);

        void RemovePersonalisedMedia(long mediaId, PersonalMediaType personalisedMediaType);
    }
}