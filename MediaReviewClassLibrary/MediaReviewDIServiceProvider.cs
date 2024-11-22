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

            // 2. Register (add and configure) the services.
            //services.AddSingleton<IMediaDatabase, MediaDatabase>();
            //services.AddSingleton<IAddMediaDataManager, AddMediaDataManager>();
            //services.AddSingleton<IAddPersonalMediaDataManager, AddPersonalMediaDataManager>();
            //services.AddSingleton<IAddRatingDataManager, AddRatingDataManager>();
            //services.AddSingleton<IAddReactionDataManager, AddReactionDataManager>();
            //services.AddSingleton<IAddReviewDataManager, AddReviewDataManager>();
            //services.AddSingleton<ICreateUserDataManager, CreateUserDataManager>();
            //services.AddSingleton<IFilterMediaByGenreDataManager, FilterMediaByGenreDataManager>();
            //services.AddSingleton<IGenreValidationDataManager, GenreValidationDataManager>();
            //services.AddSingleton<IGetAllMediaDataManager, GetAllMediaDataManager>();
            //services.AddSingleton<IGetPersonalMediaDataManager, GetPersonalMediaDataManager>();
            //services.AddSingleton<IGetRatedMediaDataManager, GetRatedMediaDataManager>();
            //services.AddSingleton<IGetReactedMediaDataManager, GetReactedMediaDataManager>();
            //services.AddSingleton<IGetMediaDetailDataManager, GetSingleMediaDataManager>();
            //services.AddSingleton<IGetUserReviewDataManager, GetUserReviewDataManager>();
            //services.AddSingleton<ISearchMediaDataManager, SearchMediaDataManager>();
            //services.AddSingleton<IShowGenresDataManager, ShowGenresDataManager>();
            services.AddSingleton<ILoginUserDataManager, LoginUserDataManager>();
            services.AddSingleton<ICreateUserDataManager, CreateUserDataManager>();
            services.AddSingleton<IGetAllMediaDataManager, GetAllMediaDataManager>();
            services.AddSingleton<IGetMediaDetailDataManager, GetMediaDetailDataManager>();
            services.AddSingleton<IUpdatePersonalMediaDataManager, UpdatePersonalMediaDataManager>();
            services.AddSingleton<IUpdateUserRatingDataManager, UpdateUserRatingDataManager>();
            services.AddSingleton<IAddReviewDataManager, AddReviewDataManager>();
            services.AddSingleton<IGetMediaReviewDataManager, GetMediaReviewDataManager>();
            


            services.AddSingleton<IUserDataHandler, UserDataHandler>();
            services.AddSingleton<IMediaDataHandler, MediaDataHandler>(); 
            services.AddSingleton<IPersonalMediaDataHandler, PersonalMediaDataHandler>();
            services.AddSingleton<IRatingDataHandler, RatingDataHandler>();
            services.AddSingleton<IReviewDataHandler, ReviewDataHandler>();

            services.AddSingleton<IDatabaseAdapter, DatabaseAdapter>();
            services.AddSingleton<ISessionManager, SessionManager>();
            services.AddSingleton<IPasswordAdapter, PasswordAdapter>();

            // 3. Build the service provider from the service collection.
            ServiceProvider serviceProvider = services.BuildServiceProvider();
            _serviceProvider = serviceProvider;
        }

        public static ServiceProvider GetServiceProvider()
        {
            return _serviceProvider;
        }
    }
}
