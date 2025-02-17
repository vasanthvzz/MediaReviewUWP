using MediaReviewUWP.Components;
using MediaReviewUWP.Utility;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using static MediaReviewUWP.Components.UserProfilePicturePresenter;

namespace MediaReviewUWP.DataTemplateSelectors
{
    public class ProfileTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ProfilePictureTemplate { get; set; }
        public DataTemplate InitialsTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (item is UserDTBObj user)
            {
                if (!string.IsNullOrEmpty(user.ProfilePicture))
                {
                    var task = FileManager.FileExistsAsync(user.ProfilePicture);

                    if (!task.IsFaulted && task.Result)
                    {
                        return ProfilePictureTemplate;
                    }
                }
            }
            return InitialsTemplate;
        }

        protected override DataTemplate SelectTemplateCore(object item)
        {
            if (item is UserDTBObj user)
            {
                if (!string.IsNullOrEmpty(user.ProfilePicture))
                {
                    var task = FileManager.FileExistsAsync(user.ProfilePicture);

                    if (!task.IsFaulted && task.Result)
                    {
                        return ProfilePictureTemplate;
                    }
                }
            }
            return InitialsTemplate;
        }
    }
}