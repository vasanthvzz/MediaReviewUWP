using CommonClassLibrary;
using MediaReviewClassLibrary.Models;
using MediaReviewClassLibrary.Models.Constants;
using MediaReviewClassLibrary.Models.Enitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaReviewUWP.ViewModel.Contract
{
    public interface IPersonalisedMediaViewModel
    {
        void GetPersonlisedMedia(PersonalMediaType personalisedMediaType);
        void SendData(List<MediaBObj> mediaList);
        void RemoveFromMediaList(long mediaId);
        void RemovePersonalisedMedia(long mediaId, PersonalMediaType personalisedMediaType);
    }
}
