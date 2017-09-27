using System;

namespace Capstone_CharityBLL.Interfaces
{
    public interface IUserBO
    {
        long UserID { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Phone { get; set; }
        string HouseAptNumber { get; set; }
        string StreetName { get; set; }
        string City { get; set; }
        string State { get; set; }
        string Zip { get; set; }
        Int16 Role { get; set; }
        string Username { get; set; }
        string Password { get; set; }
    }
}
