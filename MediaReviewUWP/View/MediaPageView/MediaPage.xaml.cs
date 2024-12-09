using MediaReviewClassLibrary.Models;
using MediaReviewUWP.View.Contract;
using MediaReviewUWP.ViewModel;
using MediaReviewUWP.ViewModel.Contract;
using MediaReviewUWP.ViewObject;
using Microsoft.Graphics.Canvas.Effects;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Numerics;
using Windows.UI.Composition;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Media.Imaging;

namespace MediaReviewUWP.View.MediaPageView
{
    public sealed partial class MediaPage : Page, IMediaPage
    {
        private IMediaPageViewModel _viewModel;
        private Compositor _compositor;
        private SpriteVisual _blurVisual;
        public MediaPageVObj MediaDetail { get; set; }

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

        private void UserRatingChanged(object sender , UserRatingChangedEventArgs e)
        {
            ReviewSectionComponent.UserRatingChanged(e);
        }


        private void AdjustScrollViewer(object sender, WindowSizeChangedEventArgs e)
        {
            double windowHeight = Window.Current.Bounds.Height;
            BackgroundImageElement.Height = windowHeight;
            MainScrollViewer.Height = windowHeight;
        }

        private void AdaptiveSize(object sender, RoutedEventArgs e)
        {
            MainScrollViewer.Height = BackgroundContainer.ActualHeight;
        }

        #region Background Blur Operations

        private void ApplyBlurEffect()
        {
            _compositor = ElementCompositionPreview.GetElementVisual(this).Compositor;

            var backgroundImageVisual = ElementCompositionPreview.GetElementVisual(BackgroundImageElement);

            var backdropBrush = _compositor.CreateBackdropBrush();

            // Create the blur effect
            var blurEffect = new GaussianBlurEffect
            {
                Name = "Blur",
                BlurAmount = 4.0f,
                BorderMode = EffectBorderMode.Soft,
                Optimization = EffectOptimization.Speed,
                Source = new CompositionEffectSourceParameter("source")
            };

            // Create an effect factory
            var effectFactory = _compositor.CreateEffectFactory(blurEffect);
            var effectBrush = effectFactory.CreateBrush();
            effectBrush.SetSourceParameter("source", backdropBrush);

            // Create a visual for the background image and apply the blur effect to it
            _blurVisual = _compositor.CreateSpriteVisual();
            _blurVisual.Brush = effectBrush;
            _blurVisual.Size = new Vector2((float)BackgroundContainer.ActualWidth, (float)BackgroundContainer.ActualHeight);

            // Apply the blur effect only to the background image (targeting the background image element)var
            Debug.Write(MediaDetail?.PosterPath);
            ElementCompositionPreview.SetElementChildVisual(BackgroundImageElement, _blurVisual);

            BackgroundContainer.SizeChanged += (s, e) =>
            {
                _blurVisual.Size = new Vector2((float)BackgroundContainer.ActualWidth, (float)BackgroundContainer.ActualHeight);
            };
        }

        #endregion

        public async void UpdateMediaPage(MediaDetailBObj mediaDetailBObj)
        {
            MediaDetailVObj mediaDetailVObj = new MediaDetailVObj(mediaDetailBObj) ;
            await Dispatcher.TryRunAsync(CoreDispatcherPriority.Normal, () =>
            {
                MediaDetail.UpdateFrom(mediaDetailBObj);
                MediaDetailControlComponent.MediaDetail = mediaDetailVObj;
            });
        }

        public void Init(long mediaId)
        {
            MediaDetail = new MediaPageVObj(mediaId);
            ReviewSectionComponent.MediaId = MediaDetail.MediaId;
            ReloadData();
        }

        public void ReloadData()
        {
            _viewModel.GetMediaDetail(MediaDetail.MediaId);
            ReviewSectionComponent.ReloadData();
            ApplyBlurEffect();
        }

        private void BackgroundImageElement_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }
    }
}
