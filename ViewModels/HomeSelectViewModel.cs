using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DiabetWeb.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DiabetWeb.ViewModels
{
    public class HomeSelectViewModel
    {
        [Range(0, 10)]
        public int GetFavorite { get; set; }

        [Range(0, 10)]
        public int SetFavorite { get; set; }

        public string CurrentGreeting { get; set; }

        public IEnumerable<FoodItem> FoodItems { get; set; }

        public int[] SelectedItemIds { get; set; }
    }
}
