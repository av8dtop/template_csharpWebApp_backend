using System;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using csharp_demo_backend.Models;

namespace csharp_demo_backend.Utils
{
    public static class TokenUtils
    {
        private const string SecretKey = "your_secret_key_here";

        public static string GenerateToken(User user)
        {
            // Create claims for the user
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            // Create symmetric security key using the secret key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));

            // Create signing credentials using the key
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Create token descriptor with claims, expiry date, and signing credentials
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = creds
            };

            // Create token handler
            var tokenHandler = new JwtSecurityTokenHandler();

            // Generate token using the token descriptor
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Return the generated token
            return tokenHandler.WriteToken(token);
        }

        public static bool ValidateToken(string token)
        {
            try
            {
                // Create token validation parameters with the secret key
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };

                // Create token handler
                var tokenHandler = new JwtSecurityTokenHandler();

                // Validate token and return true if valid
                tokenHandler.ValidateToken(token, validationParameters, out _);

                return true;
            }
            catch
            {
                // Return false if token validation fails
                return false;
            }
        }
    }
}
