using System.Collections.Generic;
using System.Security.Claims;
using DiabetWeb.Models;

namespace DiabetWeb.Services
{
    public interface IDiabetWebData
    {
        IEnumerable<FoodItem> GetAllFoodItems();
        IEnumerable<FoodItem> GetSelectItems(int[] ids);

        FoodItem GetFoodItem(int id);
        FoodItem AddFoodItem(FoodItem fooditem);
        FoodItem UpdateFoodItem(FoodItem fooditem);
        FoodItem[] UpdateFoodItems(FoodItem[] selecteditems);
        FoodItem DeleteFoodItem(FoodItem fooditem);
		FoodItem[] DeleteFoodItems(FoodItem[] fooditems);

		MemberItem[] GetAllMemberItems();
		MemberItem GetMemberItem(string login);
        MemberItem AddMemberItem(MemberItem memberitem);
        MemberItem UpdateMemberItem(MemberItem memberitem);

		MealItem[] GetAllMealItems();
		MealItem[] GetMealItems(string login);
        MealItem[] AddMealItems(MealItem[] mealitems);
        MealItem[] UpdateMealItems(MealItem[] mealitems);
        MealItem DeleteMealItem(MealItem mealitem);
        MealItem DeleteMealItem(int mealitemid);
        MealItem[] DeleteMealItems(MealItem[] mealitems);

		void SaveToFile(string filePath);
		void LoadFromFile(string filePath);
	}
}
