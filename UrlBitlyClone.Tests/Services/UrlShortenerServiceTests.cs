using FluentAssertions;
using UrlBitlyClone.Core.Context;
using UrlBitlyClone.Core.Services.Interfaces;
using UrlBitlyClone.Tests.Infrastructure;
using Xunit;

namespace UrlBitlyClone.Tests.Services
{
    public class UrlShortenerServiceTests
    {
        private const string Url = "http://www.google.co.uk";
        private readonly ObjectMother objectMother;
        private readonly IUrlShortenerService urlShortenerService;

        public UrlShortenerServiceTests(ObjectMother mother, IUrlShortenerService urlShortenerService)
        {
            this.objectMother = mother;
            this.urlShortenerService = urlShortenerService;
        }

        [Fact]
        public void Create()
        {
            // Act
            var obj = urlShortenerService.Create(Url);

            // Assert
            obj.Should().NotBeNull();
            obj.FullUrl.Should().Be(Url);
            obj.ShortenedUrl.Should().NotBeNullOrWhiteSpace();

            // Did this actually get saved?
            var saved = this.objectMother.GetFirstEntity<UrlShortForm>();
            saved.Should().NotBeNull();
            saved.FullUrl.Should().Be(Url);
        }

        [Fact]
        public void Get()
        {
            // Arrange
            var url = objectMother.WithUrls(1).GetFirstEntity<UrlShortForm>();

            // Act
            UrlShortForm obj = urlShortenerService.GetByShortUrl(url.ShortenedUrl);
            
            // Assert
            obj.Should().NotBeNull();
            obj.ShortenedUrl.Should().BeEquivalentTo(url.ShortenedUrl);
        }
    }
}
