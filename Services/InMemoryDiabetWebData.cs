using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DiabetWeb.Data;
using DiabetWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace DiabetWeb.Services
{
    public class InMemoryDiabetWebData : IDiabetWebData
    {
		DiabetWebMemoryContext _context;

		public InMemoryDiabetWebData(DiabetWebMemoryContext context)
		{
			_context = context;
			LoadFromFile();
		}

		public FoodItem AddFoodItem(FoodItem fooditem)
		{
			fooditem.Id = Enumerable.Range(1, int.MaxValue).Except(_context.FoodItems.Select(fi => fi.Id)).FirstOrDefault();
			_context.FoodItems.Add(fooditem);
			_context.SaveChanges(ModelUtils.SaveModes.Foods);
			return fooditem;
		}

		public FoodItem GetFoodItem(int id)
		{
			return _context.FoodItems.FirstOrDefault(r => r.Id == id);
		}

		public IEnumerable<FoodItem> GetAllFoodItems()
		{
			return _context.FoodItems.OrderBy(r => r.Name);
		}

		public FoodItem UpdateFoodItem(FoodItem foodItem)
		{
			_context.SaveChanges(ModelUtils.SaveModes.Foods);
			return foodItem;
		}

		public FoodItem DeleteFoodItem(FoodItem fooditem)
		{
			_context.MealItems.RemoveAll(mi => fooditem.Id == mi.FoodItem.Id);
			_context.SaveChanges(ModelUtils.SaveModes.Meals);
			_context.FoodItems.RemoveAll(fi => fi.Id == fooditem.Id);
			_context.SaveChanges(ModelUtils.SaveModes.Foods);
			return fooditem;
		}

		public FoodItem[] DeleteFoodItems(FoodItem[] fooditems)
		{
			//untested
			_context.MealItems.RemoveAll(mi => fooditems.Contains(mi.FoodItem));
			_context.SaveChanges(ModelUtils.SaveModes.Meals);
			_context.FoodItems.RemoveAll(fi => fooditems.Contains(fi));
			_context.SaveChanges(ModelUtils.SaveModes.Foods);
			return fooditems;
		}

		public FoodItem[] UpdateFoodItems(FoodItem[] Fooditems)
		{
			_context.SaveChanges(ModelUtils.SaveModes.Foods);
			return Fooditems;
		}





		public IEnumerable<FoodItem> GetSelectItems(int[] ids)
		{
			if ((ids != null) && (ids.Length > 0))
			{
				return _context.FoodItems.Where<FoodItem>(i => ids.Contains(i.Id));
			}
			return _context.FoodItems.OrderBy(r => r.Name);
		}




		public MemberItem[] GetAllMemberItems()
		{
			return _context.MemberItems.OrderBy(m => m.MemberLogin).ToArray<MemberItem>();
		}

		public MemberItem AddMemberItem(MemberItem memberitem)
		{
			memberitem.Id = Enumerable.Range(1, int.MaxValue).Except(_context.MemberItems.Select(mi => mi.Id)).FirstOrDefault();
			_context.MemberItems.Add(memberitem);
			_context.SaveChanges(ModelUtils.SaveModes.Members);
			return memberitem;
		}


		public MemberItem UpdateMemberItem(MemberItem memberitem)
		{
			_context.SaveChanges(ModelUtils.SaveModes.Members);
			return memberitem;
		}

		public MemberItem GetMemberItem(string login)
		{
			return _context.MemberItems.FirstOrDefault(m => m.MemberLogin.Equals(login));
		}




		public MealItem[] GetAllMealItems()
		{
			var result = _context.MealItems.OrderBy(m => m.Id).ToArray<MealItem>();
			if (result.Count() == 0)
			{
				//making sure the collection empty, may be deleted later
				_context.MealItems.Clear();
			}
			return _context.MealItems.OrderBy(m => m.Id).ToArray<MealItem>();
		}

		public MealItem[] AddMealItems(MealItem[] mealitems)
		{
			var maxMealId = (_context.MealItems.Count == 0) ? 1 : _context.MealItems.Max(mi => mi.Id) + 1;
			foreach (var meal in mealitems)
			{
				var found = _context.MealItems.Find((m) => m.FoodItem.Id == meal.FoodItem.Id);
				meal.Id = (found == null) ? maxMealId++ : 0;
			}
			//remove duplicates
			_context.MealItems.AddRange(mealitems.Where((m) => m.Id > 0).ToArray<MealItem>());
			_context.SaveChanges(ModelUtils.SaveModes.Meals);
			return mealitems;
		}

		public MealItem[] UpdateMealItems(MealItem[] mealitems)
		{
			_context.SaveChanges();
			return mealitems;
		}

		public MealItem DeleteMealItem(MealItem mealitem)
		{
			_context.MealItems.Remove(mealitem);
			_context.SaveChanges(ModelUtils.SaveModes.Meals);
			return mealitem;
		}

		public MealItem[] DeleteMealItems(MealItem[] mealitems)
		{
			_context.MealItems.RemoveAll(meal => mealitems.Contains(meal));
			_context.SaveChanges(ModelUtils.SaveModes.Meals);
			return mealitems;
		}

		public MealItem[] GetMealItems(string login)
		{
			var meals = _context.MealItems.Where(m => m.MemberItem.MemberLogin.Equals(login)).ToArray<MealItem>();
			return meals;
		}

		public MealItem DeleteMealItem(int mealitemid)
		{
			var meal = _context.MealItems.Where(mi => mi.Id == mealitemid).ToArray<MealItem>();
			DeleteMealItems(meal);
			return meal[0];
		}

		public void SaveToFile(string filePath)
		{
			_context.SaveToFile(filePath);
		}

		public void LoadFromFile()
		{
			_context.LoadFromFile(ModelUtils.PATH);
		}

		public void LoadFromFile(string filePath)
		{
			_context.LoadFromFile(filePath);
		}
	}
}
