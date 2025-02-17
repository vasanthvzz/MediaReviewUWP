using System;

namespace MediaReviewClassLibrary.Utility
{
    public static class IdentityManager
    {
        public static long GenerateUniqueId()
        {
            return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        }
    }
}