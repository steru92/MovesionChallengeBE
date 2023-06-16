using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MovesionChallengeWebApi.Entities;
using MovesionChallengeWebApi.Helpers;
using MovesionChallengeWebApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MovesionChallengeWebApi.Services
{
    public interface IAuthService
    {
        AuthResponse Authenticate(AuthRequest model);
        IEnumerable<User> GetAll();
        User GetById(int id);
    }

    public class AuthService : IAuthService
    {
        private readonly AppSettings _appSettings;

        /// <summary>
        /// harcoded users for simplicity
        /// </summary>
        private readonly List<User> _users = new()
        {
            new User(1, "Mario", "Verdi", "mario.verdi", "Passw0rd2023!"),
            new User(2, "Claudio", "Rossi", "claudio.rossi", "2023Passw0rd!"),
            new User(3, "Mirco", "Bianchi", "mirco.bianchi", "!Passw0rd2023")
        };

        public AuthService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public AuthResponse Authenticate(AuthRequest model)
        {
            var user = _users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

            /// return null if user not found
            if (user == null) return null;

            /// authentication successful so generate jwt token
            var token = GenerateJwtToken(user, model.Expiration);

            return new AuthResponse(user, token);
        }

        public IEnumerable<User> GetAll()
        {
            return _users;
        }

        public User GetById(int id)
        {
            return _users.FirstOrDefault(x => x.Id == id);
        }

        private string GenerateJwtToken(User user, int? expiration)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(Constants.Constants.CLAIM_ID, user.Id.ToString()) }),
                /// token expires after 5 minutes if expiration minutes are not declared by client
                Expires = expiration.HasValue ? DateTime.UtcNow.AddMinutes(expiration.Value) : DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}