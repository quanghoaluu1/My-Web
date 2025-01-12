using System.Text.RegularExpressions;

namespace WebApplication1.Services;

public partial class ValidationService
{
    [GeneratedRegex(@"^(?=.*[A-Z])(?=.*\d)[A-Za-z\d]{1,20}$")]
    private partial Regex MyRegex();
    
    public int CheckValidPassword(string password, string confirmPassword)
    {
        if (string.IsNullOrEmpty(password) || password.Length < 1 ||
            password.Length > 20)
        {
            return 1;
        }

        if (!MyRegex().IsMatch(password))
        {
            return 2;
        }

        if (password != confirmPassword)
        {
            return 3;
        }
        return 0;
    }
}