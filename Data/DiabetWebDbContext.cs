using DiabetWeb.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace DiabetWeb.Data
{
    public class DiabetWebDbContext : DbContext
    {
        public DiabetWebDbContext(DbContextOptions<DiabetWebDbContext> options) : base(options)
        {

        }

        public DbSet<FoodItem> FoodItems { get; set; }

        public DbSet<MemberItem> MemberItems { get; set; }

        public DbSet<MealItem> MealItems { get; set; }


		public void SaveToFile(string filePath)
		{
			File.WriteAllText(Path.Combine(filePath, ModelUtils.FOODS), ModelUtils.GetXMLFromObject(FoodItems));
			File.WriteAllText(Path.Combine(filePath, ModelUtils.MEMBERS), ModelUtils.GetXMLFromObject(MemberItems));
			File.WriteAllText(Path.Combine(filePath, ModelUtils.MEALS), ModelUtils.GetXMLFromObject(MealItems));
		}

		internal void LoadFromFile(string filePath)
		{
			throw new NotImplementedException();
		}
	}
}
