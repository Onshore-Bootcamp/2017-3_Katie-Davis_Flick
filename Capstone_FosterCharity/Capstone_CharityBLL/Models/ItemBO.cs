using Capstone_CharityBLL.Interfaces;

namespace Capstone_CharityBLL.Models
{
    class ItemBO : IItemBO
    {
        public long ItemID { get; set; }
        public long UserID { get; set; }
        public string ItemName { get; set; }
        public bool Used { get; set; }
        public string Description { get; set; }
    }
}
