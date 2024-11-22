using MediaReviewClassLibrary.Models.Enitites;
using MediaReviewUWP.View.Contract;
using MediaReviewUWP.View.HomePageView;
using MediaReviewUWP.ViewModel;
using MediaReviewUWP.ViewModel.Contract;

using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;


// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace MediaReviewUWP.View.WelcomePageView
{
    public sealed partial class LoginUserControl : UserControl, ILoginUserView
    {
        private ILoginUserViewModel _viewModel;
        public LoginUserControl()
        {
            this.InitializeComponent();
            _viewModel = new LoginUserViewModel(this);
            _viewModel.Dispatcher = Dispatcher;
        }

        public async void PasswordMissmatch()
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                ErrorText.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
                ErrorText.Text = "Password Missmatch";
            });
        }

        public async void UsernameNotFound()
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                ErrorText.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
                ErrorText.Text = "User not Found";
            });

        }

        public async void ValidationSuccess(UserDetail user)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.High, () =>
            {
                NavigateToHomePage(user);
            });
        }

        private void NavigateToHomePage(UserDetail user)
        {
            var parentFrame = Window.Current.Content as Frame;
            if (parentFrame != null)
            {
                parentFrame.Navigate(typeof(HomePage));
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTB.Text;
            string password = PasswordPB.Password;

            if (username != null && password != null)
            {
                _viewModel.LoginUser(username, password);
            }
        }
    }
}
