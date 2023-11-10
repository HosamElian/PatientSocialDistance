using Microsoft.EntityFrameworkCore;
using PatientSocialDistance.BusinessLogic.Services;
using PatientSocialDistance.BusinessLogic.Services.IServices;
using PatientSocialDistance.DataAccess.Data;
using PatientSocialDistance.DataAccess.Repository;
using PatientSocialDistance.DataAccess.Repository.IRepository;

namespace PatientSocialDistance.API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services,
        IConfiguration config)
        {
            services.AddDbContext<ApplicationDbContext>(option =>
                option.UseSqlServer(config.GetConnectionString("DefaultConnection")));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IInteractionService, InteractionService>();
            services.AddScoped<IVisitService, VisitService>();
            services.AddScoped<IBlockService, BlockService>();
            services.AddScoped<IUserService, UserService>();

            services.AddCors();
            return services;
        }
    }
}
