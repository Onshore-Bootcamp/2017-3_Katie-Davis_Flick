namespace Capstone_FosterCharity.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class UserPO
    {
        public long UserID { get; set; }

        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "Phone Number")]
        [Required]//have to have                        //only certain numbers
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        [DisplayFormat(DataFormatString = "{0:(###)###-####}")] //format of how phone number looks on view 
        public Int64 PhoneNumber { get; set; }

        [Display(Name = "House/Apt Number")]
        [Required]
        public string HouseAptNumber { get; set; }

        [Display(Name = "Street Name")]
        [Required]
        public string StreetName { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)] //Only 2 letters
        public string State { get; set; }

        [Required]
        public string Zip { get; set; }

        [Range(0, 3)]
        public Int16 Role { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }




    }
}