using FluentValidation;
using FluentValidation.AspNetCore;
using System.Reflection;

namespace BACampusApp.DataAccess.Extentesions
{
    public static class FluentValidationExtensions
    {
        public static IServiceCollection AddFluentValidationWithAssemblies(this IServiceCollection services)
        {

            services
            .AddFluentValidationAutoValidation(config => config.DisableDataAnnotationsValidation = true) //"config => config.DisableDataAnnotationsValidation = true" yapılmasının sebebi validation mesajları gelirken DATA ANNOTATİON tarafından düşen default mesajları devre dışı bırakmak için yapılmıştır.
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
