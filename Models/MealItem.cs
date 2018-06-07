using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DiabetWeb.Models
{
    public class MealItem
    {
        public int Id { get; set; }

        [Display(Name = "Food Item")]
        public FoodItem FoodItem { get; set; }

        [Display(Name = "Member Item")]
        public MemberItem MemberItem { get; set; }

        [Display(Name = "Weight", Description = "Weight in gramms")]
        [Range(0, 5000)]
        public int Weight { get; set; }

		[Display(Name = "Dose Part", Description = "Constributed Dose")]
		public double DosePart { get; set; }
	}
}
