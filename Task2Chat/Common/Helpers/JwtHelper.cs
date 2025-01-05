using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Task2Chat.Common.Helpers
{
    public class JwtHelper
    {
        public static string GenerateToken(string userName, string role)
        {
            var issuer = Environment.GetEnvironmentVariable("ISSUER");
            var audience = Environment.GetEnvironmentVariable("AUDIENCE");
            var secretKey = Environment.GetEnvironmentVariable("SECRET_KEY");
            var tokenExpiration = Environment.GetEnvironmentVariable("TOKEN_EXPIRATION");

            if (string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(audience) || string.IsNullOrEmpty(secretKey))
            {
                throw new ArgumentException("Issuer, Audience, or SecretKey is not provided.");
            }

            if (int.TryParse(tokenExpiration, out int expirationInSeconds))
            {
                var expirationDuration = TimeSpan.FromSeconds(expirationInSeconds);

                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.Role, role),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // Claim ID token (unique)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: issuer,
                    audience: audience,
                    claims: claims,
                    expires: DateTime.Now.Add(expirationDuration),
                    signingCredentials: credentials
                );

                var tokenHandler = new JwtSecurityTokenHandler();
                return tokenHandler.WriteToken(token);
            }
            else
            {
                throw new ArgumentException("Invalid TOKEN_EXPIRATION value.");
            }
        }

        public static string GetUserNameFromToken(string token)
        {
            try
            {
                var issuer = Environment.GetEnvironmentVariable("ISSUER");
                var audience = Environment.GetEnvironmentVariable("AUDIENCE");
                var secretKey = Environment.GetEnvironmentVariable("SECRET_KEY");

                if (string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(audience) || string.IsNullOrEmpty(secretKey))
                {
                    throw new ArgumentException("Issuer, Audience, or SecretKey is not provided.");
                }

                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                    ClockSkew = TimeSpan.Zero
                };

                var tokenHandler = new JwtSecurityTokenHandler();

                var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);

                var userNameClaim = principal?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                return userNameClaim;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Token validation failed: {ex.Message}");
                return string.Empty;
            }
        }
    }
}
