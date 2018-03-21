﻿using System.Collections.Generic;

namespace CheeseMVC.Models
{
    public class Cheese
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int ID { get; set; }

        public int CategoryID { get; set; }
        public Category Category { get; set; }

        public List<CheeseMenu> CheeseMenus { get; set; }

        public Cheese()
        {
            //default constructor for model binding
        }
    }
}
