namespace Capstone_FosterCharity.Custom
{
    using Capstone_CharityBLL.Interfaces;
    using Capstone_CharityDAL.Interfaces;
    using Capstone_CharityDAL.Models;
    using Capstone_FosterCharity.Models;
    using System.Collections.Generic;

    public class ItemMap
    {
        public static IItemDO MapPOtoDO(ItemPO iFrom)
        {
            IItemDO oTo = new ItemDO();//creating new instance
            //DO            //PO
            oTo.ItemID = iFrom.ItemID;
            oTo.UserID = iFrom.UserID;
            oTo.ItemName = iFrom.ItemName;
            oTo.Used = iFrom.Used;
            oTo.Description = iFrom.Description;

            return oTo; //return DO
        }

        public static ItemPO MapDOtoPO(IItemDO iFrom)
        {
            ItemPO oTo = new ItemPO(); //creating a new instance

            //PO            //DO
            oTo.ItemID = iFrom.ItemID;
            oTo.UserID = iFrom.UserID;
            oTo.ItemName = iFrom.ItemName;
            oTo.Used = iFrom.Used;
            oTo.Description = iFrom.Description;

            return oTo; //return PO
        }

        public static List<ItemPO> MapDOtoPO(List<IItemDO> iFrom)
        {
            List<ItemPO> iMap = new List<ItemPO>(); //Creating a new instance
            foreach (IItemDO prop in iFrom)
            {
                ItemPO map = MapDOtoPO(prop);   //Going thourgh the other DOtoPO method
                iMap.Add(map); //Adding each property allow it to be mapped
                            }

            return iMap; //Return the mapped list
        }

      





    }
}