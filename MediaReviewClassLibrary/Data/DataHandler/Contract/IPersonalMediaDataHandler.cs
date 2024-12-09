using MediaReviewClassLibrary.Models.Constants;
using MediaReviewClassLibrary.Models.Enitites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediaReviewClassLibrary.Data.DataHandler.Contract
{
    public interface IPersonalMediaDataHandler
    {
        Task<PersonalMedia> GetPersonalMedia(long mediaId, long userId);
        Task<PersonalMedia> UpdatePersonalMedia(PersonalMedia personalMedia);
        Task<List<PersonalMedia>> GetPersonalisedMedia(long userId, PersonalMediaType personalMediaType);
        Task<long> RemoveFromFavourite(long userId, long mediaId);
        Task<long> RemoveFromWatchList(long userId, long mediaId);
        Task<long> RemoveFromHasWatched(long userId, long mediaId);
    }
}
