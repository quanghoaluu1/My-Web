using WebApplication1.Models;

namespace WebApplication1.Interfaces;

public interface IUserController
{
    public List<User> GetUsers();
    public User? GetUserByEmail(string email);
}