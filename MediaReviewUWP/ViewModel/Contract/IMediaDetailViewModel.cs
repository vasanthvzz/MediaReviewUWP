using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaReviewUWP.ViewModel.Contract
{
    public interface IMediaDetailViewModel
    {
        void GetMediaRating(long mediaId);
        void SendUpdatedMediaRating(float MediaRating);
    }
}
