using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace IdentityServer4.AccessTokenValidation
{
    /// <summary>
    /// Extension methods for setting up access token validation services in an <see cref="IServiceCollection" />.
    /// </summary>
    public static class AccessTokenValidationServiceCollectionExtensions
    {
        /// <summary>
        /// Adds access token validation services to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns> 
        public static IServiceCollection AddAccessTokenValidation(this IServiceCollection services)
        {
            services.AddCaching();
            services.AddAuthentication();
            services.TryAdd(ServiceDescriptor.Singleton<IClock, SystemClock>());
            services.TryAdd(ServiceDescriptor.Singleton<IValidationResultCache, InMemoryValidationResultCache>());

            return services;
        }
    }
}
