using MediaReviewClassLibrary.Data.Contract;
using MediaReviewClassLibrary.Data.DataHandler.Contract;
using MediaReviewClassLibrary.Models.Constants;
using MediaReviewClassLibrary.Models.Enitites;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediaReviewClassLibrary.Data.DataHandler
{
    public class PersonalMediaDataHandler : IPersonalMediaDataHandler
    {
        private IDatabaseAdapter _databaseAdapter = MediaReviewDIServiceProvider.GetRequiredService<IDatabaseAdapter>();

        public async Task<PersonalMedia> GetPersonalMedia(long mediaId, long userId)
        {
            return await _databaseAdapter.GetAsyncTableQuery<PersonalMedia>()
                .Where(personalMedia => personalMedia.MediaId == mediaId && personalMedia.UserId == userId)
                .FirstOrDefaultAsync();
        }

        public async Task<List<PersonalMedia>> GetPersonalisedMedia(long userId, PersonalMediaType personalMediaType)
        {
            switch (personalMediaType)
            {
                case PersonalMediaType.FAVOURITE:
                    {
                        return await _databaseAdapter.GetAsyncTableQuery<PersonalMedia>()
                .Where(personalMedia => personalMedia.UserId == userId && personalMedia.IsFavourite)
                    .ToListAsync();
                    }
                case PersonalMediaType.WATCHLIST:
                    {
                        return await _databaseAdapter.GetAsyncTableQuery<PersonalMedia>()
                .Where(personalMedia => personalMedia.UserId == userId && personalMedia.WatchList)
                    .ToListAsync();
                    }
                case PersonalMediaType.HAS_WATCHED:
                    {
                        return await _databaseAdapter.GetAsyncTableQuery<PersonalMedia>()
                .Where(personalMedia => personalMedia.UserId == userId && personalMedia.HasWatched)
                    .ToListAsync();
                    }
                default:
                    {
                        throw new Exception("Invalid Personalised media");
                    }
            }
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
            return personalMedia;
        }

        public async Task<long> RemoveFromPersonalisedList(long userId, long mediaId,PersonalMediaType PersonalisedType)
        {
            string columnName = "";
            switch (PersonalisedType)
                {
                case PersonalMediaType.FAVOURITE:
                    {
                        columnName = "is_favourite";
                        break;
                    }
                case PersonalMediaType.WATCHLIST:
                    {
                        columnName = "watchlist";
                        break;
                    }
                case PersonalMediaType.HAS_WATCHED:
                    {
                        columnName = "has_watched";
                        break;
                    }
                }
            var customQuery = "UPDATE personal_media SET " + columnName + " = ? WHERE media_id = ? AND user_id = ?";
            await _databaseAdapter.ExecuteQuery<PersonalMedia>(customQuery, false, mediaId, userId);
            return mediaId;
        }
    }
}