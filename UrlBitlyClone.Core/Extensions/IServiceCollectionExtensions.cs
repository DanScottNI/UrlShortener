using Microsoft.Extensions.DependencyInjection;
using UrlBitlyClone.Core.Enums;
using UrlBitlyClone.Core.Services;
using UrlBitlyClone.Core.Services.Interfaces;

namespace UrlBitlyClone.Core.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddUrlShortenerLibraries(this IServiceCollection serviceProvider, StringHashTypes stringHashTypes)
        {
            serviceProvider.AddScoped<IUrlShortenerService, UrlShortenerService>();

            switch (stringHashTypes)
            {
                case StringHashTypes.TruncatedMd5:
                    serviceProvider.AddScoped<IStringHashService, Md5StringHashService>();
                    break;
                case StringHashTypes.None:
                    serviceProvider.AddScoped<IStringHashService, NoneStringHashService>();
                    break;
            }

            return serviceProvider;
        }
    }
}
