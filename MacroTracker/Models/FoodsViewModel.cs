using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace MacroTracker.Models
{
    public class FoodsViewModel
    {
        public List<Food> Foods { get; set; } = new List<Food>();
        public List<string> Category { get; set; } = new List<string> { "Breakfast", "Lunch", "Dinner", "Snack" };
        public Dictionary<string, bool> SelectedCategories { get; set; } = new Dictionary<string, bool>();
        public decimal TotalFat => Foods.Sum(food => food.Fat);
        public decimal TotalCarbs => Foods.Sum(food => food.Carbs);
        public decimal TotalProtein => Foods.Sum(food => food.Protein);
        public int TotalCalories => Foods.Sum(food => food.Calories);

    }

}
