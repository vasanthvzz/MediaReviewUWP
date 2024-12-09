using System;
using System.Collections.Generic;
using System.Linq;

namespace MediaReviewUWP.Utils
{
    public static class WindowManager
    {
        private static Dictionary<ViewType,List<int>> _viewIdPair = new Dictionary<ViewType, List<int>>();

        public static bool CanCreateView(ViewType viewType, int viewId)
        {
            List<int> viewIds = _viewIdPair.GetValueOrDefault(viewType);
            return viewIds?.Count == 0;
        }

        public static void ViewDeleted(ViewType viewType, int viewId)
        {
            List<int> viewIds = _viewIdPair.GetValueOrDefault(viewType);
            viewIds.Remove(viewId);
        }

        public static int GetSettingsId()
        {
            _viewIdPair.TryGetValue(ViewType.SETTINGS, out var viewIds);
            return viewIds.FirstOrDefault();
        }

        public static void AddSettingId(int id)
        {
            _viewIdPair.GetValueOrDefault(ViewType.SETTINGS).Add(id);
        }
    }

    public enum ViewType
    {
        SETTINGS,
        OTHERS
    }
}
