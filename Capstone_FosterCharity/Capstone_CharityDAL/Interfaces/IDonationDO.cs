using System;

namespace Capstone_CharityDAL.Interfaces
{
   public interface IDonationDO
    {
         long DonationID { get; set; }
         long UserID { get; set; }
         decimal Amount { get; set; }
        Int64 CardNumber { get; set; }
         bool Rendered { get; set; }
    }
}
