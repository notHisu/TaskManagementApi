using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementApi.DTOs;
using TaskManagementApi.Interfaces;
using TaskManagementApi.Models;

namespace TaskManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository<CategoryResponseDto> _categoryRepository;

        public CategoryController(ICategoryRepository<CategoryResponseDto> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet(Name = "GetAllCategories")]
        public ActionResult GetAllCategories()
        {
            try
            {
                var categories = _categoryRepository.GetAll();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}", Name = "GetCategoryById")]
        public ActionResult GetCategoryById(int id) 
        {
            try
            {
                var category = _categoryRepository.GetById(id);
                if (category == null)
                {
                    return NotFound();
                }
                return Ok(category);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost(Name = "AddCategory")]
        public ActionResult<CategoryResponseDto> AddCategory(CategoryCreateDto category)
        {
            try
            {
                _categoryRepository.Add(category);
                return CreatedAtRoute("GetAllCategories", new { }, category);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
