using MediaReviewClassLibrary;
using MediaReviewClassLibrary.Data.DataHandler;
using MediaReviewClassLibrary.Models.Enitites;
using MediaReviewClassLibrary.Utlis;
using MediaReviewUWP.Utils;
using MediaReviewUWP.View.Contract;
using MediaReviewUWP.View.MediaPageView;
using MediaReviewUWP.View.WelcomePageView;
using MediaReviewUWP.ViewModel;
using MediaReviewUWP.ViewModel.Contract;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


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
                if (item.Tag != null && item.Tag.Equals(e.Media.Id))
                {
                    MainTabView.SelectedItem = item; // Navigate to the already added tab
                    return;
                }
            }

            // Create a new TabViewItem
            TabViewItem newItem = new TabViewItem
            {
                Tag = e.Media.Id,
                Header = $"{e.Media.Title}",
                IconSource = new Microsoft.UI.Xaml.Controls.SymbolIconSource() { Symbol = Symbol.Pictures }
            };
           
            var page = new MediaPage();
            page.Init(e.Media.Id);
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

        private void DbButton_Click(object sender, RoutedEventArgs e)
        {
            new MediaDataHandler();
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


        public void UpdateMediaList(List<Media> MediaList)
        {
            //var contentPage = new HomeContentPage();
            HomeContent.UpdateMedia(MediaList);
           // TabViewItem newItem = new TabViewItem
           // {
           //     Header = "Home",
           //     IconSource = new Microsoft.UI.Xaml.Controls.SymbolIconSource() { Symbol = Symbol.Home },
           // };
           // newItem.Content = contentPage;
           // newItem.Tag = "home";
           // MainTabView.TabItems.Add(newItem);
           //// MainTabView.SelectedItem = newItem;
           // newItem.IsClosable = false;

            HomeContent.MediaTileGridComponent.TileClicked -= MediaTileSelected;
            HomeContent.MediaTileGridComponent.TileClicked += MediaTileSelected;
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
        }
    }
}



