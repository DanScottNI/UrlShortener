using FluentAssertions;
using UrlBitlyClone.Core.Services;
using UrlBitlyClone.Core.Services.Interfaces;
using Xunit;

namespace UrlBitlyClone.Tests.Services
{
    public class Md5StringHashServiceTests
    {
        private const string Url = "http://www.google.co.uk";
        private readonly IStringHashService stringHashService;

        public Md5StringHashServiceTests(IStringHashService stringHashService)
        {
            this.stringHashService = stringHashService;
        }

        [Fact]
        public void StringHashed()
        {
            // Got this by hashing the above URL through https://www.md5hashgenerator.com/
            stringHashService.HashUrl(Url).Should().NotBeEquivalentTo(Url);
        }

        /// <summary>
        /// Checks that the hash is 8 characters.
        /// </summary>
        [Fact]
        public void StringHashLengthIsEight()
        {
            stringHashService.HashUrl(Url).Length.Should().Be(8);
        }

        /// <summary>
        /// Checks that this hasher isn't just spitting back the original values.
        /// </summary>
        [Fact]
        public void StringHashIsntOriginal()
        {
            stringHashService.HashUrl(Url).Should().NotBeEquivalentTo(Url);

        }
    }
}
