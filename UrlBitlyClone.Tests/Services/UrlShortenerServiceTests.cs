using FizzWare.NBuilder;
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
        private readonly ObjectMother mother;
        private readonly IUrlShortenerService urlShortenerService;

        public UrlShortenerServiceTests(ObjectMother mother, IUrlShortenerService urlShortenerService)
        {
            this.mother = mother;
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
            var saved = this.mother.GetFirstEntity<UrlShortening>();
            saved.Should().NotBeNull();
            saved.FullUrl.Should().Be(Url);
        }
    }
}
