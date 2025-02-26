using TaskManagementApi.DTOs;
using TaskManagementApi.Interfaces;
using TaskManagementApi.Models;

namespace TaskManagementApi.Repositories
{
    public class CategoryRepository : ICategoryRepository<CategoryResponseDto>
    {
        private readonly TaskContext _context;

        public CategoryRepository(TaskContext context)
        {
            _context = context;
        }

        private static CategoryResponseDto ToDto(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }
            return new CategoryResponseDto
            {
                Name = category.Name,
                Description = category.Description
            };
        }

        private static IEnumerable<CategoryResponseDto> ToDtos(IEnumerable<Category> categories)
        {
            return categories.Select(ToDto).ToList();
        }   

        public void Add(CategoryCreateDto entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException("Category cannot be null");
            }

            var category = new Category
            {
                Name = entity.Name,
                Description = entity.Description
            };

            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);

            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
        }

        public IEnumerable<CategoryResponseDto> GetAll()
        {
            var categories = _context.Categories.ToList();
            return ToDtos(categories);
        }

        public CategoryResponseDto? GetById(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                throw new InvalidOperationException("Category not found.");
            }
            return ToDto(category);
        }

        public void Update(int id, CategoryUpdateDto entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException("Category cannot be null");
            }

            var category = _context.Categories.FirstOrDefault(c => c.Id == id);

            if (category == null)
            {
                throw new InvalidOperationException("Category not found");
            }

            if(entity.Name != null)
            {
                category.Name = entity.Name;
                category.Description = entity.Description;
            }

            _context.Categories.Update(category);
            _context.SaveChanges();
        }
    }
}
