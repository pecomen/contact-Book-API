using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ContactBook.Data
{
    public class ContactContext : IdentityDbContext<IdentityUser>
    {
        public ContactContext(DbContextOptions<ContactContext> options) : base(options)
        {

        }

        public DbSet<Contact> Contacts { get; set; }
       

    }
}
