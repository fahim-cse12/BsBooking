using BSBookingQuery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSBookingQuery.Infrastucture
{
    public class BSDbContext : DbContext
    {
        public BSDbContext(DbContextOptions<BSDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            setDefaultValues(modelBuilder);
        }

        private void setDefaultValues(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hotel>()
                .Property(b => b.CreatedAt)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Comment>()
                .Property(b => b.CreatedAt)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Reply>()
                .Property(b => b.CreatedAt)
                .HasDefaultValueSql("getdate()");
        }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Reply> Replys { get; set; }
    }
}
