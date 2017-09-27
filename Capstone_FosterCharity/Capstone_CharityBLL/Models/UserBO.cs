using Capstone_CharityBLL.Interfaces;
using System;

namespace Capstone_CharityBLL.Models
{
    public class UserBO : IUserBO
    {
        public long UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string HouseAptNumber { get; set; }
        public string StreetName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public Int16 Role { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
