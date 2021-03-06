using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Identity
{
    // Next, we need to adjust the AppIdentityDbContext to include the new AppRole and we also need to specify the Id type here as well. 
    //Since we are sticking with the defaults this will mean that we use a string as the Id for these Identity classes
    public class AppIdentityDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options) {}

        // This needs to be done to avoid Identity primary key errors () 163

        protected override void OnModelCreating(ModelBuilder builder) 
        {
            base.OnModelCreating(builder);
        }
    }
}