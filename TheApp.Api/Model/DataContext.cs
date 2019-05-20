using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheApp.Api.Entities;

namespace TheApp.Api.Model
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }


        public DbSet<UsersInCommunities> UsersInCommunities { get; set; }
        public DbSet<DBUser> Users { get; set; }
        public DbSet<Community> Communities { get; set; }
        public DbSet<CleaningDate> CleaningDates { get; set; }
        public DbSet<VisitorDate> VisitorDates { get; set; }
        public DbSet<ShoppingItem> ShoppingItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //create composite key
            builder.Entity<UsersInCommunities>().HasKey(s => new { s.CommunityId, s.UserID });
        }
    }
}
