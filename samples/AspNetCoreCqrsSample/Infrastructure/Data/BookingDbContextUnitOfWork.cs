using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AspNetCoreCqrsSample.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreCqrsSample.Infrastructure.Data
{
    public class BookingDbContextUnitOfWork : IUnitOfWork
    {
        private readonly BookingDbContext dbContext;

        public BookingDbContextUnitOfWork(BookingDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<T> Find<T>(params object[] keys) where T : class
        {
            return await dbContext.Set<T>().FindAsync(keys);
        }

        public async Task<IEnumerable<T>> GetAll<T>() where T : class
        {
            return await dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> Get<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            var entity = await dbContext
                .Set<T>()
                .AsNoTracking()
                .SingleOrDefaultAsync(predicate);
            return entity;
        }

        public async Task<T> Get<T, TProperty>(Expression<Func<T, bool>> predicate, Expression<Func<T, TProperty>> includePath) where T : class
        {
            var entity = await dbContext
                .Set<T>()
                .Include(includePath)
                .AsNoTracking()
                .SingleOrDefaultAsync(predicate);
            return entity;
        }

        public async Task<bool> Exists<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return await dbContext.Set<T>().AnyAsync(predicate);
        }

        public async Task Add<T>(T entity) where T : class
        {
            await dbContext.AddAsync(entity);
        }

        public Task Update<T>(T entity) where T : class
        {
            dbContext.Update(entity);
            return Task.CompletedTask;
        }

        public async Task Save()
        {
            await dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}
