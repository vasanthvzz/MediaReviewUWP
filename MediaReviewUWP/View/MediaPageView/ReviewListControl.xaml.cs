using MediaReviewClassLibrary.Models;
using MediaReviewClassLibrary.Models.Enitites;
using MediaReviewUWP.View.Contract;
using MediaReviewUWP.ViewModel;
using MediaReviewUWP.ViewModel.Contract;
using MediaReviewUWP.ViewObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;


namespace MediaReviewUWP.View.MediaPageView
{
    public sealed partial class ReviewListControl : UserControl, IReviewListView
    {
        public ObservableCollection<MediaReviewVObj> ReviewList { get; set; }
        public long MediaId {  get; set; }
        private IReviewListViewModel _vm;

        public ReviewListControl()
        {
            _vm = new ReviewListViewModel(this);
            this.InitializeComponent();
            ReviewList = new ObservableCollection<MediaReviewVObj>();
        }

        public void RetriveMediaReviews(long mediaId)
        {
            MediaId = mediaId;
            _vm.GetMediaReviews(MediaId);
        }

        public void UpdateMediaReviews(List<MediaReviewVObj> mediaReviews)
        {
            foreach (var review in mediaReviews)
            {
                UpdateMediaReviews(review);
            }
        }

        public async void UpdateMediaReviews(MediaReviewVObj mediaReview)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                ReviewList.Add(mediaReview);
            });
        }
    }
}
