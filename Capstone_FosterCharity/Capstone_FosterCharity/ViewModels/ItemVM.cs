namespace Capstone_FosterCharity.ViewModels
{
    using Capstone_FosterCharity.Models;
    using System.Collections.Generic;

    public class ItemVM
    {
        public ItemVM() //Constructor
        {
            Item = new ItemPO(); //new instance
            ItemList = new List<ItemPO>(); //new instance of list
        }
        public ItemPO Item { get; set; } //All PO's in Item
        public List<ItemPO> ItemList { get; set; }
        public string ErrorMessage { get; set; }
    }
}