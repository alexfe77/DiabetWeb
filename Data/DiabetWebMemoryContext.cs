using DiabetWeb.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace DiabetWeb.Data
{
    public class DiabetWebMemoryContext : DbContext
    {
		public static object _lock = new object();

        public DiabetWebMemoryContext(DbContextOptions<DiabetWebMemoryContext> options) : base(options)
        {

        }

        public List<FoodItem> FoodItems { get; set; }

        public List<MemberItem> MemberItems { get; set; }

        public List<MealItem> MealItems { get; set; }

		public void SaveToFile(string filePath)
		{
			lock (_lock)
			{
				File.WriteAllText(Path.Combine(filePath, ModelUtils.FOODS), ModelUtils.GetXMLFromObject(FoodItems));
				File.WriteAllText(Path.Combine(filePath, ModelUtils.MEMBERS), ModelUtils.GetXMLFromObject(MemberItems));
				File.WriteAllText(Path.Combine(filePath, ModelUtils.MEALS), ModelUtils.GetXMLFromObject(MealItems));
			}
		}

		public void LoadFromFile(string filePath)
		{
			var foods = File.ReadAllText(Path.Combine(filePath, ModelUtils.FOODS));
			FoodItems = ModelUtils.GetObjectFromXML(foods, typeof(List<FoodItem>)) as List<FoodItem>;

			var members = File.ReadAllText(Path.Combine(filePath, ModelUtils.MEMBERS));
			MemberItems = ModelUtils.GetObjectFromXML(members, typeof(List<MemberItem>)) as List<MemberItem>;

			var meals = File.ReadAllText(Path.Combine(filePath, ModelUtils.MEALS));
			MealItems = ModelUtils.GetObjectFromXML(meals, typeof(List<MealItem>)) as List<MealItem>;
		}

		public override int SaveChanges()
		{
			SaveToFile(ModelUtils.PATH);
			return base.SaveChanges();
		}

		public int SaveChanges(ModelUtils.SaveModes aMode)
		{
			lock (_lock)
			{
				try
				{
					switch (aMode)
					{
						case ModelUtils.SaveModes.All:
							SaveToFile(ModelUtils.PATH);
							break;
						case ModelUtils.SaveModes.Foods:
							File.WriteAllText(Path.Combine(ModelUtils.PATH, ModelUtils.FOODS), ModelUtils.GetXMLFromObject(FoodItems));
							break;
						case ModelUtils.SaveModes.Members:
							File.WriteAllText(Path.Combine(ModelUtils.PATH, ModelUtils.MEMBERS), ModelUtils.GetXMLFromObject(MemberItems));
							break;
						case ModelUtils.SaveModes.Meals:
							File.WriteAllText(Path.Combine(ModelUtils.PATH, ModelUtils.MEALS), ModelUtils.GetXMLFromObject(MealItems));
							break;
					}
				}
				catch (IOException)
				{
					Thread.Sleep(500);
					SaveToFile(ModelUtils.PATH);
				}
				catch (Exception)
				{
				}
			}
			return base.SaveChanges();
		}

	}
}
