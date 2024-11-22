using System;

namespace MediaReviewClassLibrary.Utlis
{
    public static class IdentityManager
    {
        public static long GenerateUniqueId()
        {
            return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        }
    }
}
