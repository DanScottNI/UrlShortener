using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace UrlBitlyClone.Tests.Infrastructure.Stubs
{

    public class FakeHttpContextAccessor : IHttpContextAccessor
    {
        private HttpContext _httpContextCurrent;
        private readonly IServiceProvider serviceProvider;

        public FakeHttpContextAccessor(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public HttpContext HttpContext
        {
            get
            {
                return _httpContextCurrent ??= GetStuff();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        private HttpContext GetStuff()
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, HttpContextConstants.Username),
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim("name", HttpContextConstants.Username),
            };

            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            return new DefaultHttpContext
            {
                Session = SetUpSession(),
                User = claimsPrincipal,
                RequestServices = serviceProvider,
            };
        }

        private FakeHttpSession SetUpSession() => SetUpSession(null);

        private FakeHttpSession SetUpSession(Action<FakeHttpSession> configure)
        {
            FakeHttpSession mockSession = new FakeHttpSession();

            if (configure != null)
            {
                configure(mockSession);
            }

            return mockSession;
        }
    }

}
