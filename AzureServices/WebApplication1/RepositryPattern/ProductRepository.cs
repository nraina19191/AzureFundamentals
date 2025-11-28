using WebApplication1.Models;

namespace WebApplication1.RepositryPattern
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(HRMSContext context): base(context)
        {
            
        }
    }
}
