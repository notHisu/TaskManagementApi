using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementApi.Interfaces;
using TaskManagementApi.Models;

namespace TaskManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IGenericRepository<Category> _categoryRepository;

        public CategoryController(IGenericRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet(Name = "GetAllCategories")]
        public ActionResult GetAllCategories()
        {
            var categories = _categoryRepository.GetAll();
            return Ok(categories);
        }

        [HttpPost(Name = "AddCategory")]
        public ActionResult<Category> AddCategory(Category category)
        {
            _categoryRepository.Add(category);
            return CreatedAtAction("GetCategoryById", new { id = category.Id }, category);
        }
    }
}
