using MacroTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace MacroTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            if (!_context.Foods.Any())
            {
                var foods = new List<Food>
                {
                    new Food { Name = "Apple", Category = "Snack", Fat = 0.3m, Protein = 0.5m, Carbs = 25m, Calories = 95 },
                    new Food { Name = "Chicken Breast", Category = "Dinner", Fat = 3.6m, Protein = 31m, Carbs = 0m, Calories = 165 }
                };

                _context.Foods.AddRange(foods);
                await _context.SaveChangesAsync();
            }

            var model = new FoodsViewModel
            {
                Foods = await _context.Foods.ToListAsync(),
                SelectedCategories = new Dictionary<string, bool>
                {
                    { "Breakfast", false },
                    { "Lunch", false },
                    { "Dinner", false },
                    { "Snack", false }
                }
            };

            return View(model);
        }

        [HttpPost] // had to get help with code below so leaving comments in to better understand it!
        public async Task<IActionResult> Index(FoodsViewModel model)
        {
            // Extract the selected categories from the request form
            var selectedCategories = model.SelectedCategories
                .Where(kvp => kvp.Value)
                .Select(kvp => kvp.Key)
                .ToList();

            // Filter the foods based on the selected categories
            var filteredFoods = await _context.Foods
                .Where(f => selectedCategories.Contains(f.Category))
                .ToListAsync();

            // Assign the filtered list of foods to the model
            model.Foods = filteredFoods;

            // Reassign the selected categories to ensure they are retained after postback
            foreach (var category in model.Category)
            {
                model.SelectedCategories[category] = Request.Form["SelectedCategories[" + category + "]"].FirstOrDefault() == "true";
            }

            // Pass the updated model back to the view
            return View(model);
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
