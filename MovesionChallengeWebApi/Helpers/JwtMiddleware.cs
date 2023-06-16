using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MovesionChallengeWebApi.Entities;
using MovesionChallengeWebApi.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace MovesionChallengeWebApi.Helpers
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context, IAuthService authService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                AttachUserToContext(context, authService, token);
            }
            
            await _next(context);
        }

        private void AttachUserToContext(HttpContext context, IAuthService authService, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    /// this way, tokens expire exactly at token expiration time
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == Constants.Constants.CLAIM_ID).Value);

                /// attach user to context on successful jwt validation
                context.Items[nameof(User)] = authService.GetById(userId);
            }
            catch
            {
                /// does nothing if jwt validation fails
                /// user is not attached to context so request won't have access to secure routes
            }
        }
    }
}