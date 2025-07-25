//using Microsoft.Extensions.DependencyInjection;
//using SIGEBI.Application.Contracts;
//using SIGEBI.Persistence.Repositories;

//namespace SIGEBI.IOC.Dependency
//{
//    public static class ReservationDependency
//    {
//        public static class DependencyContainer
//        {
//            public static IServiceCollection AddProjectDependencies(this IServiceCollection services)
//            {

//                services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

//                // Repositorios
//                services.AddScoped<IReservationStatusesRepository, ReservationStatusesRepository>();
//                services.AddScoped<IReservationRepository, ReservationRepository>();
//                services.AddScoped<IReservationHistoryRepository, ReservationHistoryRepository>();

//                // Servicios de la capa de aplicación
//                services.AddTransient<IReservationService, ReservationService>();
//                services.AddTransient<IReservationStatusService, ReservationStatusService>();
//                services.AddTransient<IReservationHistoryService, ReservationHistoryService>();


//                services.AddScoped<ReservationApiService>();
//                services.AddScoped<ReservationStatusSApiervice>();
//                services.AddScoped<ReservationHistoryApiService>();

//                return services;
//            }
//        }
//    }
//}
