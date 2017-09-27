namespace Capstone_CharityBLL.Models
{
    using Capstone_CharityBLL.Interfaces;

    public class DonationBO : IDonationBO
    {
        public long DonationID { get; set; }
        public long UserID { get; set; }
        public decimal Amount { get; set; }
        public decimal CardNumber { get; set; }
        public bool Rendered { get; set; }
    }
}
