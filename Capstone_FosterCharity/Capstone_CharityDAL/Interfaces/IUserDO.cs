namespace Capstone_CharityDAL.Interfaces
{
    using System;

    public interface IUserDO
    {
        long UserID { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        Int64 PhoneNumber { get; set; }
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
