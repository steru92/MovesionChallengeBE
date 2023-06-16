using System.ComponentModel.DataAnnotations;

namespace MovesionChallengeWebApi.Models
{
    public class AuthRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
        public int? Expiration { get; set; }
    }
}