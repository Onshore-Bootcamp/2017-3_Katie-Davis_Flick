using Capstone_CharityDAL.Interfaces;
using System;

namespace Capstone_CharityDAL.Models
{
    public class UserDO :IUserDO
    {
        public long UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Int64 PhoneNumber { get; set; }
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
