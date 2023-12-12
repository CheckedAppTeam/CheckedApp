using CheckedAppProject.DATA.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CheckedAppProject.DATA.CheckedAppDbContext

{
    public class UserItemContext : DbContext
    {
        public UserItemContext(DbContextOptions<UserItemContext> options) : base(options)
        {
            
        }
        public DbSet<UserTable> Users { get; set; }
        public DbSet<ItemTable> Items { get; set; }
        public DbSet<ItemListTable> ItemLists { get; set; }
        public DbSet<UserItemTable> UserItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTable>(eb =>
            {
                eb.Property(u => u.UserEmail).IsRequired().HasMaxLength(200);
                eb.Property(u => u.UserName).IsRequired().HasMaxLength(200);
                eb.Property(u => u.UserSurname).IsRequired().HasMaxLength(200);
                eb.HasMany(w => w.ItemListTable)
                .WithOne(u => u.UserTable)
                .HasForeignKey(x => x.UserTableId);
                });
            modelBuilder.Entity<ItemListTable>(eb =>
            {
                eb.Property(il => il.Date).HasDefaultValueSql("getutcdate()");
            });
        }
    }
}
