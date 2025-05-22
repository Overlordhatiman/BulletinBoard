using Azure.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace BulletinBoard.Api.GoogleHandler
{
    public class GoogleAccessTokenHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public GoogleAccessTokenHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IHttpClientFactory httpClientFactory)
            : base(options, logger, encoder, clock)
        {
            _httpClientFactory = httpClientFactory;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var authHeader = Request.Headers["Authorization"].ToString();

            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
            {
                return AuthenticateResult.Fail("No bearer token");
            }

            var token = authHeader["Bearer ".Length..];

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://www.googleapis.com/oauth2/v3/tokeninfo?access_token={token}");

            if (!response.IsSuccessStatusCode)
            {
                return AuthenticateResult.Fail("Invalid access token");
            }

            var payload = await response.Content.ReadFromJsonAsync<GoogleTokenInfo>();

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, payload.sub),
            new Claim(ClaimTypes.Name, payload.name ?? ""),
            new Claim(ClaimTypes.Email, payload.email ?? "")
        };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }

    public class GoogleTokenInfo
    {
        public string sub { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string aud { get; set; }
    }
}
