using MediaReviewClassLibrary.Data;
using MediaReviewClassLibrary.Models;
using MediaReviewClassLibrary.Models.Constants;
using MediaReviewClassLibrary.Models.Enitites;
using MediaReviewUWP.Utility;
using MediaReviewUWP.View.AddMovieView;
using MediaReviewUWP.View.Contract;
using MediaReviewUWP.View.LandingPageView;
using MediaReviewUWP.View.MediaPageView;
using MediaReviewUWP.View.SettingsView;
using MediaReviewUWP.ViewModel;
using MediaReviewUWP.ViewModel.Contract;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.Resources;
using Windows.UI.WindowManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;
using static MediaReviewUWP.Components.UserProfilePicturePresenter;

namespace MediaReviewUWP.View.HomePageView
{
    public sealed partial class HomePage : Page, IHomePageView
    {
        private IHomePageViewModel _viewModel;
        private UserDetail _user;
        private ObservableCollection<Genre> _genreList;

        public UserDTBObj UserDt {  get; set; }

        public HomePage()
        {
            this.InitializeComponent();
            _user = SessionManager.User;
            UserDt = new UserDTBObj(_user);
            _genreList = new ObservableCollection<Genre>();
            _viewModel = new HomePageViewModel(this);
            GlobalEventManager.OnMediaAdded += GlobalEventManager_OnMediaAdded;
        }

        private void GlobalEventManager_OnMediaAdded(object sender, MediaAddedEventArgs e)
        {
            //Get media by id if the release date is greater than the current

        }


        #region Page Initilization Methods

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadContentPage();
        }

        private void LoadContentPage()
        {
            RetrieveMedia();
            RetrieveTags();
        }

        private void RetrieveTags()
        {
            _viewModel.GetAllGenre();
        }

        private void UpdateThemeButtonContent()
        {
            var currentTheme = ThemeManager.CurrentElementTheme;
            ThemeButton.Content = currentTheme == ElementTheme.Dark ? "☾" : "☼";
        }

        #endregion Page Initilization Methods

        #region Tab items operations

        private TabViewItem GetTabItem(string tag)
        {
            foreach (TabViewItem item in MainTabView.TabItems)
            {
                if (item.Tag != null && item.Tag.Equals(tag))
                {
                    return item;
                }
            }
            return null;
        }

        private TabViewItem CreateTabItem(string tag, string header, Symbol symbol)
        {
            TabViewItem newItem = new TabViewItem
            {
                Tag = tag,
                Header = header,
                IconSource = new Microsoft.UI.Xaml.Controls.SymbolIconSource() { Symbol = symbol }
            };
            MainTabView.TabItems.Add(newItem);
            return newItem;
        }

        private void MediaTileSelected(object sender, MediaTileEventArgs e)
        {
            var tabItem = GetTabItem(e.MediaId.ToString()) ?? CreateTabItem(e.MediaId.ToString(), e.Title, Symbol.Pictures);
            var page = new MediaPage();
            page.Init(e.MediaId);
            tabItem.Content = page;
            MainTabView.SelectedItem = tabItem;
        }

        private void FavoriteButton_Click(object sender, RoutedEventArgs e)
        {
            var loader = new ResourceLoader();
            var tabItem = GetTabItem("favourites") ?? CreateTabItem("favourites", loader.GetString("MyFavourites"), Symbol.Favorite);
            CreatePersonalisedMediaTab(PersonalMediaType.FAVOURITE, tabItem);
            MainTabView.SelectedItem = tabItem;
        }


        private void CreatePersonalisedMediaTab(PersonalMediaType type, TabViewItem tabItem)
        {
            var page = new PersonalisedMediaControl();
            page.Init(type);
            page.PersonalisedMediaTileClicked -= MediaTileSelected;
            page.PersonalisedMediaTileClicked += MediaTileSelected;
            tabItem.Content = page;
        }

        //private void CreateFolloweeTab(FollowType followType,TabViewItem tabItem)
        //{
        //    var page = new FollowListControl();
        //    page.Init(followType);
        //    tabItem.Content = page;
        //}

        //private void MyFolloweeButton_Click(object sender, RoutedEventArgs e)
        //{
        //    var loader = new ResourceLoader();
        //    var tabItem = GetTabItem("followees") ?? CreateTabItem("followees",loader.GetString("PeopleIFollow") , Symbol.Contact);
        //    CreateFolloweeTab(FollowType.FOLLOWEE, tabItem);
        //    MainTabView.SelectedItem = tabItem;
        //}

