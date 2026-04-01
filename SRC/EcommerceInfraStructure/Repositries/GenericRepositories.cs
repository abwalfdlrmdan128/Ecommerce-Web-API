using Ecommerce.Core.Intefaces;
using EcommerceInfraStructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceInfraStructure.Repositries
{
    public class GenericRepositories<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;

        public GenericRepositories(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T Entity)
        {
            await _context.Set<T>().AddAsync(Entity);
            await _context.SaveChangesAsync();
        }

        public Task<int> CountAsync()=>_context.Set<T>().CountAsync();

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()=>await _context.Set<T>().AsNoTracking().ToListAsync();
     
        public async Task<IReadOnlyList<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
           var query=_context.Set<T>().AsQueryable();
            foreach(var item in includes)
            {
                query=query.Include(item);
            }
            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            return entity;
        }

        public async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            var query = _context.Set<T>().AsQueryable();
            foreach (var item in includes)
            {
                query = query.Include(item);
            }
            var entity = await query.FirstOrDefaultAsync(x => EF.Property<int>(x, "Id") == id);
            return entity;
        }

        public async Task UpdateAsync(T Entity)
        {
           _context.Entry(Entity).State=EntityState.Modified;
            await _context.SaveChangesAsync();
        }

    }
}
