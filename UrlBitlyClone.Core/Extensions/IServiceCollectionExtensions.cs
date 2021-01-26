using Microsoft.Extensions.DependencyInjection;
using UrlBitlyClone.Core.Enums;
using UrlBitlyClone.Core.Services;
using UrlBitlyClone.Core.Services.Interfaces;

namespace UrlBitlyClone.Core.Extensions
{
    /// <summary>
    /// Extension methods for quickly adding the necessary services to the DI container.
    /// </summary>
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the services to the DI container.
        /// </summary>
        /// <param name="serviceCollection">The <see cref="IServiceCollection"/> to inject the services into.</param>
        /// <param name="stringHashTypes">The type of hashing to use.</param>
        /// <returns>
        /// An <see cref="IServiceCollection"/> with the services added.
        /// </returns>
        public static IServiceCollection AddUrlShortenerLibraries(this IServiceCollection serviceCollection, StringHashTypes stringHashTypes)
        {
            serviceCollection.AddScoped<IUrlShortenerService, UrlShortenerService>();

            switch (stringHashTypes)
            {
                case StringHashTypes.TruncatedMd5:
                    serviceCollection.AddScoped<IStringHashService, Md5StringHashService>();
                    break;
                case StringHashTypes.None:
                    serviceCollection.AddScoped<IStringHashService, NoneStringHashService>();
                    break;
            }

            return serviceCollection;
        }
    }
}