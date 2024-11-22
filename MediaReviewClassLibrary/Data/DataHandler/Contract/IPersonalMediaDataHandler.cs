using MediaReviewClassLibrary.Models.Enitites;
using System.Threading.Tasks;

namespace MediaReviewClassLibrary.Data.DataHandler.Contract
{
    public interface IPersonalMediaDataHandler
    {
        Task<PersonalMedia> GetPersonalMedia(long mediaId, long userId);
        Task<PersonalMedia> UpdatePersonalMedia(PersonalMedia personalMedia);
    }
}
