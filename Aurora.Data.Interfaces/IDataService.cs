namespace Aurora.Data.Interfaces;

public interface IDataService<T, TKey>

{
    Task AddOrUpdateAsync(TKey id, T data);

    Task<List<T>> GetAllAsync();

    Task<T?> GetAsync(string TKey);
}