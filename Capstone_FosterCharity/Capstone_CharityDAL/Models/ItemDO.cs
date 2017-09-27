using Capstone_CharityDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone_CharityDAL.Models
{
   public class ItemDO : IItemDO
    {
        public long ItemID { get; set; }
        public long UserID { get; set; }
        public string ItemName { get; set; }
        public bool Used { get; set; }
        public string Description { get; set; }




    }
}
