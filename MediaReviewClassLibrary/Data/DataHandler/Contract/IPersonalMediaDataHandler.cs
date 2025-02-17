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

        Task<long> RemoveFromPersonalisedList(long userId, long mediaId, PersonalMediaType personalisedType);
    }
}