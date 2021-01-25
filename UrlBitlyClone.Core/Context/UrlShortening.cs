#nullable disable

namespace UrlBitlyClone.Core.Context
{
    public class UrlShortening
    {
        public long UrlShorteningId { get; set; }
        public string FullUrl { get; set; }
        public string ShortenedUrl { get; set; }
    }
}
