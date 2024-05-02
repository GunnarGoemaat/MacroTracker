using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MacroTracker.Models
{
    public class Food
    {
        public int FoodId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Category is required.")]
        [StringLength(50)]
        public string Category { get; set; } = string.Empty;
        [Range(0, 1000)]
        public decimal Fat { get; set; }
        [Range(0, 1000)]
        public decimal Protein { get; set; }
        [Range(0, 1000)]
        public decimal Carbs { get; set; }
        [Range(0, 10000)]
        public int Calories { get; set; }

    }
}
