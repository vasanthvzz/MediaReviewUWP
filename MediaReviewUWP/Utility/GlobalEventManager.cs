using System;

namespace MediaReviewUWP.Utility
{
    public static class GlobalEventManager
    {
        public static event EventHandler<MediaAddedEventArgs> OnMediaAdded;

        public static void MediaAdded(MediaAddedEventArgs e)
        {
            OnMediaAdded?.Invoke(null,e);
        }

        public static void MediaAdded(long mediaId, DateTimeOffset releaseDate)
        {
            OnMediaAdded?.Invoke(null,new MediaAddedEventArgs(mediaId, releaseDate));
        }
    }

    public class MediaAddedEventArgs : EventArgs
    {
        public long MediaId { get; private set; }
        public DateTimeOffset ReleaseDate { get; private set; }

        public MediaAddedEventArgs(long mediaId, DateTimeOffset releaseDate)
        {
            MediaId = mediaId;
            ReleaseDate = releaseDate;
        }
    }
}