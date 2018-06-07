using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiabetWeb.Models;
using DiabetWeb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DiabetWeb.Pages.FoodItems
{
    [Authorize]
    public class UpdateModel : PageModel
    {
        private IDiabetWebData _diabetWebData;
		
		[BindProperty]
        public FoodItem FoodItem { get; set; }

        public UpdateModel(IDiabetWebData diabetWebData)
        {
            _diabetWebData = diabetWebData;
			//_diabetWebData.LoadFromFile(ModelUtils.PATH);
		}

        public IActionResult OnGet(int id)
        {
            FoodItem = _diabetWebData.GetFoodItem(id);
            if (FoodItem == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return Page();
        }

        public IActionResult OnPost(string submit)
        {
            if (ModelState.IsValid)
            {
                if (Request.Form.ContainsKey("update"))
                {
					var fi = _diabetWebData.GetFoodItem(FoodItem.Id);
					fi.Id = FoodItem.Id;
					fi.Name = FoodItem.Name;
					fi.Description = FoodItem.Description;
					fi.Protein = FoodItem.Protein;
					fi.Fat = FoodItem.Fat;
					fi.Carbohydrates = FoodItem.Carbohydrates;
					fi.GlycemicIndex = FoodItem.GlycemicIndex;
					fi.Attribute = FoodItem.Attribute;
					fi.Category = FoodItem.Category;
					fi.Favorites = FoodItem.Favorites;
					DiabetCalcService.CalcEnergy(fi);
                    _diabetWebData.UpdateFoodItem(FoodItem);
                    return RedirectToAction("FoodItemDetails", "Home", new { id = FoodItem.Id });
                }
                else if (Request.Form.ContainsKey("delete"))
                {
                    _diabetWebData.DeleteFoodItem(FoodItem);
                    return RedirectToAction("FoodItems", "Home");
                }
            }
            return Page();

        }

    }
}
