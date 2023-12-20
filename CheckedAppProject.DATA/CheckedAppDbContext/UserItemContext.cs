using CheckedAppProject.DATA.Entities;
using Microsoft.EntityFrameworkCore;

namespace CheckedAppProject.DATA.CheckedAppDbContext

{
    public class UserItemContext : DbContext
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
                eb.Property(u => u.UserEmail).IsRequired().HasMaxLength(200);
                eb.Property(u => u.UserName).IsRequired().HasMaxLength(200);
                eb.Property(u => u.UserSurname).IsRequired().HasMaxLength(200);
                eb.HasMany(w => w.ItemList)
                .WithOne(u => u.User)
                .HasForeignKey(x => x.UserId);
                });
            modelBuilder.Entity<ItemList>(eb =>
            {
                eb.Property(il => il.Date).HasDefaultValueSql("CURRENT_TIMESTAMP");
                eb.HasMany(x => x.Item)
                .WithMany(i => i.ItemList)
                .UsingEntity<UserItem>(
                    x => x.HasOne(uit => uit.Item)
                    .WithMany()
                    .HasForeignKey(uit => uit.ItemId),

                    x => x.HasOne(uit => uit.ItemList)
                    .WithMany()
                    .HasForeignKey(uit => uit.ItemListId),

                    uit =>
                    {
                        uit.HasKey(uit => new { uit.ItemListId, uit.ItemId });
                    });
            });
            
        }
    }
}
