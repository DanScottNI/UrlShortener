using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UrlBitlyClone.Core.Context;
using UrlBitlyClone.Core.Services.Interfaces;

namespace UrlBitlyClone.Core.Services
{
    /// <summary>
    /// Implementation of the <see cref="IUrlShortenerService"/>.
    /// </summary>
    public class UrlShortenerService : IUrlShortenerService
    {
        private readonly UrlBitlyCloneContext context;
        private readonly IStringHashService stringHashService;
        private readonly ILogger<UrlShortenerService> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlShortenerService"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="stringHashService">The string hashing service.</param>
        /// <param name="logger">The logger for this class.</param>
        public UrlShortenerService(UrlBitlyCloneContext context, IStringHashService stringHashService, ILogger<UrlShortenerService> logger)
        {
            this.context = context;
            this.stringHashService = stringHashService;
            this.logger = logger;
        }

        /// <inheritdoc/>
        public UrlShortForm Create(string url)
        {
            int retryCount = 0;
            bool saveFailed = false;

            do
            {
                if (retryCount < 10)
                {
                    try
                    {
                        UrlShortForm urlEntity = new UrlShortForm
                        {
                            FullUrl = url,
                            ShortenedUrl = this.stringHashService.HashUrl(url),
                        };

                        this.context.Add(urlEntity);
                        this.context.SaveChanges();

                        return urlEntity;
                    }
                    catch (DbUpdateException ex)
                    {
                        this.logger.LogInformation(ex, "This URL already exists");
                        saveFailed = true;
                    }
                }
            }
            while (saveFailed);

            return null;
        }

        /// <inheritdoc/>
        public UrlShortForm GetByShortUrl(string shortenedUrl)
        {
            return this.context.UrlShortForms.FirstOrDefault(x => x.ShortenedUrl == shortenedUrl);
        }
    }
}