using Domain.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProyectoAPI.Seguridad
{
    public class JwtConfigurator
    {
        //generico
        public static string GetToken(Usuario userInfo, IConfiguration config)
        {
            string SecretKey = config["Jwt:Secretkey"];
            string Issuer = config["Jwt:Issuer"];
            string Audience = config["Jwt:Audience"];

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);//firmar credenciales //recibe contra y algoritmo

            var claims = new[]
            {//3 tipos de reclamaciones: registradas, publicas y privadas
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.Email),//rec registrada (sub: pasando nomusuario)
                new Claim("idUsuario", userInfo.IdUsuario.ToString())//claim privada //pasamos idusuario //cantidad de reclamaciones que quieras ...
            };
            //generacion del token
            var token = new JwtSecurityToken(
                issuer: Issuer,
                audience: Audience,
                claims,
                expires: DateTime.Now.AddDays(30),//cuando expira (en 60 minutos) // cuando pase el tiempo dara no autorizado
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //Retorna el id a partir de los claims de un JWT
        //nos interesa por ahora el id, pero podriamos devolver todo lo que queramos
        public static Guid GetTokenUsuario(ClaimsIdentity identity)
        {
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                foreach (var claim in claims)
                {
                    if (claim.Type == "idUsuario")
                    {
                        return Guid.Parse(claim.Value);
                    }
                }
            }
            return Guid.Empty;//po si identity no es nulo, pero si llega hasta aqui es porque el token tiene valor
        }
        ////nos interesa por ahora el id, pero podriamos devolver todo lo que queramos
        //public static int GetTokenUsuario(ClaimsIdentity identity)
        //{
        //    if (identity != null)
        //    {
        //        IEnumerable<Claim> claims = identity.Claims;
        //        foreach (var claim in claims)
        //        {
        //            if (claim.Type == "idUsuario")
        //            {
        //                return int.Parse(claim.Value);
        //            }
        //        }
        //    }
        //    return 0;//po si identity no es nulo, pero si llega hasta aqui es porque el token tiene valor
        //}
    }
}
