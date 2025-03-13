using Microsoft.EntityFrameworkCore;
using TaskManagementApi.Interfaces;
using TaskManagementApi.Models;

namespace TaskManagementApi.Repositories
{
    public class LabelRepository : IGenericRepository<Label>
    {
        private readonly TaskContext _context;
        public LabelRepository(TaskContext context) {
            _context = context;
        }
        public async Task<Label> AddAsync(Label entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await _context.Labels.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<IEnumerable<Label>> GetAllAsync()
        {
            return await _context.Labels.ToListAsync();
        }

        public async Task<Label?> GetByIdAsync(int id)
        {
            return await _context.Labels.FindAsync(id);
        }

        public async Task DeleteAsync(int id)
        {
            var Label = await _context.Labels.FindAsync(id);
            if (Label != null)
            {
                _context.Labels.Remove(Label);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Label not found.");
            }
        }

        public async Task UpdateAsync(int id, Label entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Label cannot be null");
            }

            var Label = await _context.Labels.FindAsync(id);
            if (Label == null)
            {
                throw new KeyNotFoundException("Label not found.");
            }

            _context.Entry(Label).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }

    }
}
