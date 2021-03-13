namespace Suftnet.Cos.Web.Services
{
    using Common;
    using Core;
    using Cos.Services.Interface;
    using Microsoft.IdentityModel.Tokens;

    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    public class JwToken : IJwToken
    {
        public string Create(string username, string userId, string tenantId)
        {
            //Set issued at date
            var issuedAt = DateTime.UtcNow;

            //set the time when it expires
            var expires = DateTime.UtcNow.AddDays(30);
          
            var tokenHandler = new JwtSecurityTokenHandler();

            //create a identity and add claims to the user which we want to log in
            var claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.GroupSid, tenantId)
            });

            var Secret = SecurityTokenGenerator.SecretKey;
            var securityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(Secret));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            
            //create the jwt
            var token = tokenHandler.CreateJwtSecurityToken(
                    issuer: "http://jerur.com",
                    audience: "http://jerur.com",
                    subject: claimsIdentity,
                    notBefore: issuedAt,
                    expires: expires,
                    signingCredentials: signingCredentials);

            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

        public ClaimsPrincipal Principal(string token)
        {
            try
            {
                SecurityToken securityToken;
                var sec = SecurityTokenGenerator.SecretKey;

                var securityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(sec));

                var handler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters
                {
                    ValidAudience = "http://jerur.com",
                    ValidIssuer = "http://jerur.com",
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    LifetimeValidator = LifetimeValidator,
                    IssuerSigningKey = securityKey
                };

                var principal = handler.ValidateToken(token, validationParameters, out securityToken);
                return principal;

            }
            catch (SecurityTokenValidationException ex)
            {               
                GeneralConfiguration.Configuration.Logger.Log(ex.Message, EventLogSeverity.Error);
            }
            catch (Exception ex)
            {              
                GeneralConfiguration.Configuration.Logger.Log(ex.Message, EventLogSeverity.Error);
            }

            return null;
        }


        #region private function

        private bool LifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters)
        {
            if (expires != null)
            {
                if (DateTime.UtcNow < expires) return true;
            }
            return false;
        }

        #endregion
    }
}