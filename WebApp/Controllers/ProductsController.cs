using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private static List<Product> products = new List<Product>
        {
        };

        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            try
            {

                if (!products.Any())
                {
                    return Ok(new
                    {
                        Message ="No Products Available",
                        Timestamp = DateTime.UtcNow
                    });
                }
                var response = new
                {
                    Results = products,
                    Message = "Product Fetched Successfully",
                    Timestamp = DateTime.UtcNow

                };
                return Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    Message = "An error occured while fetching the products",
                    ErrorDetails = e.Message,
                    Timestamp = DateTime.UtcNow
                });
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Product> Get(Guid id)
        {
            try {
                var product = products.FirstOrDefault(p => p.Id == id);
                if (product == null)
                {
                    return NotFound(new
                    {
                        Message = "Product not found with specific Id",
                        Timesstamp = DateTime.UtcNow
                    });
                }
                return Ok(new
                {
                    Result = product,
                    Message = "Product Fetched Successfully",
                    Timestamp = DateTime.UtcNow
                }
                    );
            }

            catch (FormatException)
            {
                return BadRequest(new { Message = "Product not found with specific Id" });
            }
            
        }

        [HttpPost]
        public ActionResult Post([FromBody] Product product)
        {
            try
            {
                product.Id = product.Id == Guid.Empty ? Guid.NewGuid() : product.Id;

                products.Add(product);
                return CreatedAtAction(nameof(Get), new { id = product.Id }, new
                {
                    Result = product,
                    Message = "Product Added Successsfully",
                    Timestamp = DateTime.UtcNow
                });
            }
            catch (FormatException)
            {
                return BadRequest(new { Message = "Invalid post format" });
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(Guid id, [FromBody] Product updateProduct)
        {
            try {
                var product = products.FirstOrDefault(p => p.Id == id);
                if (product == null)
                {
                    return NotFound(
                        new
                        {
                            Result = "Product not found with the given Id",
                            Timestamp = DateTime.UtcNow
                        });
                }
                product.ProductName = updateProduct.ProductName;
                product.Price = updateProduct.Price;

                return Ok(new
                {
                    Message = "Product Updated Successfully",
                    Result = "Success",
                    UpdatedProduct = new
                    {
                        product.Id,
                        product.ProductName,
                        product.Price,
                        Timestamp = DateTime.UtcNow
                    }
                });
            }
            catch(FormatException)
            {
                return BadRequest(new { Message = "Invalid put format. Please provide a valid GUID.",
                                        Timestamp = DateTime.UtcNow,
                });
            }
           
        }

        [HttpDelete("{id}")]

        public ActionResult Delete(Guid id)
        {
            try
            {
                var product = products.FirstOrDefault();
                if (product == null)
                {
                    return NotFound(new { Message = "Product not found with the given Id" });
                }
                products.Remove(product);
                return Ok(new { Message = "Product Deleted Successfully" });
            }
            catch(FormatException)
            {
                return BadRequest(new { Message = "Invalid Id format, Please enter valid Id format" });
            }

        }
    }
}
