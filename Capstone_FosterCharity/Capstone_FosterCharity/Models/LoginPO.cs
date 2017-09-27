namespace Capstone_FosterCharity.Models
{ 
    using System.ComponentModel.DataAnnotations;

    public class LoginPO
    {
        [Required] //Need to Login
        public string Username { get; set; }

        [Required] //Need to Login
        public string Password { get; set; }
    }
}