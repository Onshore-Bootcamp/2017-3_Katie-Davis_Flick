namespace Capstone_FosterCharity.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DonationPO
    {
        public long DonationID { get; set; }

        public long UserID { get; set; }

        [Required] //Has to be filled in
        [Range(1, double.MaxValue, ErrorMessage = "Please enter proper amount")] //info range
        [DisplayFormat(DataFormatString = "${0:0.00}")]  //how it was shown
        public decimal Amount { get; set; }

        [RegularExpression(@"^(\d{16})$", ErrorMessage = "Please enter proper card details")] //only 16 digits
        [Display(Name = "Card Number")] //Display name
        [Required] //Has to be filled in
        [DisplayFormat(DataFormatString = "{0:####-####-####-####}")] //Displayed with -
        public Int64 CardNumber { get; set; }

        [Required]
        public bool Rendered { get; set; }
    }
}