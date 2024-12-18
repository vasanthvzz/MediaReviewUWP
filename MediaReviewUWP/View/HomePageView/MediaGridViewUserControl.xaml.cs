using MediaReviewUWP.ViewObject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

namespace MediaReviewUWP.View.HomePageView
{
    public sealed partial class MediaGridViewUserControl : UserControl
    {
        public event EventHandler<MediaTileEventArgs> TileClicked;
        public event Action ScrollViewerEnd;

        public ObservableCollection<MediaTileVObj> MediaList
        {
            get { return (ObservableCollection<MediaTileVObj>)GetValue(MediaListProperty); }
            set
            {
                SetValue(MediaListProperty, value);
            }
        }

        public static readonly DependencyProperty MediaListProperty =
      DependencyProperty.Register("MediaList", typeof(ObservableCollection<MediaTileVObj>), typeof(MediaGridViewUserControl), new PropertyMetadata(null));



        public MediaGridViewUserControl()
        {
            this.InitializeComponent();
            MediaList = new ObservableCollection<MediaTileVObj>();
            
        }

        private void Grid_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            if (sender is Grid grid)
            {
                var overlayGrid = grid.FindName("OverlayGrid") as Grid;
                var overlayTransform = overlayGrid?.RenderTransform as TranslateTransform;

                if (overlayTransform != null)
                {
                    var animation = new DoubleAnimation
                    {
                        To = 0,
                        Duration = TimeSpan.FromMilliseconds(200)
                    };

                    var storyboard = new Storyboard();
                    Storyboard.SetTarget(animation, overlayTransform);
                    Storyboard.SetTargetProperty(animation, "Y");
                    storyboard.Children.Add(animation);
                    storyboard.Begin();
                }
            }
        }

        private void Grid_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            if (sender is Grid grid)
            {
                var overlayGrid = grid.FindName("OverlayGrid") as Grid;
                var overlayTransform = overlayGrid?.RenderTransform as TranslateTransform;

                if (overlayTransform != null)
                {
                    var animation = new DoubleAnimation
                    {
                        To = 200,
                        Duration = TimeSpan.FromMilliseconds(200)
                    };

                    var storyboard = new Storyboard();
                    Storyboard.SetTarget(animation, overlayTransform);
                    Storyboard.SetTargetProperty(animation, "Y");
                    storyboard.Children.Add(animation);
                    storyboard.Begin();
                }
            }
        }


        private void Image_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }

        private void MediaTileClick(object sender, ItemClickEventArgs e)
        {
            MediaTileEventArgs args = new MediaTileEventArgs(e.ClickedItem as MediaTileVObj);
            TileClicked?.Invoke(null, args);
        }

        private void MediaGridView_Loaded(object sender, RoutedEventArgs e)
        {
            var scrollViewer = FindScrollViewer(MediaGridView);
            if (scrollViewer != null)
            {
                scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
                scrollViewer.ViewChanged -= ScrollViewer_ViewChanged;
                scrollViewer.ViewChanged += ScrollViewer_ViewChanged;
            }
        }

        private void ScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            var scrollViewer = sender as ScrollViewer;

            if (scrollViewer != null)
            {
                if (scrollViewer.VerticalOffset >= scrollViewer.ScrollableHeight)
                {
                    ScrollViewerEnd?.Invoke();
                }
            }
        }

        private ScrollViewer FindScrollViewer(DependencyObject parent)
        {
            if (parent == null) return null;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is ScrollViewer scrollViewer)
                {
                    return scrollViewer;
                }
                else
                {
                    var result = FindScrollViewer(child);
                    if (result != null) return result;
                }
            }

            return null;
        }
    }
}
