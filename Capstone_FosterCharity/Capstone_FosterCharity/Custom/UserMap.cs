namespace Capstone_FosterCharity.Custom
{
    using Capstone_CharityDAL.Interfaces;
    using Capstone_CharityDAL.Models;
    using Capstone_FosterCharity.Models;
    using System.Collections.Generic;

    public class UserMap
    {
        public static IUserDO MapPOtoDO(UserPO iFrom)
        {
            IUserDO oTo = new UserDO(); //create a new instance

            //DO            //PO
            oTo.UserID = iFrom.UserID;
            oTo.FirstName = iFrom.FirstName;
            oTo.LastName = iFrom.LastName;
            oTo.PhoneNumber = iFrom.PhoneNumber;
            oTo.HouseAptNumber = iFrom.HouseAptNumber;
            oTo.StreetName = iFrom.StreetName;
            oTo.City = iFrom.City;
            oTo.State = iFrom.State;
            oTo.Zip = iFrom.Zip;
            oTo.Role = iFrom.Role;
            oTo.Username = iFrom.Username;
            oTo.Password = iFrom.Password;

            return oTo; //Return DO
        }

        public static UserPO MapDOtoPO(IUserDO iFrom)
        {
            UserPO oTo = new UserPO(); //creating a new instance

            //PO                //DO
            oTo.UserID = iFrom.UserID;
            oTo.FirstName = iFrom.FirstName;
            oTo.LastName = iFrom.LastName;
            oTo.PhoneNumber = iFrom.PhoneNumber;
            oTo.HouseAptNumber = iFrom.HouseAptNumber;
            oTo.StreetName = iFrom.StreetName;
            oTo.City = iFrom.City;
            oTo.State = iFrom.State;
            oTo.Zip = iFrom.Zip;
            oTo.Role = iFrom.Role;
            oTo.Username = iFrom.Username;
            oTo.Password = iFrom.Password;

            return oTo; //return PO
        }

        public static List<UserPO> MapDOtoPO(List<IUserDO> iFrom)
        {
            List<UserPO> iMap = new List<UserPO>(); //Creating a new instance
            foreach (IUserDO prop in iFrom)
            {
                UserPO map = MapDOtoPO(prop);   //Going thourgh the other DOtoPO method
                iMap.Add(map); //Adding each property allow it to be mapped

            }

            return iMap; //Return the mapped list
        }
    }
}