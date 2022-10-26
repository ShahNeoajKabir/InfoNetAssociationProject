

using InfonetAssociates.BLLManager.ForCity.Interface;
using InfonetAssociates.BLLManager.ForCity.Logic;
using InfonetAssociates.BLLManager.ForCountry.Interface;
using InfonetAssociates.BLLManager.ForCountry.Logic;
using InfonetAssociates.BLLManager.ForLanguage.Interface;
using InfonetAssociates.BLLManager.ForLanguage.Logic;
using InfonetAssociates.BLLManager.ForMemberPersonInfo.Interfaces;
using InfonetAssociates.BLLManager.ForMemberPersonInfo.Logic;

namespace InfonetAssociates.Services.Extentions

{
    public static class InterfaceServiceDeclareExtentions
    {
        public static IServiceCollection AddScopedServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICountryBLLManager, CountryBLLManager>();
            services.AddScoped<ICityBLLManager, CityBLLManager>();
            services.AddScoped<ILanguageBLLManager, LanguageBLLManager>();
            services.AddScoped<IMemberPersonInfoBLLManager, MemberPersonInfoBLLManager>();
            
            return services;
        }
    }
}
