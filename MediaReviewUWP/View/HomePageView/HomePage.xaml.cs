using MediaReviewClassLibrary;
using MediaReviewClassLibrary.Data.DataHandler;
using MediaReviewClassLibrary.Models;
using MediaReviewClassLibrary.Models.Constants;
using MediaReviewClassLibrary.Models.Enitites;
using MediaReviewClassLibrary.Utlis;
using MediaReviewUWP.Utils;
using MediaReviewUWP.View.Contract;
using MediaReviewUWP.View.MediaPageView;
using MediaReviewUWP.View.SettingsView;
using MediaReviewUWP.View.WelcomePageView;
using MediaReviewUWP.ViewModel;
using MediaReviewUWP.ViewModel.Contract;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using static MediaReviewUWP.View.HomePageView.ShowMediaListControl;


namespace MediaReviewUWP.View.HomePageView
{
    public sealed partial class HomePage : Page, IHomePageView
    {
        private ISessionManager _sessionManager;
        private IHomePageViewModel _viewModel;
        private UserDetail _user;

        public HomePage()
        {
            this.InitializeComponent();
            _viewModel = new HomePageViewModel(this);
            _sessionManager = MediaReviewDIServiceProvider.GetServiceProvider().GetRequiredService<ISessionManager>();
            _user = _sessionManager.RetriveUserFromStorage();
           
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateThemeContent();
            LoadContentPage();
        }

        private void LoadContentPage()
        {
            RetrieveMedia();
        }    

        private void MediaTileSelected(object sender, MediaTileEventArgs e)
        {
            // Check if the media ID is already present in any existing tab
            foreach (TabViewItem item in MainTabView.TabItems)
            {
                if (item.Tag != null && item.Tag.Equals(e.MediaId))
                {
                    MainTabView.SelectedItem = item; // Navigate to the already added tab
                    return;
                }
            }

            // Create a new TabViewItem
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

        private void UpdateThemeContent()
        {
            var currentTheme = ((FrameworkElement)Window.Current.Content).RequestedTheme;
            string theme = "";
            if (currentTheme == ElementTheme.Dark)
            {
                ThemeButton.Content = "☾";
                theme = "dark";
            }
            else
            {
                ThemeButton.Content = "☼";
                theme = "light";
            }
            _sessionManager.SetApplicationTheme(theme);
        }

        #endregion

        #region Button Clicks

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            LogoutSession();
        }

        private void ThemeButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var button = sender as Button;
            button.IsEnabled = false;
            var currentTheme = ((FrameworkElement)Window.Current.Content).RequestedTheme;
            if (currentTheme == ElementTheme.Dark)
            {
                ((FrameworkElement)Window.Current.Content).RequestedTheme = ElementTheme.Light;
            }
            else
            {
                ((FrameworkElement)Window.Current.Content).RequestedTheme = ElementTheme.Dark;
            }

            //SolidColorBrush mildBackgroundBrush = (SolidColorBrush)Application.Current.Resources["MildBackground"];
            //Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().TitleBar.BackgroundColor = mildBackgroundBrush.Color;
            ThemeManager.InvokeThemeChange();
            UpdateThemeContent();
            button.IsEnabled = true;
        }

        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProfileButton.Flyout is Flyout)
            {
                ProfileFlyout.ShowAt(ProfileButton);
            }
        }

        private void AccentButton_Click(object sender, RoutedEventArgs e)
        {
            ThemeManager.ToggleTheme();
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
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.High, () =>
            {

                Frame.Navigate(typeof(WelcomePage));
            });
            _sessionManager.RemoveUserFromStorage();
        }

        public void RetrieveMedia()
        {
            _viewModel.GetAllMedia();
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
            //sender
            if(e.AddedItems == null || e.AddedItems.Count == 0)
            {
                return;
            }
            var tabItem = e.AddedItems.FirstOrDefault();
            var tabViewItem = tabItem as TabViewItem;
            if(tabViewItem.Content is PersonalisedMediaControl personalisedMediaPage)
            {
                personalisedMediaPage.ReloadData();
            }          
            if(tabViewItem.Content is ShowMediaListControl showMediaList)
            {
                showMediaList.ReloadData();
            }
            if(tabViewItem.Content is ShowMediaListControl mediaListControl)
            {
                _viewModel.GetAllMedia();
            }
            if (tabViewItem.Content is UserRatedMediaPage userRatedMediaPage)
            {
                userRatedMediaPage.ReloadData();   
            }
            if(tabViewItem.Content is MediaPage mediaPage)
            {
                mediaPage.ReloadData();
            }
            if (tabViewItem.Content is UserReviewPage userReviewPage)
            {
                userReviewPage.ReloadData();
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

            // Create a new TabViewItem
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
            var view = CoreApplication.CreateNewView();
            
            int id = 0;

            await view.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                Window.Current.Content = new SettingsPage();
                Window.Current.Activate();
                id = ApplicationView.GetForCurrentView().Id;
            });
            Debug.WriteLine(id);
            if (!WindowManager.CanCreateView(ViewType.SETTINGS, id))
            {
                id = WindowManager.GetSettingsId();
            }
            else
            {
                WindowManager.AddSettingId(id);
            }
            await ApplicationViewSwitcher.TryShowAsStandaloneAsync(id);
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
    }
}



