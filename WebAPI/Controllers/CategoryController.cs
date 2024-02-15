using Buisness.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("getcategories")]
      
        public IActionResult GetCategories()
        {
            var data = _categoryService.GetList();
            if (data.Success)
            {
                return Ok(data.Data);
            }

            return BadRequest(data);
        }

        
        [HttpPost(template: "addcategory")]
        public IActionResult AddCategory([FromBody] Category category)
        {
            var result = _categoryService.Add(category);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
