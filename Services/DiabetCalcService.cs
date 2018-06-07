using DiabetWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DiabetWeb.Services
{
    public class DiabetCalcService
    {
        public static void CalcDose(MemberItem aMember, MealItem[] aMeals)
        {
            double KD = 0; // dose
            double tec = 0; // energy kCal
            double tej = 0; // energy kJ

            double tf = 0; // total fat
            double tp = 0; // total protein
            double tc = 0; // total carbohydrates

            double XE = aMember.K3;
            if ((aMeals != null) && (aMeals.Length > 0))
            {
                foreach(var item in aMeals)
                {
                    //food
                    if (item.Weight > 0)
                    {
                        double CY = item.Weight * item.FoodItem.Carbohydrates / 100;
                        double CP = item.Weight * item.FoodItem.Protein / 100;
                        double CF = item.Weight * item.FoodItem.Fat / 100;

                        tf += CF;
                        tp += CP;
                        tc += CY;

						var dosepart = CY / XE * aMember.K1
							+ CP * 4.1 / 100 * aMember.K2
							+ CF * 9.3 / 100 * aMember.K2;
						item.DosePart = Math.Round(dosepart, 2);
						KD += dosepart;

                        CalcEnergy(item.FoodItem);
                        if (item.FoodItem.EnergyKC == null) item.FoodItem.EnergyKC = 0;
                        if (item.FoodItem.EnergyKJ == null) item.FoodItem.EnergyKJ = 0;
                        tec += (double)item.FoodItem.EnergyKC * (double)item.Weight;
                        tej += (double)item.FoodItem.EnergyKJ * (double)item.Weight;
                    }
                }

                //adjustments
                if ((aMember.F1 > 0) && (aMember.F2 > 0) && (aMember.F3 > 0))
                {
                    KD += (aMember.F1 - aMember.F2) / aMember.F3;
                }
            }
            aMember.Dose = Math.Round(KD, 2);
            aMember.EnergyKC = Math.Round(tec, 2);
            aMember.EnergyKJ = Math.Round(tej, 2);
            aMember.TotalFat = Math.Round(tf, 2);
            aMember.TotalProtein = Math.Round(tp, 2);
            aMember.TotalCarbohydrates = Math.Round(tc, 2);
        }

        public static void CalcEnergy(FoodItem foodItem)
        {
            foodItem.EnergyKC = Math.Round( (9.29 * foodItem.Fat + 4.1 * foodItem.Protein + 4.1 * foodItem.Carbohydrates) / (double)100, 2);
            foodItem.EnergyKJ = Math.Round( (38.9 * foodItem.Fat + 17.2 * foodItem.Protein + 17.2 * foodItem.Carbohydrates) / (double)100, 2);
        }

        public static MemberItem EnsureMemberExists(IDiabetWebData _foodItemData, string login)
        {
            var member = _foodItemData.GetMemberItem(login);
            if (member == null)
            {
                member = new MemberItem
                {
                    Name = login.Substring(login.IndexOf("#") + 1, login.IndexOf("@") - login.IndexOf("#") - 1),
                    MemberLogin = login,
                };
                member = _foodItemData.AddMemberItem(member);
            }
            return member;
        }

    }
}
