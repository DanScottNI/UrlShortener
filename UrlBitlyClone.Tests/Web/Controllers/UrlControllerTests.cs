using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using UrlBitlyClone.Controllers;
using UrlBitlyClone.Core.Context;
using UrlBitlyClone.Models.Url;
using UrlBitlyClone.Tests.Infrastructure;
using UrlBitlyClone.Tests.Infrastructure.Stubs;
using Xunit;

namespace UrlBitlyClone.Core.Web.Controllers
{
    /// <summary>
    /// Tests for the <see cref="UrlController"/>.
    /// </summary>
    public class UrlControllerTests
    {
        private readonly UrlController controller;
        private readonly ObjectMother objectMother;
        private readonly IConfiguration configuration;

        public UrlControllerTests(ITestableController<UrlController> cont, ObjectMother objectMother, IConfiguration configuration)
        {
            this.controller = cont.Controller;
            this.objectMother = objectMother;
            this.configuration = configuration;
        }

        [Fact]
        public void Details()
        {
            // Arrange
            var url = objectMother.WithUrls(1).GetFirstEntity<UrlShortForm>();

            // Act
            IActionResult result = controller.Details(url.ShortenedUrl);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();

            var view = result as ViewResult;
            view.Model.Should().NotBeNull();
            view.Model.Should().BeOfType<UrlDetailsModel>();

            var model = view.Model as UrlDetailsModel;

            model.FullUrl.Should().Be(url.FullUrl);
            model.ShortenedUrl.Should().Be(url.ShortenedUrl);
            string baseUrl = configuration.GetValue<string>("BaseUrl", "NotTheRightValue").TrimEnd('\\').TrimEnd('/');
            model.BaseUrl.Should().Be(baseUrl);
            model.BaseUrl.Should().NotEndWith("/");
            model.BaseUrl.Should().NotEndWith("\\");
        }

        [Fact]
        public void RedirectToUrl()
        {
            // Arrange
            var url = objectMother.WithUrls(1).GetFirstEntity<UrlShortForm>();

            // Act
            IActionResult result = controller.RedirectToUrl(url.ShortenedUrl);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<RedirectResult>();
            var redirect = result as RedirectResult;
            redirect.Permanent.Should().BeTrue();
        }
    }
}
