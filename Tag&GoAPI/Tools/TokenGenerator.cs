using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tag_Go.DAL.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Tag_GoAPI.Tools
{
    public class TokenGenerator
    {
    #nullable disable
        private readonly IConfiguration _configuration;

        public TokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(NUser nu)
        {
            //Génération de la clé de signature du token

            string secretKey = _configuration["Jwt:Key"];
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(secretKey));
            SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha512);

            // Définition du rôle
            string role = nu.Role_Id switch
            {
                "1" => "admin",
                "2" => "modo",
                _ => "user"
            };

            //Création du payload (donnée contenues dans le token)

            Claim[] userInfo = new[]
             {
                new Claim(ClaimTypes.Role, role),
                new Claim(ClaimTypes.Sid, nu.Role_Id.ToString()),
                new Claim(ClaimTypes.Email, nu.Email)
             };

            JwtSecurityToken jwt = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Issuer"],
            claims: userInfo,
            signingCredentials: credentials,
            expires: DateTime.Now.AddDays(1)
            );

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            return handler.WriteToken(jwt);
        }
    }
}
