using System;

namespace TwitterSearcher.Models
{
    public class User
    {
        private int UserID { get; set; } = 1;

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public User()
        {
            UserID++;
        }
    }

    

}