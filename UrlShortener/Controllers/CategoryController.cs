using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Data.Entities;
using UrlShortener.Data.Models.DTO;
using UrlShortener.Services;

namespace UrlShortener.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly CategoryServices _categoryServices;

        public CategoryController(CategoryServices categoryServices) 
        { 
            _categoryServices = categoryServices;
        }
        [HttpGet("categories")]
        public IActionResult GetAll()
        {
           // string userRole = User.Claims.First(claim => claim.Type.Contains("role")).Value;
            //if (userRole is not "Admin") return Forbid();
            return Ok(_categoryServices.GetAll());
        }
        [HttpGet("admin/category/{nameCategory}")]
        public IActionResult GetByName(string nameCategory)
        {

            Category? name = _categoryServices.GetByName(nameCategory);
            if (name is not null) return Ok(name);
            return NotFound();
        }

       
        [HttpPost("admin/category")]
        public IActionResult AddCategory([FromBody] CategoryDto category)
        {
            string userRole = User.Claims.First(claim => claim.Type.Contains("role")).Value;
            if (userRole is not "Admin") return Forbid();

            int categoryId = _categoryServices.Add(category);

            return CreatedAtAction(nameof(GetByName), new { nameCategory = category.NameCategory }, categoryId);
        }

        [HttpDelete("admin/category/{id}")]
        public IActionResult DeleteCategory(int id)
        {
            string userRole = User.Claims.First(claim => claim.Type.Contains("role")).Value;
            if (userRole is not "Admin") return Forbid();
            bool deleted = _categoryServices.Delete(id);

            if (deleted)
            {
                return NoContent();
            }

            return NotFound($"Category with ID '{id}' not found.");
        }

    }
}
