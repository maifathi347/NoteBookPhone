using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dll
{
    public class ApplicationUsersIdentity : IdentityUser
    {
         
    }
    public class ApplicationUserStore : UserStore<ApplicationUsersIdentity>
    {

        public ApplicationUserStore() : base(new ApplicationDBContext())
        {

        }
        public ApplicationUserStore(DbContext db) : base(db)
        {

        }
    }
    public class ApplicationDBContext : IdentityDbContext<ApplicationUsersIdentity>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
               .UseSqlServer("Data Source=.;Initial Catalog=PhoneBook;Integrated Security=True"
               , options => options.EnableRetryOnFailure());
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public ApplicationDBContext()
        {

        }
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {

        }


        public  DbSet<Contact> contacts { get; set; }

    }


    }