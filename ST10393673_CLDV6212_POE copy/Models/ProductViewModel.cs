using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace ST10393673_CLDV6212_POE.Models
{
    public class ProductViewModel
    {
        // The unique identifier for the product (used in Azure Table Storage)
        public string ProductId { get; set; }  // Added ProductId

        public string RowKey { get; set; }  // Used for Azure Table Storage, if applicable

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Product name must be between 3 and 100 characters.")]
        public string ProductName { get; set; }  // Adjusting to match controller expectations

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string ProductDescription { get; set; }  // Ensure this matches the controller

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
        public decimal ProductPrice { get; set; }  // Ensure this matches the controller

        public string ProductImageUrl { get; set; }  // Optional, for storing a URL to the product image

        [Required(ErrorMessage = "Please upload an image.")]
        public IFormFile ProductImage { get; set; }  // Keep this for handling image uploads
    }
}
