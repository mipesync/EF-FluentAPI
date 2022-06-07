using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace EF_FluentAPI__Front_;

public class AuthOptions
{
    public static string ISSUER = "localhost:7084";
    public static string AUDIENCE = "localhost:7144";
    const string KEY = "375ff8a55aca72cf3a11e318d1592d2f0d3995ae";
    public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
}