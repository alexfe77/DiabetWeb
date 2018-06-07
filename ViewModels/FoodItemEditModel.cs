using System.ComponentModel.DataAnnotations;
using DiabetWeb.Models;

namespace DiabetWeb.ViewModels
{
    public class FoodItemEditModel
    {
        public int Id { get; set; }

        [Display(Name = "Food Item Name")]
        [Required, MaxLength(80)]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [MaxLength(200)]
        public string Description { get; set; }

        [Display(Name = "Protein")]
        [Range(0, 1000)]
        public double Protein { get; set; }

        [Display(Name = "Fat")]
        [Range(0, 1000)]
        public double Fat { get; set; }

        [Display(Name = "Carbohydrates")]
        [Range(0, 1000)]
        public double Carbohydrates { get; set; }

        [Display(Name = "Glycemic Index")]
        [Range(0, 200)]
        public int GlycemicIndex { get; set; }

        [Display(Name = "Attribute")]
        public int? Attribute { get; set; }

        [Display(Name = "Category")]
        public FoodCategory Category { get; set; }

        [Display(Name = "Favorites")]
        public int? Favorites { get; set; }

        [Display(Name = "Energy kJ")]
        public double? EnergyKJ { get; set; }

        [Display(Name = "Energy kCal")]
        public double? EnergyKC { get; set; }
    }
}
