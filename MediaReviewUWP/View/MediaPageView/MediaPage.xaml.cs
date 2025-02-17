using MediaReviewClassLibrary.Models;
using MediaReviewUWP.Utility;
using MediaReviewUWP.View.Contract;
using MediaReviewUWP.ViewModel;
using MediaReviewUWP.ViewModel.Contract;
using MediaReviewUWP.ViewObject;
using Microsoft.Graphics.Canvas.Effects;
using System;
using System.Numerics;
using System.Threading.Tasks;
using Windows.UI.Composition;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Media.Imaging;

namespace MediaReviewUWP.View.MediaPageView
{
    public sealed partial class MediaPage : Page, IMediaPage, ITabItemContent
    {
        private IMediaPageViewModel _viewModel;

        public MediaPageVObj MediaDetail
        {
            get
            { return this.DataContext as MediaPageVObj; }
            set { this.DataContext = value; }
        }

        public MediaPage()
        {
            _viewModel = new MediaPageViewModel(this);
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MediaDetailControlComponent.UserRatingComponent.UserRatingChanged -= UserRatingChanged;
            MediaDetailControlComponent.UserRatingComponent.UserRatingChanged += UserRatingChanged;
        }

        private void UserRatingChanged(object sender, UserRatingChangedEventArgs e)
        {
            ReviewSectionComponent.UserRatingChanged(e);
        }

        public void ScrollToTop()
        {
            this.MainScrollViewer?.ChangeView(0, 0, 1);
        }

        #region Background Blur Operations

        private async Task ApplyBlurEffect()
        {
            OnImageOpened(null, null);
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                BackgroundImageElement.ImageOpened -= OnImageOpened;
                BackgroundImageElement.ImageOpened += OnImageOpened;
            });
        }

        private void OnImageOpened(object sender, RoutedEventArgs e)
        {
            Compositor _compositor = ElementCompositionPreview.GetElementVisual(this).Compositor;

            var backgroundImageVisual = ElementCompositionPreview.GetElementVisual(BackgroundImageElement);

            var backdropBrush = _compositor.CreateBackdropBrush();

            // Create the blur effect
            var blurEffect = new GaussianBlurEffect
            {
                Name = "Blur",
                BlurAmount = 8.0f,
                BorderMode = EffectBorderMode.Soft,
                Optimization = EffectOptimization.Quality,
                Source = new CompositionEffectSourceParameter("source")
              
            };

            var effectFactory = _compositor.CreateEffectFactory(blurEffect);
            
            var effectBrush = effectFactory.CreateBrush();
            effectBrush.SetSourceParameter("source", backdropBrush);

            SpriteVisual _blurVisual = _compositor.CreateSpriteVisual();
            _blurVisual.Brush = effectBrush;
            _blurVisual.Size = new Vector2((float)BackgroundContainer.ActualWidth, (float)BackgroundContainer.ActualHeight);
            ElementCompositionPreview.SetElementChildVisual(BackgroundImageElement, _blurVisual);

            BackgroundContainer.SizeChanged += (s, args) =>
            {
                _blurVisual.Size = new Vector2((float)BackgroundContainer.ActualWidth, (float)BackgroundContainer.ActualHeight);
            };
        }

        #endregion Background Blur Operations

        public async Task UpdateMediaPage(MediaDetailBObj mediaDetailBObj)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                MediaDetailVObj mediaDetailVObj = new MediaDetailVObj(mediaDetailBObj);
                MainGrid.Visibility = Visibility.Visible;
                MediaDetail.UpdateFrom(mediaDetailBObj);
                MediaDetailControlComponent.MediaDetail = mediaDetailVObj;
            });
        }

        public void Init(long mediaId)
        {
            MediaDetail = new MediaPageVObj(mediaId);
            ReviewSectionComponent.MediaId = mediaId;
            ReloadPageContent();
        }

        public void ReloadPageContent()
        {
            if(MediaDetail != null && MediaDetail.MediaId != 0)
            {
                _viewModel.GetMediaDetail(MediaDetail.MediaId);
                ReviewSectionComponent.ReloadData();
                ApplyBlurEffect();
            }
        }

        private void BackgroundImageElement_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            if (sender is Image image)
            {
                image.Source = new BitmapImage(new Uri(ImageManager.GetDefaultPosterImagePath()));
            }
        }


    }
}