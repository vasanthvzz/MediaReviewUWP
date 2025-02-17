﻿using MediaReviewClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaReviewUWP.View.Contract
{
    public interface IFilteredMediaPage
    {
        void UpdateMediaList(List<MediaBObj> filteredMediaList);
    }
}
