namespace WebApplication1.RepositryPattern
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }

        Task<int> SaveAsync();
    }
}