        //private void FollowersButton_Click(object sender, RoutedEventArgs e)
        //{
        //    var loader = new ResourceLoader();
        //    var tabItem = GetTabItem("followers") ?? CreateTabItem("followers",loader.GetString("MyFollowers"), Symbol.People);
        //    CreateFolloweeTab(FollowType.FOLLOWER, tabItem);
        //    MainTabView.SelectedItem = tabItem;
        //}

        private void HasWatchedButton_Click(object sender, RoutedEventArgs e)
        {
            var loader = new ResourceLoader();
            var tabItem = GetTabItem("hasWatched") ?? CreateTabItem("hasWatched", loader.GetString("MyWatchedlist"), Symbol.Bookmarks);
            CreatePersonalisedMediaTab(PersonalMediaType.HAS_WATCHED, tabItem);
            MainTabView.SelectedItem = tabItem;
        }

        private void WatchListButton_Click(object sender, RoutedEventArgs e)
        {
            var loader = new ResourceLoader();
            var tabItem = GetTabItem("watchlist") ?? CreateTabItem("watchlist", loader.GetString("MyWatchlist"), Symbol.Emoji);
            CreatePersonalisedMediaTab(PersonalMediaType.WATCHLIST, tabItem);
            MainTabView.SelectedItem = tabItem;
        }

        private void RatingsButton_Click(object sender, RoutedEventArgs e)
        {
            var loader = new ResourceLoader();
            var tabItem = GetTabItem("myRatings") ?? CreateTabItem("myRatings", loader.GetString("MyRatings"), Symbol.OutlineStar);
            var page = new UserRatedMediaPage();
            tabItem.Content = page;
            page.RatedMediaClick -= MediaTileSelected;
            page.RatedMediaClick += MediaTileSelected;
            MainTabView.SelectedItem = tabItem;
        }

        private void SearchMedia()
        {
            var searchText = UniversalSearchBox.Text;
            if (string.IsNullOrWhiteSpace(searchText)) { return; }
            var loader = new ResourceLoader();
            var tabItem = GetTabItem("search") ?? CreateTabItem("search", loader.GetString("Search"), Symbol.Find);

            if (tabItem.Content is SearchResultPage searchPage)
            {
                searchPage.Init(UniversalSearchBox.Text);
            }
            else
            {
                var page = new SearchResultPage();
                tabItem.Content = page;
                page.SearchResultClick -= Page_SearchResultClick;
                page.SearchResultClick += Page_SearchResultClick;
                page.Init(searchText);
            }
            MainTabView.SelectedItem = tabItem;
        }

        private void Page_SearchResultClick(object sender, MediaTileEventArgs e)
        {
            var tabItem = GetTabItem(e.MediaId.ToString()) ?? CreateTabItem(e.MediaId.ToString(), e.Title, Symbol.Pictures);
            var page = new MediaPage();
            tabItem.Content = page;
            page.Init(e.MediaId);
            MainTabView.SelectedItem = tabItem;
        }

        private void ApplyFilterBtn_Click(object sender, RoutedEventArgs e)
        {
            if (GenreListView.SelectedItems.Count != 0)
            {
                FilterBtn.Flyout.Hide();
                List<Genre> genreList = new List<Genre>();
                foreach (var genre in GenreListView.SelectedItems)
                {
                    genreList.Add(genre as Genre);
                };
                var loader = new ResourceLoader();
                var tabItem = GetTabItem("filteredMovies") ?? CreateTabItem("filteredMovies", loader.GetString("FilteredMovies"), Symbol.Pictures);
                FilteredMediaPage page = tabItem.Content as FilteredMediaPage;
                if (page == null)
                {
                    page = new FilteredMediaPage();
                    page.TileClicked -= MediaTileSelected;
                    page.TileClicked += MediaTileSelected;
                    tabItem.Content = page;
                }
                page.Init(genreList);
                MainTabView.SelectedItem = tabItem;
            }
        }

        private void ReviewsButton_Click(object sender, RoutedEventArgs e)
        {
            var loader = new ResourceLoader();
            var tabItem = GetTabItem("myReviews") ?? CreateTabItem("myReviews", loader.GetString("MyReviews"), Symbol.Edit);
            var page = new UserReviewPage();
            tabItem.Content = page;
            page.MediaReviewClicked -= MediaTileSelected;
            page.MediaReviewClicked += MediaTileSelected;
            MainTabView.SelectedItem = tabItem;
        }

        private void TabView_TabCloseRequested(TabView sender, TabViewTabCloseRequestedEventArgs args)
        {
            sender.TabItems.Remove(args.Tab);
        }

