using MediaReviewClassLibrary.Models;
using MediaReviewClassLibrary.Models.Constants;
using MediaReviewClassLibrary.Models.Enitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
