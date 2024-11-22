using MediaReviewClassLibrary;
using MediaReviewClassLibrary.Data;
using MediaReviewClassLibrary.Models;
using MediaReviewClassLibrary.Utlis;
using MediaReviewUWP.ViewObjects;
using Microsoft.Extensions.DependencyInjection;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace MediaReviewUWP.View.MediaPageView
{
    public sealed partial class ReviewListItem : UserControl
    {
        private ISessionManager _sessionManager = MediaReviewDIServiceProvider.GetServiceProvider().GetRequiredService<ISessionManager>();
        public ReviewListItem()
        {
            this.InitializeComponent();
            this.DataContextChanged += (s, e) =>
            {
                Bindings.Update();
                RefreshFollowButton();
            };
        }

        private void RefreshFollowButton()
        {
            if(UserReview == null)
            {
                return;
            }
            if(UserReview?.UserId == _sessionManager.RetriveUserFromStorage().UserId)
            {
                FollowButton.Visibility = Visibility.Collapsed;
            }
            if (UserReview.Following)
            {
                FollowButton.Content = "following";
            }
            else
            {
                FollowButton.Content = "follow";
            }
        }

        public MediaReviewVObj UserReview
        {
            get
            { return this.DataContext as MediaReviewVObj; }
        }


    }
}
