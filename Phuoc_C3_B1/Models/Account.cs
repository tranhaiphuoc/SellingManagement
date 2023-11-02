

namespace Phuoc_C3_B1.Models
{
    public class Account
    {
        private static int _id = 1;


        public int Id { get; set; }
        public string Username { get; }
        public string Password { get; }
        public int Role { get; }


        public Account(string username, string password, int role)
        {
            Id = _id++;
            Username = username;
            Password = password;
            Role = role;
        }
    }
}
