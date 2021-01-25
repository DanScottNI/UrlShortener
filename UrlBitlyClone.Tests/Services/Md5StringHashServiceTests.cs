using FluentAssertions;
using UrlBitlyClone.Core.Services;
using UrlBitlyClone.Core.Services.Interfaces;
using Xunit;

namespace UrlBitlyClone.Tests.Services
{
    public class Md5StringHashServiceTests
    {
        private const string Url = "http://www.google.co.uk";

        [Fact]
        public void StringHashed()
        {
            // Got this by hashing the above URL through https://www.md5hashgenerator.com/
            string HashString = "d5d4cf8ec8dc8fddc90b7024afa3ddb3".Substring(0, 8);
            IStringHashService stringHashService = new Md5StringHashService();
            stringHashService.HashUrl(Url).Should().BeEquivalentTo(HashString);
        }

        /// <summary>
        /// Checks that the hash is 8 characters.
        /// </summary>
        [Fact]
        public void StringHashLengthIsEight()
        {
            IStringHashService stringHashService = new Md5StringHashService();
            stringHashService.HashUrl(Url).Length.Should().Be(8);
        }

        /// <summary>
        /// Checks that this hasher isn't just spitting back the original values.
        /// </summary>
        [Fact]
        public void StringHashIsntOriginal()
        {
            IStringHashService stringHashService = new Md5StringHashService();
            stringHashService.HashUrl(Url).Should().NotBeEquivalentTo(Url);

        }
    }
}
