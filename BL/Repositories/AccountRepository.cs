using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Bases;
using BL.Dto;
using Dll;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BL.Repositories
{
    public class AccountRepository : BaseRepository<ApplicationUsersIdentity>
    {
        private readonly UserManager<ApplicationUsersIdentity> manager;
      

        public AccountRepository(DbContext db, UserManager<ApplicationUsersIdentity> manager) : base(db)
        {
            this.manager = manager;
          

        }
        public async Task<ApplicationUsersIdentity> FindByName(string userName)
        {
            ApplicationUsersIdentity result = await manager.FindByNameAsync(userName);
            return result;
        }

        public async Task<IdentityResult> Register(ApplicationUsersIdentity user)
        {
            user.Id = Guid.NewGuid().ToString();
            IdentityResult result;
            result = await manager.CreateAsync(user, user.PasswordHash);

            return result;
        }
      
        public bool CheckAccountExistsByData(ApplicationUsersIdentity user)
        {
            return GetAny(std => std.UserName == user.UserName && std.Email == user.Email);
        }
    }
}
