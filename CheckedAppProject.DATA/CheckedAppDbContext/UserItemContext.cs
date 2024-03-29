﻿using CheckedAppProject.DATA.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CheckedAppProject.DATA.CheckedAppDbContext
{
    public class UserItemContext : IdentityDbContext<AppUser, IdentityRole, string>
    {
        public UserItemContext(DbContextOptions<UserItemContext> options) : base(options)
        {
        }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemList> ItemLists { get; set; }
        public DbSet<UserItem> UserItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>(eb =>
            {
                eb.Property(u => u.UserName).IsRequired().HasMaxLength(200);
                eb.Property(u => u.UserSurname).HasMaxLength(200);
                eb.HasMany(w => w.ItemList)
                .WithOne(u => u.User)
                .HasForeignKey(x => x.UserId);
            });

            modelBuilder.Entity<ItemList>()
                 .HasMany(e => e.Items)
                 .WithMany(e => e.ItemLists)
                 .UsingEntity<UserItem>();
            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.HasKey(e => e.UserId);
            });
            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });
            });
        }
    }
}
