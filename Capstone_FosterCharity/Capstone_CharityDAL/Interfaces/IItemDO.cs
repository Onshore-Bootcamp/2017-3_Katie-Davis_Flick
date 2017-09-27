namespace Capstone_CharityDAL.Interfaces
{
    public interface IItemDO
    {
        long ItemID { get; set; }
        long UserID { get; set; }
        string ItemName { get; set; }
        bool Used { get; set; }
        string Description { get; set; }
    }
}
