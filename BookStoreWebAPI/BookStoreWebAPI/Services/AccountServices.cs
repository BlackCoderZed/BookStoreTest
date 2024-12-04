using BookStoreDataAccess.DAO;
using BookStoreDataAccess.Models;
using BookStoreDataAccess.Models.Account;
using BookStoreWebAPI.Models.Account;
using BookStoreWebAPI.Models.Account.Response;
using BookStoreWebAPI.Utils;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookStoreWebAPI.Services
{
    public class AccountServices : BaseServices
    {
        /// <summary>
        /// Authenticate User
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        internal ResUserLogin Authentiate(UserLoginInfo userLogin, IConfiguration configuration)
        {
            ResUserLogin response = new ResUserLogin();

            try
            {
                ModelValidator.ValidateUserLoginInfo(userLogin);

                AccountLoginInfo dbLoginInfo = ModelConverter.CreateDbUserLoginInfo(userLogin);

                User user = AccountDao.Authenciate(dbLoginInfo);

                response.UserID = user.Id;
                response.UserDlpName = user.UserDlpName;
                response.AuthToken = CreateJWTToken(userLogin.Email, configuration);
                response.Result = CreateResult(Constants.ACK_RESULT);
            }
            catch (Exception ex)
            {
                response.Result = CreateResult(Constants.NACK_RESULT, ex.Message);
            }

            return response;
        }

        /// <summary>
        /// CreateJWTToken
        /// </summary>
        /// <param name="email"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        private string CreateJWTToken(string email, IConfiguration config)
        {

            // jwt
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"]));
            var cred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, email)
            };

            var token = new JwtSecurityToken(
                issuer: config["JWT:Issuer"],
                audience: config["JWT:Audience"],
                expires: DateTime.Now.AddMinutes(30),
                claims: claims,
                signingCredentials: cred
               );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
