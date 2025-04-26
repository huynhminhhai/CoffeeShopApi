using System.ComponentModel.DataAnnotations;

namespace CoffeeShopApi.Dto.Customer
{
    public class UpdateCustomerRequestDto
    {
        [Required(ErrorMessage = "FullName is required")]
        [MaxLength(50, ErrorMessage = "FullName must be at most 50 characters")]
        public string FullName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Point is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Point must be a non-negative integer")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Point must be a non-negative integer")]
        public int Point { get; set; }
    }
}