using TaskManagementApi.Interfaces;
using TaskManagementApi.Models;

namespace TaskManagementApi.Repositories
{
    public class CategoryRepository : IGenericRepository<Category>
    {
        private readonly TaskContext _context;

        public CategoryRepository(TaskContext context)
        {
            _context = context;
        }

        public void Add(Category entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException("Category cannot be null");
            }

            _context.Categories.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id, int? secondId = null)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);

            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Category> GetAll()
        {
            return _context.Categories.ToList();
        }

        public Category? GetById(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                throw new NullReferenceException("Category not found.");
            }
            return category;
        }

        public void Update(Category entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException("Category cannot be null");
            }

            _context.Categories.Update(entity);
            _context.SaveChanges();
        }
    }
}
