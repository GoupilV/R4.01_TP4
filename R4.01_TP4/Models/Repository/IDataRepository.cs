using Microsoft.AspNetCore.Mvc;

namespace R4._01_TP4.Models.Repository
{
    public interface IDataRepository<TEntity>
    {
        ActionResult <IEnumerable<TEntity>> GetAll ();
        Task<ActionResult<TEntity>> GetByIdAsync(int id);
        Task<ActionResult<TEntity>> GetByStringAsync(string str);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entityToUpdate, TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}
