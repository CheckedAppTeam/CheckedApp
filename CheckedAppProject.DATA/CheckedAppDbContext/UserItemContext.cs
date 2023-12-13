using CheckedAppProject.DATA.Entities;
using Microsoft.EntityFrameworkCore;

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
                eb.Property(il => il.Date).HasDefaultValueSql("CURRENT_TIMESTAMP");
                eb.HasMany(x => x.ItemTables)
                .WithMany(i => i.ItemListTables)
                .UsingEntity<UserItemTable>(
                    x => x.HasOne(uit => uit.ItemTable)
                    .WithMany()
                    .HasForeignKey(uit => uit.ItemTableId),

                    x => x.HasOne(uit => uit.ItemListTable)
                    .WithMany()
                    .HasForeignKey(uit => uit.ItemListTableId),

                    uit =>
                    {
                        uit.HasKey(uit => new { uit.ItemListTableId, uit.ItemTableId });
                    });
            });
            
        }
    }
}
