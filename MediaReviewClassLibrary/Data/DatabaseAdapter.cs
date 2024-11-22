using MediaReviewClassLibrary.Data.Contract;
using MediaReviewClassLibrary.Models.Enitites;
using SQLite;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;

namespace MediaReviewClassLibrary.Data
{
    public class DatabaseAdapter : IDatabaseAdapter
    {
        private static SQLiteAsyncConnection _db;
        private static SQLiteConnection _dbSync;

        static DatabaseAdapter()
        {
            string folderPath = ApplicationData.Current.LocalFolder.Path;
            string databasePath = Path.Combine(folderPath, "media_db.db");
            Debug.WriteLine($"Database Path: {databasePath}");
            _db = new SQLiteAsyncConnection(databasePath);

            _db.CreateTableAsync<ActorMapper>();
            _db.CreateTableAsync<Artist>();
            _db.CreateTableAsync<DirectorMapper>();
            _db.CreateTableAsync<FollowerMappper>();
            _db.CreateTableAsync<Genre>();
            _db.CreateTableAsync<GenreMapper>();
            _db.CreateTableAsync<Media>();
            _db.CreateTableAsync<PersonalMedia>();
            _db.CreateTableAsync<Rating>();
            _db.CreateTableAsync<Reaction>();
            _db.CreateTableAsync<Review>();
            _db.CreateTableAsync<UserDetail>();
            DataLoader.LoadData();
        }

        public async Task<int> DeleteAsync<T>(long id) where T : new()
        {
            return await _db.DeleteAsync<T>(id);
        }

        public async Task<T> FindAsync<T>(long id) where T : new()
        {
            return await _db.FindAsync<T>(id);
        }

        public async Task<int> DeleteAsync<T>(T obj) where T : new()
        {
            return await _db.DeleteAsync<T>(obj);
        }

        public AsyncTableQuery<T> GetTableQuery<T>() where T : new()
        {
            return _db.Table<T>();
        }

        public async Task<List<T>> GetTableAsync<T>() where T : new()
        {
            return await _db.Table<T>().ToListAsync();
        }

        public async Task<int> InsertAsync<T>(T obj) where T : new()
        {
            return await _db.InsertAsync(obj);
        }

        public async Task<int> InsertOrReplaceAsync<T>(T obj) where T : new()
        {
            return await _db.InsertOrReplaceAsync(obj);
        }

        public async Task<List<T>> ExecuteQuery<T>(string query,params object[] args) where T : new()
        {
            return await _db.QueryAsync<T>(query,args);
        }

        public async Task<int> UpdateAsync<T>(T obj) where T : new()
        {
            return await _db.UpdateAsync(obj);
        }

    }
}
