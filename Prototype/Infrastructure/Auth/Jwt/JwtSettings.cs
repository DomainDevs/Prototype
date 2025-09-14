using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Auth.JWT;

public class JwtSettings
{
    public string Secret { get; set; } = "clave-muy-secreta-con-32bytes123";
    public string Issuer { get; set; } = "MyModularIssuer";
    public string Audience { get; set; } = "MyModularAudience";
}
