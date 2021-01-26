using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;

namespace UrlBitlyClone.Tests.Infrastructure.Stubs
{

    public class FakeIHostingEnvironment : IWebHostEnvironment
    {
        public IFileProvider WebRootFileProvider { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IFileProvider ContentRootFileProvider { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string WebRootPath { get; set; } = "wwwroot";
        public string ApplicationName { get; set; } = "testapp";
        public string ContentRootPath { get; set; } = ".";
        public string EnvironmentName { get; set; } = "Development";
    }

}
