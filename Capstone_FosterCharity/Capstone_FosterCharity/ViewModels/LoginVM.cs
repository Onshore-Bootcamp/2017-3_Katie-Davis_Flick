namespace Capstone_FosterCharity.ViewModels
{
    using Capstone_FosterCharity.Models;
using System.Collections.Generic;

    public class LoginVM
    {
        public LoginVM() //Constructor
        {
            User = new LoginPO(); //new instance
            UserList = new List<LoginPO>(); //new instance of list
            ErrorMessage = ""; //no error message sent to file
        }

        public LoginPO User { get; set; } //PO's in User

        public List<LoginPO> UserList { get; set; }

        public string ErrorMessage { get; set; }
    }
}