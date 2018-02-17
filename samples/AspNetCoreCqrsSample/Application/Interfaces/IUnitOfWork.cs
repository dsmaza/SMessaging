using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AspNetCoreCqrsSample.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<T> Find<T>(params object[] keys) where T : class;

        Task<IEnumerable<T>> GetAll<T>() where T : class;

        Task<T> Get<T>(Expression<Func<T, bool>> predicate) where T : class;

        Task<T> Get<T, TProperty>(Expression<Func<T, bool>> predicate, Expression<Func<T, TProperty>> includePath) where T : class;

        Task<bool> Exists<T>(Expression<Func<T, bool>> predicate) where T : class;

        Task Add<T>(T entity) where T : class;

        Task Update<T>(T entity) where T : class;

        Task Save();
    }
}
