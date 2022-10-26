using InfonetAssociates.DAL.DatabaseContexts;
using Microsoft.EntityFrameworkCore;

namespace InfonetAssociates.Services.Extentions
{
    public static class ApplicationServiceExtentions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatabaseContext>(options =>

            options.UseSqlServer(configuration.GetConnectionString("Connection")), ServiceLifetime.Transient);
            return services;
        }
    }
}
