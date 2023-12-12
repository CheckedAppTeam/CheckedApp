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
        public DbSet<Users> UsersEntity { get; set; }
        public DbSet<Items> ItemsEntity { get; set; }
        public DbSet<ItemLists> ItemListsEntity { get; set; }
        public DbSet<UserItems> UserItemsEntity { get; set; }

    }
}
