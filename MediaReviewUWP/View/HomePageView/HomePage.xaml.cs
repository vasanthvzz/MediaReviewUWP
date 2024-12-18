using MediaReviewClassLibrary;
using MediaReviewClassLibrary.Models;
using MediaReviewClassLibrary.Models.Constants;
using MediaReviewClassLibrary.Models.Enitites;
using MediaReviewClassLibrary.Utlis;
using MediaReviewUWP.Utils;
using MediaReviewUWP.View.Contract;
using MediaReviewUWP.View.LandingPageView;
using MediaReviewUWP.View.MediaPageView;
using MediaReviewUWP.View.SettingsView;
using MediaReviewUWP.ViewModel;
using MediaReviewUWP.ViewModel.Contract;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.WindowManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Hosting;


namespace MediaReviewUWP.View.HomePageView
{
    public sealed partial class HomePage : Page, IHomePageView
    {
        private ISessionManager _sessionManager = MediaReviewDIServiceProvider.GetServiceProvider().GetRequiredService<ISessionManager>();
        private IHomePageViewModel _viewModel;
        private UserDetail _user;

        public HomePage()
        {
            this.InitializeComponent();
            _viewModel = new HomePageViewModel(this);
            _user = _sessionManager.RetriveUserFromStorage();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadContentPage();
        }

        private void LoadContentPage()
        {
            RetrieveMedia();
        }    

        private void MediaTileSelected(object sender, MediaTileEventArgs e)
        {
            foreach (TabViewItem item in MainTabView.TabItems)
            {
                if (item.Tag != null && item.Tag.Equals(e.MediaId))
                {
                    MainTabView.SelectedItem = item;
                    return;
                }
            }

            TabViewItem newItem = new TabViewItem
            {
                Tag = e.MediaId,
                Header = $"{e.Title}",
                IconSource = new Microsoft.UI.Xaml.Controls.SymbolIconSource() { Symbol = Symbol.Pictures }
            };
           
            var page = new MediaPage();
            page.Init(e.MediaId);
            newItem.Content = page;
            MainTabView.TabItems.Add(newItem);
            MainTabView.SelectedItem = newItem;
        }

        #region Page Initilization Methods

        private void UpdateThemeButtonContent()
        {
            var currentTheme = ThemeManager.CurrentElementTheme;
            ThemeButton.Content = currentTheme == ElementTheme.Dark ? "☾" : "☼";
        }

        #endregion

        #region Button Clicks

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            LogoutSession();
        }

