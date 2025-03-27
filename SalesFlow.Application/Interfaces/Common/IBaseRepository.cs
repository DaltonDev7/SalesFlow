
using System.Linq.Expressions;

namespace SalesFlow.Application.Interfaces.Common
{
    public interface IBaseRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> filter = null, string includeProperties = "", int? take = null, int? skip = null);
        Task<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, string includeProperties = "");
        Task Insert(TEntity entity);
        Task InsertAndSave(TEntity entity);

        Task Update(TEntity entity);
        Task UpdateAndSave(TEntity entity);

        Task Delete(int Id);
        Task DeleteAndSave(int Id);

        Task SaveChangesAsync();
    }
}
