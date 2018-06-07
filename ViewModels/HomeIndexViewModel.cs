using System.Collections.Generic;
using DiabetWeb.Models;

namespace DiabetWeb.ViewModels
{
    public class HomeIndexViewModel
    {
        public string CurrentGreeting { get; set; }
        public IEnumerable<FoodItem> FoodItems { get; set; }
    }
}
