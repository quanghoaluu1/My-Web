using WebApplication1.Models;

namespace WebApplication1.Interfaces;

public interface IProductController
{
    public Product GetProductById(int id);
    public List<Product> GetProducts();
    public Product AddProduct(Product product);
    
}