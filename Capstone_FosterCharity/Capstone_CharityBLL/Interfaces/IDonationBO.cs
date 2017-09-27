namespace Capstone_CharityBLL.Interfaces
{
   public interface IDonationBO
    {
         long DonationID { get; set; }
         long UserID { get; set; }
         decimal Amount { get; set; }
        decimal CardNumber { get; set; }
         bool Rendered { get; set; }
    }
}
