using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using CheeseMVC.Models;
using System.Collections.Generic;
using CheeseMVC.ViewModels;
using CheeseMVC.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CheeseMVC.Controllers
{
    public class CheeseController : Controller
    {
        private CheeseDbContext cheeseContext;

        public CheeseController(CheeseDbContext dbContext)
        {
            cheeseContext = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            IList<Cheese> cheeses = cheeseContext.Cheeses.Include(c=> c.Category).ToList();

            return View(cheeses);
        }

        public IActionResult Add()
        {
            // Create new ViewModel
            AddCheeseViewModel addCheeseViewModel = new AddCheeseViewModel();
            // Populate Select Element with Categories
            addCheeseViewModel.ViewModelCategories = cheeseContext.Categories
                .Select(c => new SelectListItem() { Value = c.ID.ToString(), Text = c.Name }).ToList();

            return View(addCheeseViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddCheeseViewModel addCheeseViewModel)
        {
            if (ModelState.IsValid)
            {
                // Find category object
                Category newCheeseCategory = cheeseContext.Categories.Single(c => c.ID == addCheeseViewModel.CategoryID);

                // Add the new cheese to my existing cheeses
                Cheese newCheese = new Cheese
                {
                    Name = addCheeseViewModel.Name,
                    Description = addCheeseViewModel.Description,
                    CategoryID = newCheeseCategory.ID
                };

                cheeseContext.Cheeses.Add(newCheese);
                cheeseContext.SaveChanges();

                return Redirect("/Cheese");
            }

            return View(addCheeseViewModel);
        }

        public IActionResult Remove()
        {
            // TODO: Link to Remove View
            ViewBag.title = "Remove Cheeses";
            ViewBag.cheeses = cheeseContext.Cheeses.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Remove(int[] cheeseIds)
        {
            foreach (int cheeseId in cheeseIds)
            {
                Cheese theCheese = cheeseContext.Cheeses.Single(c => c.ID == cheeseId);
                cheeseContext.Cheeses.Remove(theCheese);
            }

            cheeseContext.SaveChanges();

            return Redirect("/");
        }

    }
}
