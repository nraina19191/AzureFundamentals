using WebApplication1.Models;

namespace WebApplication1.RepositryPattern
{
    public class ProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddProduct(Product product) {
            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProducts() {
            var list = await _unitOfWork.Products.GetAllAsync();

            return list;
        }
    }
}
