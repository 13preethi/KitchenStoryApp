using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace KitchenStoryApp.Models
{
    public class AdminModel
    {
        [Key]
        [Required(ErrorMessage = "Required Field")]
        [Display(Name = "Id")]
        public int AdminId { get; set; }

        
        [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$")]
        [Display(Name = "Mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Required Field")]
        [Display(Name = "Full Name")]
        public string AdminName { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [Display(Name = "Confirm Password")]
        public string CfmPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Enter New Password")]
        public string NewPassword { get; set; }
    }
}