using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Context;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

[ApiController]
[Route("product")]
public class ProductController(MyDBContext context): ControllerBase, IProductController
{
    [HttpGet("product/{id:int}")]
    public Product GetProductById([FromRoute]int id)
    {
        return context.Products.FirstOrDefault(p => p.Id == id);
    }

    [HttpGet("products")]
    public List<Product> GetProducts()
    {
        return context.Products.ToList();
    }
    
    [HttpPost("add-product")]
    [Authorize(Policy = "Manager")]
    public Product AddProduct([FromBody]Product product)
    {
        var newProduct = new Product()
        {
            Name = product.Name,
            Price = product.Price,
            Description = product.Description,
            Category = product.Category,
            Image = product.Image,
            IsDeleted = false
        };
        context.Products.Add(newProduct);
        context.SaveChanges();
        return newProduct;
    }
}