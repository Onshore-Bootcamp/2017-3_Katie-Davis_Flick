namespace Capstone_FosterCharity.ViewModels
{
        using Capstone_FosterCharity.Models;
using System.Collections.Generic;

    public class UserVM
    {
        public UserVM() //Constructor
        {
            User = new UserPO(); //new instance
            UserList = new List<UserPO>(); //new instance of list
        }
        public UserPO User { get; set; }// PO's in user
        public List<UserPO> UserList { get; set; }
        public string ErrorMessage { get; set; }
    }
}