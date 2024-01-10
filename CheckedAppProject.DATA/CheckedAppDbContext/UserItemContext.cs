using CheckedAppProject.DATA.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CheckedAppProject.DATA.CheckedAppDbContext
{
    public class UserItemContext : IdentityDbContext<User>
    {
        public UserItemContext(DbContextOptions<UserItemContext> options) : base(options)
        {    
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemList> ItemLists { get; set; }
        public DbSet<UserItem> UserItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(eb =>
            {
                eb.Property(u => u.Email).IsRequired().HasMaxLength(200);
                eb.Property(u => u.UserName).IsRequired().HasMaxLength(200);
                eb.Property(u => u.UserSurname).IsRequired().HasMaxLength(200);
                eb.HasMany(w => w.ItemList)
                .WithOne(u => u.User)
                .HasForeignKey(x => x.Id);
            });

            modelBuilder.Entity<ItemList>()
                 .HasMany(e => e.Items)
                 .WithMany(e => e.ItemLists)
                 .UsingEntity<UserItem>();

            modelBuilder.Entity<IdentityUserLogin<string>>().HasKey("LoginProvider", "ProviderKey");
            modelBuilder.Entity<IdentityUserRole<string>>().HasKey("UserId", "RoleId");
            modelBuilder.Entity<IdentityUserToken<string>>().HasKey("UserId", "LoginProvider", "Name");

        }
    }
}
