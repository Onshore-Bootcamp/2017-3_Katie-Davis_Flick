namespace Capstone_CharityBLL.Interfaces
{
    public interface IItemBO
    {
         long ItemID { get; set; }
         long UserID { get; set; }
         string ItemName { get; set; }
         bool Used { get; set; }
         string Description { get; set; }
    }
}
