using T002.Core.Models;

namespace T002.Core.Interfaces
{
	public interface IRepository<T> where T : DbEntity
    {
        Task<T> GetByIdAsync(string id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
