using System.ComponentModel.DataAnnotations;

namespace CoffeeShopApi.Dto.Customer
{
    public class CreateCustomerRequestDto
    {
        [Required(ErrorMessage = "FullName is required")]
        [MaxLength(50, ErrorMessage = "FullName must be at most 50 characters")]
        public string FullName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Phone is required")]
        [RegularExpression(@"^(?:\+84|0)(?:\d{9}|\d{10})$", ErrorMessage = "Phone number must be a valid Vietnamese phone number (e.g., +84912345678 or 0912345678)")]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}