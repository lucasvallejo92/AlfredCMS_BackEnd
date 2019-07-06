using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace AlfredCMS.Extensions
{
    public class TokenCreator
    {
        public static string CreateToken()
        {
            //security key
            string securityKey = "abcdefgabcdefgabcdefg";
            //symmetric security key
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

            //signing credentials
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

            //add claims
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Role, "ADMIN"));
            claims.Add(new Claim("Our_Custom_Claim", "Our custom value"));
            claims.Add(new Claim("Id", "110"));


            //create token
            var token = new JwtSecurityToken(
                issuer: "AlfredCMS",
                expires: DateTime.Now.AddHours(1),
                signingCredentials: signingCredentials
                , claims: claims
            );
        
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}