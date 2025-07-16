using System.ComponentModel.DataAnnotations;

namespace EventManagementSystem.Models
{
    public class EventCategory
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        public string Description { get; set; }

        [Required]
        [Display(Name = "Price Per Hour (₹)")]
        [Range(0, double.MaxValue, ErrorMessage = "Enter a valid hourly price.")]
        public decimal PricePerHour { get; set; }

        [Required]
        [Display(Name = "Price Per Day (₹)")]
        [Range(0, double.MaxValue, ErrorMessage = "Enter a valid daily price.")]
        public decimal PricePerDay { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; } = true;

        [Required]
        [Display(Name = "Maximum Capacity")]
        [Range(1, int.MaxValue, ErrorMessage = "Capacity must be at least 1.")]
        public int MaxCapacity { get; set; }
    }
}
