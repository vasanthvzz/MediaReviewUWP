using MediaReviewClassLibrary.Models;
using MediaReviewClassLibrary.Models.Constants;
using System.Collections.Generic;

namespace MediaReviewUWP.View.Contract
{
    public interface IPersonalisedMediaView : IView
    {
        PersonalMediaType PersonalisedMediaType { get; }

        void Init(PersonalMediaType personalMediaType);

        void RemoveMedia(long mediaId);

        void UpdateMedia(List<MediaBObj> mediaList);
    }
}