using Buisness.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("getproducts")]
        public IActionResult GetProducts()
        {
            var data = _productService.GetList();
            if (data.Success)
            {
                return Ok(data.Data);
            }
            return BadRequest(data);
        }

        [HttpGet("getbycategory({categoryId:int})")]
        public IActionResult GetByCategory(int categoryId)
        {
            var result = _productService.GetListByCategory(categoryId);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result);
        }

        [HttpGet("getproduct")]
        public IActionResult GetById(int Id)
        {
            var data = _productService.GetById(Id);
            if (data.Success)
            {
                return Ok(data.Data);
            }

            return BadRequest(data);
        }
        [HttpPost("addproduct")]
        public IActionResult AddProduct([FromBody] Product product) // Ekleme: 'FromBody' kullanarak isteği gövdeden alıyoruz.
        {
            var result = _productService.Add(product);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut("updateproduct")]
        public IActionResult UpdateProduct([FromBody] Product product) // Ekleme: 'FromBody' kullanarak isteği gövdeden alıyoruz.
        {
            var result = _productService.Update(product);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpDelete("deleteproduct")]
        public IActionResult DeleteProduct(int Id)
        {
            var product = _productService.GetById(Id);
            if (!product.Success)
            {
                return BadRequest("Id Hatalı");
            }
            var result = _productService.Delete(product.Data);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        
    }
}
