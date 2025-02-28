using Microsoft.EntityFrameworkCore;
using TaskManagementApi.Interfaces;
using TaskManagementApi.Models;

namespace TaskManagementApi.Repositories
{
    public class CategoryRepository : ICategoryRepository<Category>
    {
        private readonly TaskContext _context;

        public CategoryRepository(TaskContext context)
        {
            _context = context;
        }

        public async Task<Category> AddAsync(Category entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Category cannot be null");
            }

            await _context.Categories.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Category not found.");
            }
        }

        public async Task UpdateAsync(int id, Category entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Category cannot be null");
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                throw new KeyNotFoundException("Category not found.");
            }

            _context.Entry(category).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }
    }
}
