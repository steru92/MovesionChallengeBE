using System.Text.Json.Serialization;

namespace MovesionChallengeWebApi.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        public User(int id, string firstName, string lastName, string userName, string password)
        {
            /// Id management implemented in this wrong way for simplicity
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Username = userName;
            Password = password;
        }
    }
}