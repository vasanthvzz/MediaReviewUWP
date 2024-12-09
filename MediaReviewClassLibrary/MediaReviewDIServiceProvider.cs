using MediaReviewClassLibrary.Data;
using MediaReviewClassLibrary.Data.Contract;
using MediaReviewClassLibrary.Data.DataHandler;
using MediaReviewClassLibrary.Data.DataHandler.Contract;
using MediaReviewClassLibrary.DataManager;
using MediaReviewClassLibrary.Domain;
using MediaReviewClassLibrary.Utlis;
using Microsoft.Extensions.DependencyInjection;

namespace MediaReviewClassLibrary
{
    public class MediaReviewDIServiceProvider
    {
        private static ServiceProvider _serviceProvider;
        static MediaReviewDIServiceProvider()
        {
            ServiceCollection services = new ServiceCollection();

            //Data Manager
            services.AddSingleton<ILoginUserDataManager, LoginUserDataManager>();
            services.AddSingleton<ICreateUserDataManager, CreateUserDataManager>();
            services.AddSingleton<IGetAllMediaDataManager, GetAllMediaDataManager>();
            services.AddSingleton<IGetMediaDetailDataManager, GetMediaDetailDataManager>();
            services.AddSingleton<IUpdatePersonalMediaDataManager, UpdatePersonalMediaDataManager>();
            services.AddSingleton<IUpdateUserRatingDataManager, UpdateUserRatingDataManager>();
            services.AddSingleton<IAddReviewDataManager, AddReviewDataManager>();
            services.AddSingleton<IGetMediaReviewDataManager, GetMediaReviewDataManager>();
            services.AddSingleton<IGetPersonalisedMediaDataManager, GetPersonalisedMediaDataManager>();
            services.AddSingleton<IRemovePersonalisedMediaDataManager, RemovePersonalisedMediaDataManager>();
            services.AddSingleton<IEditReviewDataManager, EditReviewDataManager>();
            services.AddSingleton<IDeleteReviewDataManager, DeleteReviewDataManager>();
            services.AddSingleton<IUpdateFollowDataManager, UpdateFollowDataManager>();
            services.AddSingleton<IGetUserRatedMediaDataManager, GetUserRatedMediaDataManager>();
            services.AddSingleton<IGetMediaRatingDataManager, GetMediaRatingDataManager>();
            services.AddSingleton<IGetUserReviewDataManager, GetUserReviewDataManager>();
            
            //Data Handler
            services.AddSingleton<IUserDataHandler, UserDataHandler>();
            services.AddSingleton<IMediaDataHandler, MediaDataHandler>(); 
            services.AddSingleton<IPersonalMediaDataHandler, PersonalMediaDataHandler>();
            services.AddSingleton<IRatingDataHandler, RatingDataHandler>();
            services.AddSingleton<IReviewDataHandler, ReviewDataHandler>();
            services.AddSingleton<IGenreDataHandler, GenreDataHandler>();
            services.AddSingleton<IFolloweeDataHandler, FolloweeDataHandler>();

            //Data Adapter
            services.AddSingleton<IDatabaseAdapter, DatabaseAdapter>();
            services.AddSingleton<ISessionManager, SessionManager>();
            services.AddSingleton<IPasswordAdapter, PasswordAdapter>();

            //Build the service provider from the service collection.
            ServiceProvider serviceProvider = services.BuildServiceProvider();
            _serviceProvider = serviceProvider;
        }

        public static ServiceProvider GetServiceProvider()
        {
            return _serviceProvider;
        }

        public static T GetRequiredService<T>()
        {
            return _serviceProvider.GetRequiredService<T>();
        }
    }
}
