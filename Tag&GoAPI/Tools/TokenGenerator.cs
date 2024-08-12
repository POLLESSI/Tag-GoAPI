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
        public static string secretKey = "µpiçaezjrkuyjfgk:ghmkjghmiugl:hjfvtFSDMOifnZAE MOVjkµ$)'éàipornjfd ù)'$piçh";

        public string GenerateToken(NUser nu)
        {
            //Génération de la clé de signature du token

            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(secretKey));
            SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha512);

            //Création du payload (donnée contenues dans le token)

            Claim[] userInfo = new[]
             {
                new Claim(ClaimTypes.Role,nu.Role_Id == "1" ? "admin" : nu.Role_Id == "2" ? "modo" : "user"),
                new Claim(ClaimTypes.Sid, nu.Role_Id.ToString()),
                new Claim(ClaimTypes.Email, nu.Email)
             };

            JwtSecurityToken jwt = new JwtSecurityToken(
            //_configuration["Jwt:Issuer"],
            //_configuration["Jwt:Issuer"],
            claims: userInfo,
            signingCredentials: credentials,
            expires: DateTime.Now.AddDays(1)
            );

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            return handler.WriteToken(jwt);
        }
    }
}
