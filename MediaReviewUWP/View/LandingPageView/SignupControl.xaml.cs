using MediaReviewClassLibrary.Models.Enitites;
using MediaReviewUWP.View.Contract;
using MediaReviewUWP.ViewModel;
using MediaReviewUWP.ViewModel.Contract;
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MediaReviewUWP.View.LandingPageView
{
    public sealed partial class SignupControl : UserControl, ISignupUserView
    {
        public event Action SignupCompleted;

        public event Action LoginRequested;

        private ISignupUserViewModel _vm;

        public SignupControl()
        {
            this.InitializeComponent();
            _vm = new SignupUserViewModel(this);
        }

        private void LoginAccountButton_Click(object sender, RoutedEventArgs e)
        {
            LoginRequested?.Invoke();
        }

        private void RevealModeCheckbox_Changed(object sender, RoutedEventArgs e)
        {
            if (revealModeCheckBox.IsChecked == true)
            {
                PasswordBox.PasswordRevealMode = PasswordRevealMode.Visible;
            }
            else
            {
                PasswordBox.PasswordRevealMode = PasswordRevealMode.Hidden;
            }
        }

        private void CreateAccountBtn_Click(object sender, RoutedEventArgs e)
        {
            bool falseUsername = false;
            bool falsePassword = false;

            string username = UsernameBox.Text;
            string password = PasswordBox.Password;

            var loader = new ResourceLoader();
            if (username.Length < 4 || username.Length > 16)
            {
                falseUsername = true;
                InvalidUserNameTT.Subtitle = loader.GetString("UsernameError1");
            }
            if (username.Trim() != username)
            {
                falseUsername = true;
                InvalidUserNameTT.Subtitle = loader.GetString("UsernameError2");
            }
            if (password.Length < 4 || password.Length > 16)
            {
                InvalidPasswordTT.Subtitle = loader.GetString("PasswordError1");
                falsePassword = true;
            }

            InvalidUserNameTT.IsOpen = falseUsername;
            InvalidPasswordTT.IsOpen = falsePassword;
            PasswordBox.Password = "";

            if (!(falsePassword || falseUsername))
            {
                _vm.CreateUser(username, password);
            }
        }

        public void AccountCreatedSuccess(UserDetail user)
        {
            SignupCompleted?.Invoke();
        }

        public async Task AccountCreationFailed()
        {
            var loader = new ResourceLoader();
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                InvalidUserNameTT.Subtitle = loader.GetString("UsernameError3");
                InvalidUserNameTT.IsOpen = true;
            });
        }

        private void UserEntering(PasswordBox sender, PasswordBoxPasswordChangingEventArgs args)
        {
            if (PasswordBox.Password != "")
            {
                InvalidPasswordTT.IsOpen = false;
            }
        }

        private void UserEntering(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            InvalidUserNameTT.IsOpen = false;
        }
    }
}