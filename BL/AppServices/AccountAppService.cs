using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BL.Bases;
using BL.Dto;
using BL.Interfaces;
using Dll;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BL.AppServices
{
    public class AccountAppService : AppServiceBase
    {
        IConfiguration _configuration;

        public AccountAppService(IUnitOfWork theUnitOfWork, IConfiguration configuration,
             IMapper mapper) : base(theUnitOfWork, mapper)
        {
            this._configuration = configuration;

        }

        public async Task<ApplicationUsersIdentity> FindByName(string userName)
        {
            ApplicationUsersIdentity user = await TheUnitOfWork.account.FindByName(userName);

            if (user != null )
                return user;
            return null;
        }


        public async Task<IdentityResult> Register(RegisterViewModel user)
        {
            bool isExist = await checkUserNameExist(user.UserName);
            if (isExist)
                return IdentityResult.Failed(new IdentityError
                { Code = "error", Description = "user name already exist" });

            ApplicationUsersIdentity identityUser = Mapper.Map<RegisterViewModel, ApplicationUsersIdentity>(user);
            var result = await TheUnitOfWork.account.Register(identityUser);
            // create user cart and wishlist 
            if (result.Succeeded)
            {
                // CreateUserCartAndWishlist(identityUser.Id);
            }
            return result;
        }
      
        
        public async Task<bool> checkUserNameExist(string userName)
        {
            var user = await TheUnitOfWork.account.FindByName(userName);
            return user == null ? false : true;
        }
        
        public async Task<dynamic> CreateToken(ApplicationUsersIdentity user)
        {
          

            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
               

                   new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

           

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            };


        }

        public bool CheckAccountExistsByData(RegisterViewModel accountInfo)
        {
            ApplicationUsersIdentity std = Mapper.Map<ApplicationUsersIdentity>(accountInfo);
            if (std == null)
            {
                return false;
            }
            else
            {
                return TheUnitOfWork.account.CheckAccountExistsByData(std);
            }
        }

    }




}
