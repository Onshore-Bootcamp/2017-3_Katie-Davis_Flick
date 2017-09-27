namespace Capstone_FosterCharity.Custom
{
    using Capstone_CharityBLL.Interfaces;
    using Capstone_CharityBLL.Models;
    using Capstone_CharityDAL.Interfaces;
    using Capstone_CharityDAL.Models;
    using Models;
    using System.Collections.Generic;
    using ViewModels;

    public class DonationMap
    {
        public static IDonationDO MapPOtoDO(DonationPO iFrom)
        {
            IDonationDO oTo = new DonationDO(); //creating instance of an object 
            //DO                //PO
            oTo.DonationID = iFrom.DonationID;
            oTo.UserID = iFrom.UserID;
            oTo.Amount = iFrom.Amount;
            oTo.CardNumber = iFrom.CardNumber;
            oTo.Rendered = iFrom.Rendered;

            return oTo; //returning that object
        }

        public static DonationPO MapDOtoPO(IDonationDO iFrom)
        {
            DonationPO oTo = new DonationPO(); //creating new instance PO
            //DO                //PO
            oTo.DonationID = iFrom.DonationID;
            oTo.UserID = iFrom.UserID;
            oTo.Amount = iFrom.Amount;
            oTo.CardNumber = iFrom.CardNumber;
            oTo.Rendered = iFrom.Rendered;

            return oTo; //returning object
        }

        public static List<DonationPO> MapDOtoPO(List<IDonationDO> iFrom)
        {
            List<DonationPO> iMap = new List<DonationPO>(); //creating new instance of a list
            foreach (IDonationDO prop in iFrom) //loop to go through each property 
            {
                DonationPO map = MapDOtoPO(prop); //setting mapping info to variable
                iMap.Add(map); //Adding each property one by one

            }
            return iMap; //return new list
        }
        public static DonationBO MapDOtoBO(IDonationDO iFrom)
        {
            DonationBO oTo = new DonationBO(); //creating new instance PO
            //BO                //DO
            oTo.DonationID = iFrom.DonationID;
            oTo.UserID = iFrom.UserID;
            oTo.Amount = iFrom.Amount;
            oTo.CardNumber = iFrom.CardNumber;
            oTo.Rendered = iFrom.Rendered;

            return oTo; //returning object
        }

        public static List<IDonationBO> MapDOtoBO(List<IDonationDO> iFrom)
        {
            List<IDonationBO> Map = new List<IDonationBO>(); //creating new instance of a list
            foreach (IDonationDO prop in iFrom) //loop to go through each property 
            {
                IDonationBO map = MapDOtoBO(prop); //setting mapping info to variable
                Map.Add(map); //Adding each property one by one
                            }
            return Map; //return new list
        }
    }
}