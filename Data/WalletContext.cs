using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserWallet.Models;

namespace UserWallet.Data
{
    public class WalletContext:DbContext
    {
        public WalletContext(DbContextOptions<WalletContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Database.EnsureCreated();
            //modelBuilder.Entity<User>().HasIndex(x => x.UserId).IsUnique();
        }

        public DbSet<Accaunt> Accounts { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
