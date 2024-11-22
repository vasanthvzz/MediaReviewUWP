using MediaReviewClassLibrary.Data.Contract;
using MediaReviewClassLibrary.Data.DataHandler.Contract;
using MediaReviewClassLibrary.Models.Enitites;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;

namespace MediaReviewClassLibrary.Data.DataHandler
{
    public class PersonalMediaDataHandler : IPersonalMediaDataHandler
    {

        private IDatabaseAdapter _databaseAdapter = MediaReviewDIServiceProvider.GetServiceProvider().GetRequiredService<IDatabaseAdapter>();

        public async Task<PersonalMedia> GetPersonalMedia(long mediaId, long userId)
        {
            return await  _databaseAdapter.GetTableQuery<PersonalMedia>()
                .Where(personalMedia => personalMedia.MediaId == mediaId && personalMedia.UserId == userId)
                .FirstOrDefaultAsync();
        }

        public async Task<PersonalMedia> UpdatePersonalMedia(PersonalMedia personalMedia)
        {
            var existingRecord = await GetPersonalMedia(personalMedia.MediaId, personalMedia.UserId);

            if (existingRecord == null)
            { 
                await _databaseAdapter.InsertAsync(personalMedia);
            }
            else
            {
                var customQuery = "UPDATE personal_media SET is_favourite = ?, has_watched = ?, watchlist = ? WHERE media_id = ? AND user_id = ?";
                await _databaseAdapter.ExecuteQuery<PersonalMedia>(customQuery, personalMedia.IsFavourite, personalMedia.HasWatched, personalMedia.WatchList, personalMedia.MediaId, personalMedia.UserId);
            }
            return await GetPersonalMedia(personalMedia.MediaId, personalMedia.UserId);
        }
    }
}
