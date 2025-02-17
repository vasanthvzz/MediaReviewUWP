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
    public interface IFollowListViewModel
    {
        void ChangeFollow(long userId, bool v, FollowType followType);
        void GetUserFollow(FollowType followType);
        void UpdateFollow(FolloweeMapper updatedFollow);
        void UpdateFollowList(List<UserFollowBObj> followList);
    }
}
