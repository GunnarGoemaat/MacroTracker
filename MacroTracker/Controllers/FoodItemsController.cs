using MacroTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class FoodItemsController : Controller
{
    private readonly ApplicationDbContext _context;

    public FoodItemsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Example: Get a list of food items
    public async Task<IActionResult> Index()
    {
        if (_context.Foods != null)
        {
            return View(await _context.Foods.ToListAsync());
        }
        else
        {
            // Handle the case where _context.Foods is null
            return NotFound();
        }
    }
    // GET
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var foodItem = await _context.Foods.FindAsync(id);
        if (foodItem == null)
        {
            return NotFound();
        }

        return View("~/Views/FoodItems/AddEdit.cshtml", foodItem); // Specify the path to the view
    }

    // POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("FoodId,Name,Category,Fat,Carbs,Protein,Calories")] Food food)
    {
        if (id != food.FoodId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(food);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Foods.Any(e => e.FoodId == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("Index", "Home");

        }
        return View("AddEdit", food);
    }


    // GET
    public IActionResult Add()
    {
        return View("~/Views/FoodItems/AddEdit.cshtml", new Food()); // Pass a new Food object to the view
    }


    // POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(Food food)
    {
        if (ModelState.IsValid)
        {
            _context.Add(food);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");

        }
        return View(food);
    }

    // GET
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var foodItem = await _context.Foods
            .FirstOrDefaultAsync(m => m.FoodId == id);
        if (foodItem == null)
        {
            return NotFound();
        }

        return View(foodItem);
    }

    // POST
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var foodItem = await _context.Foods.FindAsync(id);
        _context.Foods.Remove(foodItem);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index", "Home");
    }

}