        private void MainTabView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems == null || e.AddedItems.Count == 0)
            {
                return;
            }

            var tabItem = e.AddedItems.FirstOrDefault();
            var tabViewItem = tabItem as TabViewItem;

            if (tabViewItem.Content is ShowMediaListControl control)
            {
                _viewModel.GetAllMedia(0, control.MediaList.Count);
                control.ReloadPageContent();
            }

            if (tabViewItem.Content is ITabItemContent MediaPage)
            {
                MediaPage.ReloadPageContent();
            }
        }

        #endregion Tab items operations

        #region Button Clicks

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            _ = LogoutSession();
        }

        private void ThemeButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ThemeManager.RequestThemeChange();
            UpdateThemeButtonContent();
            ProfileFlyout?.Hide();
        }

        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateThemeButtonContent();
            if (ProfileButton.Flyout is Flyout)
            {
                ProfileFlyout.ShowAt(ProfileButton);
            }
        }

        #endregion Button Clicks

        #region Navbar Actions

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            NavBar.IsPaneOpen = !NavBar.IsPaneOpen;
        }

        private void NavBar_PaneOpening(SplitView sender, object args)
        {
            MyMovieListTb.Visibility = Visibility.Visible;
            //SocialTb.Visibility = Visibility.Visible;
            MyActionTb.Visibility = Visibility.Visible;
        }

        private void NavBar_PaneClosing(SplitView sender, SplitViewPaneClosingEventArgs args)
        {
            MyMovieListTb.Visibility = Visibility.Collapsed;
            //SocialTb.Visibility = Visibility.Collapsed;
            MyActionTb.Visibility = Visibility.Collapsed;
        }

        #endregion Navbar Actions

        private async Task LogoutSession()
        {
            await Task.Run(async () =>
            {
                await WindowManager.CloseSettingsWindow(this.Dispatcher);
                await WindowManager.CloseCreateMovieWindow(this.Dispatcher);
            });

            //await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.High, async () =>
            //{
            //    Frame.Navigate(typeof(LandingPage));

            //    await Task.Delay(500);
            //    WindowManager.MainWindow.Activate();
            //});

            await Task.Run(() => SessionManager.RemoveUserFromStorage());

            _ = CoreApplication.RequestRestartAsync("");
        }

        public void RetrieveMedia()
        {
            _viewModel.GetAllMedia(0, 10);
        }

        public void UpdateMediaList(List<MediaBObj> MediaList)
        {
            HomeContent.UpdateMedia(MediaList);
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            var homeItem = MainTabView.TabItems
        .OfType<TabViewItem>()
        .FirstOrDefault(tab => tab.Header != null && tab.Tag.ToString() == "home");
            MainTabView.SelectedItem = homeItem;
        }

        private void SettingButton_Click(object sender, RoutedEventArgs e)
        {
            _ = OpenSettingsWindow();
        }

        private async Task OpenSettingsWindow()
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

                TitleBarManager.ChangeTitleBarTheme(titleBar);
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
            else
            {
                await WindowManager.SettingsWindow.TryShowAsync();
            }
        }

        private async Task OpenAddMovieWindow()
        {
            if (!WindowManager.CreateMovieWindowExist())
            {
                var movieWindow = await AppWindow.TryCreateAsync();
                Frame appWindowContentFrame = new Frame
                {
                    RequestedTheme = ThemeManager.CurrentElementTheme
                };
                WindowManager.CreateMovieWindow = movieWindow;
                appWindowContentFrame.Navigate(typeof(AddMoviePage));
                var titleBar = movieWindow.TitleBar;
                
                TitleBarManager.ChangeTitleBarTheme(titleBar);
                movieWindow.Title = "Add Movie";
                movieWindow.Closed += delegate
                {
                    WindowManager.CreateMovieWindow = null;
                    appWindowContentFrame.Content = null;
                    movieWindow = null;
                };
                ElementCompositionPreview.SetAppWindowContent(movieWindow, appWindowContentFrame);
                await movieWindow.TryShowAsync();
            }
            else
            {
                await WindowManager.CreateMovieWindow.TryShowAsync();
            }
        }

        private void AddMovieBtn_Click(object sender, RoutedEventArgs e)
        {
            _ = OpenAddMovieWindow();
        }

        private void HomeContent_ListReachedEnd(object sender, ListReachedEndArgs e)
        {
            var currentLength = e.ExistingItemCount;
            _viewModel.GetAllMedia(currentLength, 5);
        }

        private void SearchBox_SearchButtonClicked()
        {
            SearchMedia();
        }

        private void SearchBox_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                SearchMedia();
            }
        }

        public void UpdateGenreList(List<Genre> genreList)
        {
            _ = Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                foreach (var genre in genreList) { _genreList.Add(genre); }
            });
        }
    }
}