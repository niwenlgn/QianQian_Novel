using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QianQian_Novel.Helper
{
    /// <summary>
    /// JWT帮助
    /// </summary>
    public class JWTHelper
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// JWT帮助
        /// </summary>
        /// <param name="configuration"></param>
        public JWTHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// 获取JWT Token
        /// </summary>
        /// <param name="authClaims"></param>
        /// <returns></returns>
        public JwtSecurityToken TokenGenerator(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:Secret").Value));
            var token = new JwtSecurityToken(
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    issuer: _configuration.GetSection("JWT:Issuer").Value,
                    audience: _configuration.GetSection("JWT:Audience").Value,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return token;

        }
    }
}
