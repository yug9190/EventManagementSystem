using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EventManagementSystem.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirm Password is required.")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        [DataType(DataType.Password)]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please select a role.")]
        [DisplayName("Register As")]
        public string Role { get; set; } = string.Empty;

        // Optional: predefined roles for dropdown binding in view
        public List<string> AvailableRoles { get; set; } = new List<string> { "Organizer", "Attendee" };
    }
}
