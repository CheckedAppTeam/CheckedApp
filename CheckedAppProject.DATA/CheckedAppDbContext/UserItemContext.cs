using CheckedAppProject.DATA.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CheckedAppProject.DATA.CheckedAppDbContext
{
    public class UserItemContext : IdentityDbContext
    {
        public UserItemContext(DbContextOptions<UserItemContext> options) : base(options)
        {    
        }
        public DbSet<UserAccount> UsersApp { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemList> ItemLists { get; set; }
        public DbSet<UserItem> UserItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserAccount>(eb =>
            {
                eb.Property(u => u.UserAccountName).IsRequired().HasMaxLength(200);
                eb.Property(u => u.UserSurname).IsRequired().HasMaxLength(200);
                eb.HasMany(w => w.ItemList)
                .WithOne(u => u.UserAccount)
                .HasForeignKey(x => x.AppUserId);
                eb.HasOne(u => u.AppUser)
                .WithOne()
                .HasForeignKey<UserAccount>(u => u.AppUserId)
                .IsRequired();
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
