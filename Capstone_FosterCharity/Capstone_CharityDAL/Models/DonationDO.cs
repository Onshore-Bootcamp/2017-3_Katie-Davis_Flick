namespace Capstone_CharityDAL.Models
{
    using Capstone_CharityDAL.Interfaces;
    using System;

    public class DonationDO : IDonationDO
    {
        public long DonationID { get; set; }
        public long UserID { get; set; }
        public decimal Amount { get; set; }
        public Int64 CardNumber { get; set; }
        public bool Rendered { get; set; }
    }
}
