using CheeseMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CheeseMVC.ViewModels
{
    public class AddCheeseViewModel
    {
        [Required(ErrorMessage = "You must name your cheese")]
        [Display(Name = "Cheese Name")]
        [StringLength(15, ErrorMessage = "Please give a real cheese name that is no longer than 15 characters")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required(ErrorMessage = "You must give your cheese a description")]
        [StringLength(30, ErrorMessage = "Shorter, please. We don't need a novel")]
        [DataType(DataType.Text)]
        public string Description { get; set; }

        [Required(ErrorMessage = "You must provide a category")]
        [Display(Name = "Category")]
        public int CategoryID { get; set; }

        public List<SelectListItem> ViewModelCategories { get; set; }

        public AddCheeseViewModel()
        {
            // default constructor for model binding
        }

    }
}
