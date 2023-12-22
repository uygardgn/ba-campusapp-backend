using BACampusApp.Dtos.TokenBlackList;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace BACampusApp.Business.Concretes
{
    public class JwtManager : IJwtService
    {
        private readonly JwtOptions _jwtOptions;
        private readonly UserManager<IdentityUser> _userManager;

        public JwtManager(IOptions<JwtOptions> options, UserManager<IdentityUser> userManager)
        {
            _jwtOptions = options.Value;
            _userManager = userManager;
            
        }

        /// <summary>
        /// Bu metot ile Jwt'de bulunan secret encode edilip, dijital imzayı oluşturmak için şifreleme algoritması oluşturulur ve verilen bir kullanıcı için JWT token'ı oluşturulmaktadır.
        /// </summary>
        /// <param name="user">Token'ı oluşturulacak IdentityUser nesnesi</param>
        /// <returns>JwtSecurityTokenHandler().WriteToken(token)</returns>
        public async Task<string> GenerateTokenAsync(IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var identityRoles = (await _userManager.GetRolesAsync(user)).ToList();
            identityRoles.ForEach(r => claims.Add(new Claim(ClaimTypes.Role, r)));

            var encodedKey = Encoding.UTF8.GetBytes(_jwtOptions.Secret); //Jwt'de bulunan Secret'ı encode ettik

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(encodedKey), SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(issuer: _jwtOptions.Issuer, audience: _jwtOptions.Audience, claims: claims, notBefore: DateTime.Now, expires: DateTime.Now.Add(_jwtOptions.ExpiredTime), signingCredentials: signingCredentials);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

      

    }
}
