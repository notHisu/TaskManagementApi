using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskManagementApi.DTOs;
using TaskManagementApi.Interfaces;
using TaskManagementApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository<Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetAllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var categories = await _categoryRepository.GetAllAsync();
                var categoriesResponse = _mapper.Map<List<CategoryResponseDto>>(categories);
                return Ok(categoriesResponse);
            }
            catch
            {
                return StatusCode(500, new { message = "An error occurred while fetching categories." });
            }
        }

        [HttpGet("{id}", Name = "GetCategoryById")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            try
            {
                var category = await _categoryRepository.GetByIdAsync(id);
                if (category == null)
                {
                    return NotFound(new { message = "Category not found" });
                }

                var categoryResponse = _mapper.Map<CategoryResponseDto>(category);
                return Ok(categoryResponse);
            }
            catch
            {
                return StatusCode(500, new { message = "An error occurred while fetching the category." });
            }
        }

        [HttpPost(Name = "AddCategory")]
        public async Task<IActionResult> AddCategory([FromBody] CategoryCreateDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var category = _mapper.Map<Category>(createDto);
                var createdCategory = await _categoryRepository.AddAsync(category);
                var categoryResponse = _mapper.Map<CategoryResponseDto>(createdCategory);

                return CreatedAtRoute("GetCategoryById", new { id = createdCategory.Id }, categoryResponse);
            }
            catch
            {
                return StatusCode(500, new { message = "An error occurred while creating the category." });
            }
        }
    }
}
