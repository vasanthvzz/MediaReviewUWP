using MediaReviewClassLibrary.Models.Enitites;
using MediaReviewUWP.View.Contract;
using MediaReviewUWP.View.HomePageView;
using MediaReviewUWP.ViewModel;
using MediaReviewUWP.ViewModel.Contract;
using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace MediaReviewUWP.View.WelcomePageView
{
    public sealed partial class SignupUserControl : UserControl, ISignupUserView
    {
        ISignupUserViewModel _viewModel;
        public SignupUserControl()
        {
            this.InitializeComponent();
            _viewModel = new SignupUserViewModel(this);
        }

        private void SignupButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTB.Text;
            string password = PasswordPB.Password;
            if (ValidCredential(username) && ValidCredential(password))
            {
                if (sender is Button button)
                {
                    button.IsEnabled = false;
                }
                _viewModel.CreateUser(username, password);
            }
        }

        private bool ValidCredential(string credential)
        {
            return credential != null && credential.Length >= 4 && credential.Length <= 16 && credential.Trim() == credential;
        }



        private void UsernameTB_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox box)
            {
                var text = box.Text;
                if (text.Length < 4 || text.Length > 16 || text.Trim() != text)
                {
                    UsernameErrorText.Text = "!";
                    if (text.Length < 4)
                    {
                        UsernameTT.Content = "Username should have atleast 4 characters";
                    }
                    else if (text.Length > 16)
                    {
                        UsernameTT.Content = "Username should not exceed 16 characters";
                    }
                    else
                    {
                        UsernameTT.Content = "Username should not contain spaces at start and end";
                    }
                }
                else
                {
                    UsernameErrorText.Text = "";
                }
            }
        }


        private void PasswordPB_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox box)
            {
                var text = box.Password;
                if (text.Length < 4 || text.Length > 16 || text.Trim() != text)
                {
                    PasswordErrorText.Text = "!";
                    if (text.Length < 4)
                    {
                        PasswordTT.Content = "Password should have atleast 4 characters";
                    }
                    else if (text.Length > 16)
                    {
                        PasswordTT.Content = "Password should not exceed 16 characters";
                    }
                    else
                    {
                        PasswordTT.Content = "Password should not contain spaces at start and end";
                    }
                }
                else
                {
                    PasswordErrorText.Text = "";
                }
            }
        }

        public async void AccountCreatedSuccess(UserDetail user)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.High, () =>
            {
                NavigateToHomePage();
            });
        }

        private void NavigateToHomePage()
        {
            var parentFrame = Window.Current.Content as Frame;
            if (parentFrame != null)
            {
                parentFrame.Navigate(typeof(HomePage));
            }
        }

        public async void AccountCreationFailed()
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {

                UsernameExistText.Text = "Username already exists! ☹️";
                UsernameExistText.Visibility = Visibility.Visible;
                UsernameExistText.Foreground = new SolidColorBrush(Colors.Red);
                SignupButton.IsEnabled = true;
            });
        }
    }
}
