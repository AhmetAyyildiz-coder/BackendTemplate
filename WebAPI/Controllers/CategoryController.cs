using Buisness.Abstract;
using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
   
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet(nameof(GetCategories))]
        public IActionResult GetCategories()
        {
            IDataResult<List<Category>> categories = null;
            try
            {
                var data = _categoryService.GetList();
                if (!data.Success)
                {
                    return BadRequest(data);
                }

                categories = data;
            }
            catch (Exception e)
            {
                return Unauthorized(e.Message);
            }

            return Ok(categories);
        }


        [HttpGet("{Id:int}")]
        [Authorize]
        public IActionResult GetCategory(int Id)
        {
            var data = _categoryService.GetById(Id);
            if (!data.Success)
            {
                return BadRequest("Hata Oluþtu");
            }

            return Ok(data);
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