        private void ThemeButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ThemeManager.RequestThemeChange();
            UpdateThemeButtonContent();
            if (ProfileFlyout != null)
            {
                ProfileFlyout.Hide();
            }
        }

        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateThemeButtonContent();
            if (ProfileButton.Flyout is Flyout)
            {
                ProfileFlyout.ShowAt(ProfileButton);
            }
        }

        #endregion

        #region Navbar Actions

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            NavBar.IsPaneOpen = !NavBar.IsPaneOpen;
        }

        private void NavBar_PaneOpening(SplitView sender, object args)
        {
            MyMovieListTb.Visibility = Visibility.Visible;
            SocialTb.Visibility = Visibility.Visible;
            MyActionTb.Visibility = Visibility.Visible;
        }

        private void NavBar_PaneClosing(SplitView sender, SplitViewPaneClosingEventArgs args)
        {
            MyMovieListTb.Visibility = Visibility.Collapsed;
            SocialTb.Visibility = Visibility.Collapsed;
            MyActionTb.Visibility = Visibility.Collapsed;
        }

        #endregion

        private async void LogoutSession()
        {
            _sessionManager.RemoveUserFromStorage();
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.High,async () =>
            {
                await WindowManager.CloseSettingsWindow();
                Frame.Navigate(typeof(LandingPage));
            });
        }

        public void RetrieveMedia()
        {
            _viewModel.GetAllMedia(0,10);
        }


        public void UpdateMediaList(List<MediaBObj> MediaList)
        {
            HomeContent.UpdateMedia(MediaList);
        }

        

        private void TabView_TabCloseRequested(TabView sender, TabViewTabCloseRequestedEventArgs args)
        {
            sender.TabItems.Remove(args.Tab);
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            var homeItem = MainTabView.TabItems
        .OfType<TabViewItem>()
        .FirstOrDefault(tab => tab.Header != null && tab.Tag.ToString() == "home");
            MainTabView.SelectedItem = homeItem;
        }

        private void MainTabView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.AddedItems == null || e.AddedItems.Count == 0)
            {
                return;
            }

            var tabItem = e.AddedItems.FirstOrDefault();
            var tabViewItem = tabItem as TabViewItem;

            if(tabViewItem.Content is ShowMediaListControl control)
            {
                _viewModel.GetAllMedia(0,control.MediaList.Count);
            }

            if(tabViewItem.Content is ITabItemContent personalizedMediaPage)
            {
                personalizedMediaPage.ReloadData();
            }          
        }

        private void FavoriteButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (TabViewItem item in MainTabView.TabItems)
            {
                if (item.Tag != null && item.Tag.Equals("favorites"))
                {
                    MainTabView.SelectedItem = item;
                    return;
                }
            }

            TabViewItem newItem = new TabViewItem
            {
                Tag = "favorites",
                Header = "My Favorites",
                IconSource = new Microsoft.UI.Xaml.Controls.SymbolIconSource() { Symbol = Symbol.Favorite }
            };

            var page = new PersonalisedMediaControl();
            page.Init(PersonalMediaType.FAVOURITE);
            page.PersonalisedMediaTileClicked -= MediaTileSelected;
            page.PersonalisedMediaTileClicked += MediaTileSelected;
            newItem.Content = page;
            MainTabView.TabItems.Add(newItem);
            MainTabView.SelectedItem = newItem;
        }

        private void HasWatchedButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (TabViewItem item in MainTabView.TabItems)
            {
                if (item.Tag != null && item.Tag.Equals("hasWatched"))
                {
                    MainTabView.SelectedItem = item;
                    return;
                }
            }

            // Create a new TabViewItem
            TabViewItem newItem = new TabViewItem
            {
                Tag = "hasWatched",
                Header = "Watched Movies",
                IconSource = new Microsoft.UI.Xaml.Controls.SymbolIconSource() { Symbol = Symbol.Bookmarks }
            };

            var page = new PersonalisedMediaControl();
            page.Init(PersonalMediaType.HAS_WATCHED);
            page.PersonalisedMediaTileClicked -= MediaTileSelected;
            page.PersonalisedMediaTileClicked += MediaTileSelected;
            newItem.Content = page;
            MainTabView.TabItems.Add(newItem);
            MainTabView.SelectedItem = newItem;
        }

        private void WatchListButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (TabViewItem item in MainTabView.TabItems)
            {
                if (item.Tag != null && item.Tag.Equals("watchlist"))
                {
                    MainTabView.SelectedItem = item;
                    return;
                }
            }

            TabViewItem newItem = new TabViewItem
            {
                Tag = "watchlist",
                Header = "Watch list",
                IconSource = new Microsoft.UI.Xaml.Controls.SymbolIconSource() { Symbol = Symbol.Emoji }
            };

            var page = new PersonalisedMediaControl();
            page.Init(PersonalMediaType.WATCHLIST);
            page.PersonalisedMediaTileClicked -= MediaTileSelected;
            page.PersonalisedMediaTileClicked += MediaTileSelected;
            newItem.Content = page;
            MainTabView.TabItems.Add(newItem);
            MainTabView.SelectedItem = newItem;
        }

        private async void SettingButton_Click(object sender, RoutedEventArgs e)
        {
            if (!WindowManager.SettingsWindowExist())
            {
                var settingsWindow = await AppWindow.TryCreateAsync();
                Frame appWindowContentFrame = new Frame
                {
                    RequestedTheme = ThemeManager.CurrentElementTheme
                };
                WindowManager.SettingsWindow = settingsWindow;
                appWindowContentFrame.Navigate(typeof(SettingsPage));
                var titleBar = settingsWindow.TitleBar;
                TitlebarManager.ChangeTitlebarTheme(titleBar);
                settingsWindow.Title = "Settings";
                settingsWindow.Closed += delegate
                {
                    WindowManager.SettingsWindow = null;
                    appWindowContentFrame.Content = null;
                    settingsWindow = null;
                };
                ElementCompositionPreview.SetAppWindowContent(settingsWindow, appWindowContentFrame);
                await settingsWindow.TryShowAsync();
            }
        }


        private void RatingsButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (TabViewItem item in MainTabView.TabItems)
            {
                if (item.Tag != null && item.Tag.Equals("myRatings"))
                {
                    MainTabView.SelectedItem = item;
                    return;
                }
            }

            TabViewItem newItem = new TabViewItem
            {
                Tag = "myRatings",
                Header = "My Ratings",
                IconSource = new Microsoft.UI.Xaml.Controls.SymbolIconSource() { Symbol = Symbol.OutlineStar }
            };

            var page = new UserRatedMediaPage();
            newItem.Content = page;
            page.RatedMediaClick -= MediaTileSelected;
            page.RatedMediaClick += MediaTileSelected;
            MainTabView.TabItems.Add(newItem);
            MainTabView.SelectedItem = newItem;
        }

        private void ReviewsButton_Click(object sender, RoutedEventArgs e)
        {

            foreach (TabViewItem item in MainTabView.TabItems)
            {
                if (item.Tag != null && item.Tag.Equals("myReviews"))
                {
                    MainTabView.SelectedItem = item;
                    return;
                }
            }

            TabViewItem newItem = new TabViewItem
            {
                Tag = "myReviews",
                Header = "My Reviews",
                IconSource = new Microsoft.UI.Xaml.Controls.SymbolIconSource() { Symbol = Symbol.Edit }
            };

            var page = new UserReviewPage();
            newItem.Content = page;
            MainTabView.TabItems.Add(newItem);
            MainTabView.SelectedItem = newItem;
        }

        private void HomeContent_ListReachedEnd(object sender, ListReachedEndArgs e)
        {
            var currentLength = e.ExistingItemCount;
            _viewModel.GetAllMedia(currentLength,5);
        }
    }
}



