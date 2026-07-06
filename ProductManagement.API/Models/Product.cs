using System.ComponentModel.DataAnnotations;

namespace ProductManagement.API.Models
{
    /// <summary>
    /// Represents a product in the e-commerce system
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Gets or sets the unique identifier for the product
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the product name
        /// </summary>
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the product category
        /// </summary>
        [Required(ErrorMessage = "Category is required")]
        public string Category { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the product price (must be greater than 0)
        /// </summary>
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the available stock quantity (must be 0 or greater)
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "Stock must be greater than or equal to 0")]
        public int Stock { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the product was created
        /// </summary>
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the date and time when the product was last updated
        /// </summary>
        public DateTime? UpdatedDate { get; set; }
    }
}
