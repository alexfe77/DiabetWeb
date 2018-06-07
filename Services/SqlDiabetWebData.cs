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
    public class SqlDiabetWebData : IDiabetWebData
    {
        DiabetWebDbContext _context;

        public SqlDiabetWebData(DiabetWebDbContext context)
        {
            _context = context;
        }

        public FoodItem AddFoodItem(FoodItem fooditem)
        {
            _context.FoodItems.Add(fooditem);
            _context.SaveChanges();
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

        public FoodItem UpdateFoodItem(FoodItem fooditem)
        {
            _context.Attach(fooditem).State = EntityState.Modified;
            _context.SaveChanges();
            return fooditem;
        }

        public FoodItem DeleteFoodItem(FoodItem fooditem)
        {
			//untested
			_context.MealItems.RemoveRange(_context.MealItems.Where((mi) => mi.FoodItem.Id == fooditem.Id));
			_context.FoodItems.Remove(fooditem);
            _context.SaveChanges();
            return fooditem;
        }

		public FoodItem[] DeleteFoodItems(FoodItem[] fooditems)
		{
			//untested
			_context.MealItems.RemoveRange(_context.MealItems.Where((mi) => fooditems.Contains(mi.FoodItem)));
			_context.FoodItems.RemoveRange(fooditems);
			_context.SaveChanges();
			return fooditems;
		}

		public FoodItem[] UpdateFoodItems(FoodItem[] Fooditems)
        {
            foreach (var fi in Fooditems)
            {
                _context.Attach(fi).State = EntityState.Modified;
            }
            _context.SaveChanges();
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
            _context.MemberItems.Add(memberitem);
            _context.SaveChanges();
            return memberitem;
        }


        public MemberItem UpdateMemberItem(MemberItem memberitem)
        {
            _context.Attach(memberitem).State = EntityState.Modified;
            _context.SaveChanges();
            return memberitem;
        }

        public MemberItem GetMemberItem(string login)
        {			
            return _context.MemberItems.FirstOrDefault(m => m.MemberLogin.Equals(login));
        }



		public MealItem[] GetAllMealItems()
		{
			return _context.MealItems.OrderBy(m => m.Id).ToArray<MealItem>();
		}

		public MealItem[] AddMealItems(MealItem[] mealitems)
        {
            _context.MealItems.AddRange(mealitems);
            _context.SaveChanges();
            return mealitems;
        }
        
        public MealItem[] UpdateMealItems(MealItem[] mealitems)
        {
            foreach (var mi in mealitems)
            {
                _context.Attach(mi).State = EntityState.Modified;
            }
            _context.SaveChanges();
            return mealitems;
        }

        public MealItem DeleteMealItem(MealItem mealitem)
        {
            _context.MealItems.Remove(mealitem);
            _context.SaveChanges();
            return mealitem;
        }

        public MealItem[] DeleteMealItems(MealItem[] mealitems)
        {
            _context.MealItems.RemoveRange(mealitems);
            _context.SaveChanges();
            return mealitems;
        }

        public MealItem[] GetMealItems(string login)
        {
            var meals = _context.MealItems.Where(m => m.MemberItem.MemberLogin.Equals(login)).Include(m => m.MemberItem).Include(fi => fi.FoodItem).ToArray<MealItem>();
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

		public void LoadFromFile(string filePath)
		{
			_context.LoadFromFile(filePath);
		}

	}
}
