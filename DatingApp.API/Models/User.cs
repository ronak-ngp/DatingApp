namespace DatingApp.API.Models
{
    public class UserDatingApp
    {
        public int Id { get; set; }

        public string Username {get; set;}

        public byte[] PassowrdHash {get; set;}

        public byte[] PasswordSalt {get; set;}
    }
}