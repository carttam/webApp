namespace webApp.Data.Repository
{
    public interface IGenericRepository<T> where T : class 
    {
        Task<T> FindByIDAsync(int? id);
        Task<List<T>> AllAsync();
        ValueTask<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<T>> AddAsync(T entity);
        void Remove(T entity);
        void Update(T entity);
    }
}
