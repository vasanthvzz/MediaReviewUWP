using MediaReviewUWP.View.Contract;
using MediaReviewUWP.ViewModel;
using MediaReviewUWP.ViewModel.Contract;
using MediaReviewUWP.ViewObject;
using System;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;


namespace MediaReviewUWP.View.MediaPageView
{
    public sealed partial class MediaDetailControl : UserControl, IMediaDetailControl
    {
        private IMediaDetailViewModel _vm;
        public event EventHandler<MediaRatingChangeEventArgs> MediaRatingChanged;

        public MediaDetailVObj MediaDetail
        {
            get
            { return this.DataContext as MediaDetailVObj; }
            set { this.DataContext = value; }
        }

        private void OnMediaRatingChanged(float mediaRating)
        {
            if (MediaDetail != null)
            {
                var eventArgs = new MediaRatingChangeEventArgs(mediaRating);
                MediaRatingChanged?.Invoke(this, eventArgs);
            }
        }

        public MediaDetailControl()
        {
            this.InitializeComponent();
            _vm = new MediaDetailViewModel(this);
            this.DataContextChanged += (s, e) => { 
                Bindings.Update();
                InitAfterDetailFetched();
                PersonalMediaContentComponent.UserPersonalMedia = MediaDetail?.UserPersonalMedia;
                UserRatingComponent.UserRating = MediaDetail?.UserRating;
            };
        }

        private void UserControl_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            UserRatingComponent.UserRatingChanged -= UserRatingChanged;
            UserRatingComponent.UserRatingChanged += UserRatingChanged;
        }

        private void UserRatingChanged(object sender, UserRatingChangedEventArgs e)
        {
            _vm.GetMediaRating(MediaDetail.MediaId);
        }

        private void InitAfterDetailFetched()
        {
            if (MediaDetail != null && MediaDetail.Runtime.Trim().Length == 0)
            {
                FirstDotIcon.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
        }

        private void MediaImage_ImageFailed(object sender, Windows.UI.Xaml.ExceptionRoutedEventArgs e)
        {
            if (sender is Image image)
            {
                image.Source = new BitmapImage(new Uri("ms-appx:///Assets/DefaultMediaImage.png"));
                image.Stretch = Windows.UI.Xaml.Media.Stretch.Uniform;
            }
        }

        public async void UpdateMediaRating(float mediaRating)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
            {
                MediaDetail.MediaRating = mediaRating;
                OnMediaRatingChanged(mediaRating);
            }
            );
        }
    }

    public class MediaRatingChangeEventArgs : EventArgs 
    {
        public float MediaRating {  get; set; }

        public MediaRatingChangeEventArgs(float mediaRating)
        {
            MediaRating = mediaRating;
        }
    }
}