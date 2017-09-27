namespace Capstone_FosterCharity.ViewModels
{
    using Models;
    using System.Collections.Generic;

    public class DonationVM
    {
        public DonationVM() //Constructor 
        {
            Donation = new DonationPO(); //new instance
            DonationList = new List<DonationPO>(); //new instance of list
        }

        public DonationPO Donation { get; set; } //all PO's in Donation
        public List<DonationPO> DonationList { get; set; }
        public string ErrorMessage { get; set; }
    }
}