using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DiabetWeb.Models
{
    public class MemberItem
    {
        public int Id { get; set; }

        [Display(Name = "Member Login")]
        [Required, MaxLength(200)]
        public string MemberLogin { get; set; }

        [Display(Name="Member Name")]
        public string Name { get; set; }

        [Display(Name = "K1")]
        [Range(0, 1000)]
        public double K1 { get; set; }

        [Display(Name = "K2")]
        [Range(0, 1000)]
        public double K2 { get; set; }

        [Display(Name = "XE")]
        [Range(0, 1000)]
        public double K3 { get; set; }

        [Display(Name = "MMOL Before")]
        [Range(0, 1000)]
        public double F1 { get; set; }

        [Display(Name = "MMOL After")]
        [Range(0, 1000)]
        public double F2 { get; set; }

        [Display(Name = "Insulin Xform")]
        [Range(0, 1000)]
        public double F3 { get; set; }

        [Display(Name = "Insulin dose")]
        public double Dose { get; set; }

        [Display(Name = "Energy kJ")]
        public double? EnergyKJ { get; set; }

        [Display(Name = "Energy kCal")]
        public double? EnergyKC { get; set; }

        [Display(Name = "Total Fat")]
        public double? TotalFat { get; set; }

        [Display(Name = "Total Protein")]
        public double? TotalProtein { get; set; }

        [Display(Name = "Total Carbohydrates")]
        public double? TotalCarbohydrates { get; set; }
    }
}
