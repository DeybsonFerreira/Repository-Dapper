namespace repository_dapper.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> AddAsync(T product);
        Task<bool> UpdateAsync(T product);
        Task<bool> DeleteAsync(int id);

    }
}
