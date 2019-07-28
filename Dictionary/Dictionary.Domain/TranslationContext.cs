using Dictionary.Domain.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictionary.Domain
{
    public class TranslationContext : DbContext
    {
        public TranslationContext()//(DbContextOptions options)
                                   //:base(options)
        {

        }
        public DbSet<Definition> Definitions { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer("Data Source =.; Initial Catalog = Dictionary2; Connection Timeout = 3600; Integrated Security = true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.RegisterEntitiesConfigurationFromAssemblyOfType<Definition>();
        }

        public override int SaveChanges()
        {
            MarkReadOnlyEntitiesAsUnchanged();
            return base.SaveChanges();
        }

        private void MarkReadOnlyEntitiesAsUnchanged()
        {
            foreach (var readOnlyEntity in ChangeTracker.Entries<IReadOnlyEntity>())
            {
                readOnlyEntity.State = EntityState.Unchanged;
            }
        }
    }
}
