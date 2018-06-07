using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiabetWeb.Models;
using DiabetWeb.Services;
using DiabetWeb.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DiabetWeb.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private IDiabetWebData _diabetWebData;
		private IGreeter _greeter;

        public HomeController(IDiabetWebData data, IGreeter greeter)
        {
            _diabetWebData = data;
			_greeter = greeter;

			//save to file
			//_diabetWebData.SaveToFile(ModelUtils.PATH);

			//loadfromfile
			//_diabetWebData.LoadFromFile(ModelUtils.PATH);
		}


		public IActionResult FoodItems()
        {
            var model = new HomeIndexViewModel();
            model.FoodItems = _diabetWebData.GetAllFoodItems();
            model.CurrentGreeting = _greeter.GetMessageOfTheDay();
            return View(model);
        }

        public IActionResult FoodItemDetails(int id)
        {
            var model = _diabetWebData.GetFoodItem(id);
            if (model == null)
            {
                return RedirectToAction(nameof(FoodItems));
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult AddFoodItem(FoodItem model)
        {
            return View(model);
        }

        [HttpGet]
        public IActionResult AddMealAsFood(MealItemViewModel[] model)
        {
            var member = DiabetCalcService.EnsureMemberExists(_diabetWebData, User.Identity.Name);
            var meals = _diabetWebData.GetMealItems(User.Identity.Name);
            var item = new FoodItem();
            var tw = (double)0;
            if ((meals != null) && (meals.Length > 0))
            {
                for (int i = 0; i < meals.Length; i++)
                {
                    tw += (double)meals[i].Weight;
                }
                if (tw > 0)
                {
                    for (int i = 0; i < meals.Length; i++)
                    {
                        item.Protein += meals[i].FoodItem.Protein * (double)meals[i].Weight / tw;
                        item.Fat += meals[i].FoodItem.Fat * (double)meals[i].Weight / tw;
                        item.Carbohydrates += meals[i].FoodItem.Carbohydrates * (double)meals[i].Weight / tw;
                    }
                }
                item.Protein = Math.Round(item.Protein, 2);
                item.Fat = Math.Round(item.Fat, 2);
                item.Carbohydrates = Math.Round(item.Carbohydrates, 2);
                DiabetCalcService.CalcEnergy(item);
            }
            return RedirectToAction(nameof(AddFoodItem), item);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult AddFoodItem(FoodItemEditModel model)
        {
            if (ModelState.IsValid)
            {
                var foodItem = new FoodItem();
                foodItem.Name = model.Name;
                foodItem.Description = model.Description;
                foodItem.Protein = model.Protein;
                foodItem.Fat = model.Fat;
                foodItem.Carbohydrates = model.Carbohydrates;
                foodItem.GlycemicIndex = model.GlycemicIndex;
                foodItem.Attribute = model.Attribute;
                foodItem.Category = model.Category;
				foodItem.Favorites = model.Favorites;
				DiabetCalcService.CalcEnergy(foodItem);
                foodItem = _diabetWebData.AddFoodItem(foodItem);

                return RedirectToAction(nameof(FoodItemDetails), new { id = foodItem.Id });
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult Update2FoodItem(int id)
        {
            var model = _diabetWebData.GetFoodItem(id);
            if (model == null)
            {
                return RedirectToAction(nameof(Update2FoodItem), model);
            }
            return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Update2FoodItem(FoodItemEditModel model)
        {
            if (ModelState.IsValid)
            {
				var fi = _diabetWebData.GetFoodItem(model.Id);
                fi.Id = model.Id;
                fi.Name = model.Name;
                fi.Description = model.Description;
                fi.Protein = model.Protein;
                fi.Fat = model.Fat;
                fi.Carbohydrates = model.Carbohydrates;
                fi.GlycemicIndex = model.GlycemicIndex;
                fi.Attribute = model.Attribute;
                fi.Category = model.Category;
                DiabetCalcService.CalcEnergy(fi);
				if (Request.Form.ContainsKey("update"))
				{
					fi = _diabetWebData.UpdateFoodItem(fi);
					return RedirectToAction(nameof(FoodItemDetails), new { id = fi.Id });
				}
				else if (Request.Form.ContainsKey("delete"))
				{
					_diabetWebData.DeleteFoodItem(fi);
					return RedirectToAction(nameof(FoodItems));
				}
				return View();
			}
            else
            {
                return View();
            }
        }


        [HttpGet]
        public IActionResult FoodSelect()
        {
            var foods = _diabetWebData.GetSelectItems(null);
            var model = new HomeSelectViewModel { FoodItems = foods, GetFavorite = 1 };
            return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult FoodSelect(HomeSelectViewModel model, string submit)
        {
            if (ModelState.IsValid)
            {
                var pm = DiabetCalcService.EnsureMemberExists(_diabetWebData, User.Identity.Name);
                if ((model.SelectedItemIds == null) || (model.SelectedItemIds.Length == 0))
                {
                    return RedirectToAction(nameof(FoodSelect));
                }


                /// Clear Add
                /// Update and Calculate
                /// Set Favorites
                if ("Set Favorites".Equals(submit))
                {
                    //set favorites on the selected list
                    var fooditems = _diabetWebData.GetSelectItems(model.SelectedItemIds.ToArray<int>());
                    var updateditems = fooditems.Select(fi => { fi.Favorites = model.SetFavorite; return fi; }).ToArray<FoodItem>();
                    _diabetWebData.UpdateFoodItems(updateditems);
                    return RedirectToAction(nameof(FoodSelect));
                }
                else
                {
                    //if clear add - remove existing
                    if ("Remove/Add and Calc".Equals(submit))
                    {
                        var temp = _diabetWebData.GetMealItems(User.Identity.Name);
                        if ((temp != null) && (temp.Length > 0))
                        {
                            _diabetWebData.DeleteMealItems(temp);
                        }
                    }

                    // update meal by adding more items
                    var fooditems = _diabetWebData.GetSelectItems(model.SelectedItemIds.ToArray<int>());
                    var meals = new List<MealItem>();
                    foreach (var fooditem in fooditems)
                    {
                        var mealitem = new MealItem { MemberItem = pm, FoodItem = fooditem, Weight = 0, DosePart = 0 };
                        meals.Add(mealitem);
                    }
                    _diabetWebData.AddMealItems(meals.ToArray<MealItem>());
					if ("Add and Stay".Equals(submit))
					{
						return RedirectToAction(nameof(FoodSelect));
					}
					else
					{
						return RedirectToAction(nameof(DiabetCalc));
					}
                }
            }
            else
            {
                return View();
            }
        }

        [AutoValidateAntiforgeryToken]
        public IActionResult MealItemDelete(int id)
        {
            _diabetWebData.DeleteMealItem(id);
            return RedirectToAction(nameof(DiabetCalc));
        }


        [HttpGet]
        public IActionResult DiabetCalc()
        {
            var member = DiabetCalcService.EnsureMemberExists(_diabetWebData, User.Identity.Name);
            var mealitems = _diabetWebData.GetMealItems(User.Identity.Name);
            if ((mealitems == null) || (mealitems.Length == 0))
            {
                return View();
            }
            var meals = new List<MealItemViewModel>();
            for (int i = 0; i < mealitems.Length; i++)
            {
                var mealitem = new MealItemViewModel { Id = mealitems[i].Id, MemberItem = member, FoodItem = mealitems[i].FoodItem, Weight = mealitems[i].Weight, DosePart = mealitems[i].DosePart };
                meals.Add(mealitem);
            }
            return View(meals.ToArray<MealItemViewModel>());
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult DiabetCalc(MealItemViewModel[] model)
        {
            if (ModelState.IsValid && (model != null) && (model.Length > 0))
            {
                var member = DiabetCalcService.EnsureMemberExists(_diabetWebData, User.Identity.Name);
                member.K1 = model[0].MemberItem.K1;
                member.K2 = model[0].MemberItem.K2;
                member.K3 = model[0].MemberItem.K3;
                member.F1 = model[0].MemberItem.F1;
                member.F2 = model[0].MemberItem.F2;
                member.F3 = model[0].MemberItem.F3;
                var meals = _diabetWebData.GetMealItems(User.Identity.Name);
                for (int i = 0; i < model.Length; i++)
                {
                    meals[i].Weight = model[i].Weight;
                }
                DiabetCalcService.CalcDose(member, meals);
				_diabetWebData.UpdateMealItems(meals.ToArray<MealItem>());
				member = _diabetWebData.UpdateMemberItem(member);
                return RedirectToAction(nameof(DiabetCalc));
            }
            return View(model);
        }










		[HttpGet]
		public IActionResult FoodSelectMod()
		{
			var foods = _diabetWebData.GetSelectItems(null);
			var model = new HomeSelectViewModel { FoodItems = foods, GetFavorite = 1 };
			return View(model);
		}

		[HttpPost]
		[AutoValidateAntiforgeryToken]
		public IActionResult FoodSelectMod(HomeSelectViewModel model, string submit)
		{
			if (ModelState.IsValid)
			{
				var pm = DiabetCalcService.EnsureMemberExists(_diabetWebData, User.Identity.Name);
				if ((model.SelectedItemIds == null) || (model.SelectedItemIds.Length == 0))
				{
					return RedirectToAction(nameof(FoodSelectMod));
				}


				/// Clear Add
				/// Update and Calculate
				/// Set Favorites
				if ("Set Favorites".Equals(submit))
				{
					//set favorites on the selected list
					var fooditems = _diabetWebData.GetSelectItems(model.SelectedItemIds.ToArray<int>());
					var updateditems = fooditems.Select(fi => { fi.Favorites = model.SetFavorite; return fi; }).ToArray<FoodItem>();
					_diabetWebData.UpdateFoodItems(updateditems);
					return RedirectToAction(nameof(FoodSelectMod));
				}
				else
				{
					//if clear add - remove existing
					if ("Clear Add".Equals(submit))
					{
						var temp = _diabetWebData.GetMealItems(User.Identity.Name);
						if ((temp != null) && (temp.Length > 0))
						{
							_diabetWebData.DeleteMealItems(temp);
						}
					}

					// update meal by adding more items
					var fooditems = _diabetWebData.GetSelectItems(model.SelectedItemIds.ToArray<int>());
					var meals = new List<MealItem>();
					foreach (var fooditem in fooditems)
					{
						var mealitem = new MealItem { MemberItem = pm, FoodItem = fooditem, Weight = 0, DosePart = 0 };
						meals.Add(mealitem);
					}
					_diabetWebData.AddMealItems(meals.ToArray<MealItem>());
					return RedirectToAction(nameof(DiabetCalc));
				}
			}
			else
			{
				return View();
			}
		}




	}
}
