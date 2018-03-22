using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CheeseMVC.ViewModels
{
    public class AddCategoryViewModel
    {
        [Required(ErrorMessage ="Please provide a category name")]
        [StringLength(15, ErrorMessage ="Category name must be shorter than 15 characters")]
        [DataType(DataType.Text)]
        [Display(Name="Category Name")]
        public string Name { get; set; }
    }
}
