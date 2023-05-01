namespace Aurora.Data.Interfaces;

public interface IDataService<T, TKey>

{
    Task<T> AddAsync(T data);

    Task<IList<T>> GetAllAsync();

    Task<T?> GetAsync(string key);

    Task<bool> ExistsAsync(string key);
}