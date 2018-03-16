using Microsoft.AspNetCore.Mvc;
using CheeseMVC.Models;
using System.Collections.Generic;
using CheeseMVC.ViewModels;
using CheeseMVC.Data;
using System.Linq;

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
            List<Cheese> cheeses = cheeseContext.Cheeses.ToList();

            return View(cheeses);
        }

        public IActionResult Add()
        {
            AddCheeseViewModel addCheeseViewModel = new AddCheeseViewModel();
            return View(addCheeseViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddCheeseViewModel addCheeseViewModel)
        {
            if (ModelState.IsValid)
            {
                // Add the new cheese to my existing cheeses
                Cheese newCheese = new Cheese
                {
                    Name = addCheeseViewModel.Name,
                    Description = addCheeseViewModel.Description,
                    CategoryID = addCheeseViewModel.CategoryID
                };

                cheeseContext.Cheeses.Add(newCheese);
                cheeseContext.SaveChanges();

                return Redirect("/Cheese");
            }

            return View(addCheeseViewModel);
        }

        public IActionResult Remove()
        {
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
