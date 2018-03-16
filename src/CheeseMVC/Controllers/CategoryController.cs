using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheeseMVC.Data;
using Microsoft.AspNetCore.Mvc;
using CheeseMVC.Models;
using CheeseMVC.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CheeseMVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CheeseDbContext cheeseContext;

        public CategoryController(CheeseDbContext dbContext)
        {
            cheeseContext = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Category> categories = cheeseContext.Categories.ToList();

            return View(categories);
        }

        [HttpGet]
        public IActionResult Add()
        {
            AddCategoryViewModel addCategoryViewModel = new AddCategoryViewModel();

            return View(addCategoryViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddCategoryViewModel addCategoryViewModel)
        {
            if(ModelState.IsValid)
            {
                Category newCategory = new Category
                {
                    Name = addCategoryViewModel.Name
                };

                cheeseContext.Categories.Add(newCategory);
                cheeseContext.SaveChanges();

                return Redirect("/Category");
            }

            return View(addCategoryViewModel);
        }
    }
}
