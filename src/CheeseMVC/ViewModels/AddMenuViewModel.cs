using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CheeseMVC.ViewModels
{
    public class AddMenuViewModel
    {
        [Required(ErrorMessage ="Please name your Menu")]
        [StringLength(30, ErrorMessage ="Please keep the menu name shorter than 30 characters")]
        [DataType(DataType.Text)]
        [Display(Name = "Menu Name")]
        public string Name { get; set; }
    }
}
