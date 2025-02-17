using MediaReviewClassLibrary.Models.Enitites;
using MediaReviewUWP.ViewObject;
using System.ComponentModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MediaReviewUWP.Components
{
    public sealed partial class UserProfilePicturePresenter : UserControl
    {
        public UserDTBObj UserDt
        {
            get { return (UserDTBObj)GetValue(UserDtProperty); }
            set { SetValue(UserDtProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UserDt.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UserDtProperty =
            DependencyProperty.Register("UserDt", typeof(UserDTBObj), typeof(UserProfilePicturePresenter), new PropertyMetadata(null));



        public UserProfilePicturePresenter()
        {
            this.InitializeComponent();
            this.DataContextChanged += UserProfilePicturePresenter_DataContextChanged;
        }

        private void UserProfilePicturePresenter_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            Bindings.Update();
        }

        private void UserProfilePicturePresenter_Loaded(object sender, RoutedEventArgs e)
        {
            ProfileTemplateCP.ContentTemplate = ProfileTemplateCP.ContentTemplateSelector.SelectTemplate(UserDt);
        }

        public class UserDTBObj : INotifyPropertyChanged
        {
            private string _userName;
            public string UserName
            {
                get => _userName;
                set
                {
                    if (_userName != value)
                    {
                        _userName = value;
                        OnPropertyChanged(nameof(UserName));
                    };
                }
            }

            private string _profilePicture;
            public string ProfilePicture
            {
                get => _profilePicture;
                set
                {
                    if (_profilePicture != value)
                    {
                        _profilePicture = value;
                        OnPropertyChanged(nameof(ProfilePicture));
                    }
                }
            }

            public UserDTBObj(string userName, string profilePic)
            {
                UserName = userName;
                ProfilePicture = profilePic;
            }

            public UserDTBObj(UserDetail user)
            {
                UserName = user.UserName;
                ProfilePicture = user.ProfilePicture;
            }

            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}