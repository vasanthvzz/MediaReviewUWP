using MediaReviewClassLibrary.Models;
using MediaReviewClassLibrary.Models.Constants;
using MediaReviewClassLibrary.Models.Enitites;
using MediaReviewUWP.View.Contract;
using MediaReviewUWP.ViewModel;
using MediaReviewUWP.ViewModel.Contract;
using MediaReviewUWP.ViewObject;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MediaReviewUWP.View.HomePageView
{
    public sealed partial class FollowListControl : Page , IFollowListControlView , ITabItemContent
    {
        public ObservableCollection<UserFollowVObj> FollowList {  get; set; }
        private IFollowListViewModel _vm;
        private FollowType _followType;

        public FollowListControl()
        {
            _vm = new FollowListViewModel(this);
            FollowList = new ObservableCollection<UserFollowVObj>();
            this.InitializeComponent();
        }

        public void Init(FollowType followType)
        {
            _followType = followType;
            _vm.GetUserFollow(_followType);
        }

        public void UpdateFollow(List<UserFollowBObj> followList)
        {
            _ = Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                FollowList.Clear();
                foreach (var item in followList)
                {
                    FollowList.Add(new UserFollowVObj(item));
                }
                NoFollowTb.Visibility = FollowList.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
            });
        }

        private void FollowButton_Click(object sender, RoutedEventArgs e)
        {
            if(sender is Button b && b.DataContext is  UserFollowVObj userFollow)
            {
                _vm.ChangeFollow(userFollow.UserId,!userFollow.IsFollow, _followType);
            }
        }

        public void UpdateFollow(FolloweeMapper updatedFollow)
        {
            if(updatedFollow == null)
            {
                return;
            }
            foreach (var follow in FollowList)
            {
                if ((_followType == FollowType.FOLLOWEE && follow.UserId == updatedFollow.FolloweeId) ||
                    (_followType == FollowType.FOLLOWER && follow.UserId == updatedFollow.UserId))
                {
                    _ = Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => follow.IsFollow = updatedFollow.IsFollow);
                }
            }
        }

        public void ReloadPageContent()
        {
            _vm.GetUserFollow(_followType);
        }
    }
}