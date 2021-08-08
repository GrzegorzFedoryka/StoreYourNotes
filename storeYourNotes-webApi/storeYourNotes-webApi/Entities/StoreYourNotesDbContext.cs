using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace storeYourNotes_webApi.Entities
{
    public class StoreYourNotesDbContext : DbContext
    {
        public StoreYourNotesDbContext(DbContextOptions<StoreYourNotesDbContext> options) : base(options)
        {
            
        }
        public DbSet<Page> Pages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Page>()
                .Property(p => p.OwnerId)
                .IsRequired();
        }
    }
}
