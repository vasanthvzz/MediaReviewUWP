﻿using Windows.UI.Core;

namespace MediaReviewUWP.ViewModel.Contract
{
    public interface ILoginUserViewModel
    {
        void LoginUser(string username, string password);
    }
}
