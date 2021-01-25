using UrlBitlyClone.Core.Context;

namespace UrlBitlyClone.Core.Services.Interfaces
{
    public interface IUrlShortenerService
    {
        UrlShortening Create(string url);
    }
}
