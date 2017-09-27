namespace Capstone_FosterCharity.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ItemPO
    {
        public long ItemID { get; set; }

        public long UserID { get; set; }

        [Display(Name = "Item Name")] //How it looks
        [Required] //Need to complete form
        public string ItemName { get; set; }

        [Required] //Need to complete form
        public bool Used { get; set; }

        [Required] //Need to complete form
        public string Description { get; set; }
    }
}