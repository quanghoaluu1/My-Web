namespace WebApplication1.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public double Price { get; set; }
    public string Image { get; set; }
    public bool IsDeleted { get; set; }
}