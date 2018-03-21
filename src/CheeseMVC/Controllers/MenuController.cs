using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheeseMVC.Data;
using Microsoft.AspNetCore.Mvc;
using CheeseMVC.Models;
using CheeseMVC.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CheeseMVC.Controllers
{
    public class MenuController : Controller
    {
        private CheeseDbContext cheeseContext;

        public MenuController(CheeseDbContext dbcontext)
        {
            cheeseContext = dbcontext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            IList<Menu> menus = cheeseContext.Menus.ToList();

            return View(menus);
        }

        public IActionResult ViewMenu(int id)
        {
            Menu menu = cheeseContext.Menus.Single(m => m.ID == id);

            IList<CheeseMenu> items = cheeseContext.CheeseMenus
                .Include(item => item.Cheese)
                .Where(cm => cm.MenuID == id)
                .ToList();

            ViewMenuViewModel viewMenuViewModel = new ViewMenuViewModel
            {
                Menu = menu,
                Items = items
            };

            return View(viewMenuViewModel);
        }

        public IActionResult Add()
        {
            AddMenuViewModel addMenuViewModel = new AddMenuViewModel();

            return View(addMenuViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddMenuViewModel addMenuViewModel)
        {
            if(ModelState.IsValid)
            {
                Menu newMenu = new Menu
                {
                    Name = addMenuViewModel.Name
                };

                cheeseContext.Menus.Add(newMenu);
                cheeseContext.SaveChanges();

                return Redirect(String.Format("/Menu/ViewMenu/{0}", newMenu.ID));
            };

            return View(addMenuViewModel);
        }

        public IActionResult AddItem(int id)
        {
            // Find Menu to Edit
            Menu menu = cheeseContext.Menus.Single(m => m.ID == id);

            //LINES 83-102: Kyla tried to remove existing cheeses in menu from the select list of ones to add. Got an "object not set to instance" error at line 87.
            //// All Cheeses
            //List<Cheese> allCheeses = cheeseContext.Cheeses.ToList();

            //// Find cheesemenu relationships to menu
            //List<CheeseMenu> cheeseMenusToIgnore = cheeseContext.CheeseMenus.Where(c => c.MenuID == id).ToList();

            //// Filter out cheeses already on menu
            //List<Cheese> cheeseNotOnMenu = new List<Cheese>();

            //foreach (Cheese c in allCheeses)
            //{
            //    if (c.CheeseMenus.Exists(cm=> cm.MenuID == menu.ID))
            //    {
            //        continue;
            //    }
            //    else
            //    {
            //        cheeseNotOnMenu.Add(c);
            //    }
            //}
            
            // Create new ViewModel with menu and select items
            AddMenuItemViewModel addMenuItemViewModel = new AddMenuItemViewModel
            {
                Menu = menu,
                Cheeses = cheeseContext.Cheeses.Select(c=> new SelectListItem() { Value=c.ID.ToString(), Text = c.Name }).ToList()
    
            };

            // Populate Select Element with Categories
            addMenuItemViewModel.Cheeses = cheeseContext.Cheeses
                .Select(c => new SelectListItem() { Value = c.ID.ToString(), Text = c.Name }).ToList();

            return View(addMenuItemViewModel);
        }

        [HttpPost]
        public IActionResult AddItem(AddMenuItemViewModel addMenuItemViewModel)
        {
            if(ModelState.IsValid)
            {
                IList<CheeseMenu> existingItems = cheeseContext.CheeseMenus
                    .Where(cm => cm.CheeseID == addMenuItemViewModel.CheeseID)
                    .Where(cm => cm.MenuID == addMenuItemViewModel.MenuID).ToList();

                if (existingItems.Count == 0)
                {
                    CheeseMenu newCheeseMenu = new CheeseMenu
                    {
                        MenuID = addMenuItemViewModel.MenuID,
                        CheeseID = addMenuItemViewModel.CheeseID
                    };
                     
                    cheeseContext.CheeseMenus.Add(newCheeseMenu);
                    cheeseContext.SaveChanges();

                    return Redirect(String.Format("/Menu/ViewMenu/{0}", addMenuItemViewModel.MenuID));
                }
            }

            return Redirect(String.Format("/Menu/ViewMenu/{0}", addMenuItemViewModel.MenuID));
        }
    }
}
