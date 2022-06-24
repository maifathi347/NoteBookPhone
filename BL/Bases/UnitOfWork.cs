using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Interfaces;
using BL.Repositories;
using Dll;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BL.Bases
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Common Properties
        private DbContext U_DbContext { get; set; }
        private UserManager<ApplicationUsersIdentity> _userManager;


        #endregion

        #region Constructors
        public UnitOfWork(ApplicationDBContext U_DbContext, UserManager<ApplicationUsersIdentity> userManager)
        {
            this._userManager = userManager;
            this.U_DbContext = U_DbContext;//


            // Avoid load navigation properties
            //EC_DbContext.Configuration.LazyLoadingEnabled = false;
        }
        #endregion

        #region Methods
        public int Commit()
        {
            return U_DbContext.SaveChanges();
        }

        public void Dispose()
        {
            U_DbContext.Dispose();
        }
        #endregion





        public ContactRepository Contact;//=> throw new NotImplementedException();
        public ContactRepository contact
        {
            get
            {
                if (Contact == null)
                    Contact = new ContactRepository(U_DbContext);
                return Contact;
            }
        }
        public AccountRepository Account;//=> throw new NotImplementedException();
        public AccountRepository account
        {
            get
            {
                if (Account == null)
                    Account = new AccountRepository(U_DbContext, _userManager);
                return Account;
            }
        }
    }
}
