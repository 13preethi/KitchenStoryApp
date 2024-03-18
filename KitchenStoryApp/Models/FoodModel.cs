using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace KitchenStoryApp.Models
{
    public class FoodModel
    {
        [Key]
        [Required(ErrorMessage = "Required Field")]
        [Display(Name = "Id")]
        public int FoodId { get; set; }
        [Required(ErrorMessage = "Required Field")]
        [Display(Name = "Food Name")]
        public string FoodName { get; set; }
        [Required(ErrorMessage = "Required Field")]
        [Display(Name = " Food Price")]
        public float FoodPrice { get; set; }
    }
}