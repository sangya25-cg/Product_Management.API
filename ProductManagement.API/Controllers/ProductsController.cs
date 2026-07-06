using Microsoft.AspNetCore.Mvc;
using ProductManagement.API.Models;
using ProductManagement.API.Services;

namespace ProductManagement.API.Controllers
{
    /// <summary>
    /// Manages product operations for the e-commerce system
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Retrieves all products
        /// </summary>
        /// <returns>A list of all products</returns>
        /// <response code="200">Returns the list of products</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        /// <summary>
        /// Retrieves a specific product by ID
        /// </summary>
        /// <param name="id">The product ID</param>
        /// <returns>The product with the specified ID</returns>
        /// <response code="200">Returns the product</response>
        /// <response code="404">If the product is not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound(new { message = $"Product with ID {id} not found" });
            }

            return Ok(product);
        }

        /// <summary>
        /// Creates a new product
        /// </summary>
        /// <param name="product">The product to create</param>
        /// <returns>The newly created product</returns>
        /// <response code="201">Returns the newly created product</response>
        /// <response code="400">If the product data is invalid</response>
        [HttpPost]
        [ProducesResponseType(typeof(Product), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdProduct = await _productService.CreateProductAsync(product);
            return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id }, createdProduct);
        }

        /// <summary>
        /// Updates an existing product
        /// </summary>
        /// <param name="id">The product ID</param>
        /// <param name="product">The updated product data</param>
        /// <returns>The updated product</returns>
        /// <response code="200">Returns the updated product</response>
        /// <response code="400">If the product data is invalid or ID mismatch</response>
        /// <response code="404">If the product is not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            if (id != product.Id)
            {
                return BadRequest(new { message = "Product ID mismatch" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedProduct = await _productService.UpdateProductAsync(id, product);
            if (updatedProduct == null)
            {
                return NotFound(new { message = $"Product with ID {id} not found" });
            }

            return Ok(updatedProduct);
        }

        /// <summary>
        /// Deletes a product
        /// </summary>
        /// <param name="id">The product ID to delete</param>
        /// <returns>No content</returns>
        /// <response code="204">Product successfully deleted</response>
        /// <response code="404">If the product is not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productService.DeleteProductAsync(id);
            if (!result)
            {
                return NotFound(new { message = $"Product with ID {id} not found" });
            }

            return NoContent();
        }

        /// <summary>
        /// Searches products by category
        /// </summary>
        /// <param name="category">The category name to search for</param>
        /// <returns>A list of products in the specified category</returns>
        /// <response code="200">Returns the list of products in the category</response>
        /// <response code="400">If the category is empty</response>
        /// <response code="404">If no products found in the category</response>
        [HttpGet("search/category/{category}")]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Product>>> SearchProductsByCategory(string category)
        {
            if (string.IsNullOrWhiteSpace(category))
            {
                return BadRequest(new { message = "Category cannot be empty" });
            }

            var products = await _productService.SearchProductsByCategoryAsync(category);

            if (!products.Any())
            {
                return NotFound(new { message = $"No products found in category '{category}'" });
            }

            return Ok(products);
        }
    }
}
