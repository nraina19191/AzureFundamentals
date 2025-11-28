
using WebApplication1.Models;

namespace WebApplication1.RepositryPattern
{
    public class UnitOfWork : IUnitOfWork
    {
        public IProductRepository Products { get; }
        private readonly HRMSContext _context;

        public UnitOfWork(HRMSContext context, IProductRepository productRepository)
        {
            _context = context;
            this.Products = productRepository;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public Task<int> SaveAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
