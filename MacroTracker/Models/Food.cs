using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MacroTracker.Models
{
    public class Food
    {
        public int FoodId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
        [Required]
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
    public class User
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        [JsonIgnore]
        public string FullName => FirstName + " " + LastName;
    }
}
