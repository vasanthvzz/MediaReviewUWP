using MediaReviewClassLibrary.Models;
using MediaReviewUWP.View.Contract;
using MediaReviewUWP.ViewModel;
using MediaReviewUWP.ViewModel.Contract;
using MediaReviewUWP.ViewObjects;
using Microsoft.Graphics.Canvas.Effects;
using System;
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
        private MediaDetailBObj _mediaDetail;
        private long _mediaId;

        public event EventHandler DataFetched;
        public event EventHandler MediaDetailComponentUpdated;

        public long MediaId
        {
            get => _mediaId;
            set
            {
                if (_mediaId != value)
                {
                    _mediaId = value;
                }
            }
        }

        public MediaPage()
        {
            _viewModel = new MediaPageViewModel(this);
            this.InitializeComponent();
            ApplyBlurEffect();
            Window.Current.SizeChanged += AdjustScrollViewer;
            DataFetched += MediaPage_DataFetched;
            this.SizeChanged += AdaptiveSize;
            this.Loaded += MediaPage_Loaded;
        }

        public void MediaPage_Loaded(object sender, RoutedEventArgs e) 
        {
            AdjustScrollViewer(null,null);
        }

        private void MediaPage_DataFetched(object sender, EventArgs e)
        {
            LoadImage();
        }

        private void LoadImage()
        {
            if (_mediaDetail?.Media?.PosterPath != null)
            {
                var bitmapImage = new BitmapImage(new Uri(_mediaDetail.Media.PosterPath, UriKind.Absolute));
                BackgroundImageElement.Source = bitmapImage;
            }
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

            // Apply the blur effect only to the background image (targeting the background image element)
            ElementCompositionPreview.SetElementChildVisual(BackgroundImageElement, _blurVisual);

            BackgroundContainer.SizeChanged += (s, e) =>
            {
                _blurVisual.Size = new Vector2((float)BackgroundContainer.ActualWidth, (float)BackgroundContainer.ActualHeight);
            };
        }

        #endregion

        public async void UpdateMediaPage(MediaDetailBObj mediaDetailBObj)
        {
            _mediaDetail = mediaDetailBObj;
            MediaDetailVObj mediaDetailVObj = new MediaDetailVObj(mediaDetailBObj?.Media, mediaDetailBObj?.UserPersonalMedia, mediaDetailBObj?.UserRating, mediaDetailBObj?.GenreList);
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.High, () =>
            {
                DataFetched?.Invoke(this, EventArgs.Empty);
                MediaDetailControlComponent.DataContext = mediaDetailVObj;
            });
        }

        public void Init(long mediaId)
        {
            MediaId = mediaId;
            ReviewSectionComponent.MediaId = MediaId;
            _viewModel.GetMediaDetail(MediaId);
        }

        private void BackgroundImageElement_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            Debug.WriteLine("image failed" + _mediaDetail?.Media?.Title);
        }
    }
}
