using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IOPTCore.Models
{
    public class DataContext : DbContext
    {
        public DbSet<Model> Models { get; set; }

        public DbSet<Object> Objects { get; set; }

        public DbSet<Property> Properties { get; set; }

        public DbSet<Script> Scripts { get; set; }

        public DbSet<Dashboard> Dashboards { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<AppKey> AppKeys { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Model>().HasKey(m => m.id);
            builder.Entity<Object>().HasKey(m => m.id);
            builder.Entity<Property>().HasKey(m => m.id);
            builder.Entity<Script>().HasKey(m => m.id);
            builder.Entity<User>().HasKey(m => m.id);
            builder.Entity<AppKey>().HasKey(m => m.id);
            builder.Entity<Dashboard>().HasKey(m => m.id);



            // shadow properties
            //builder.Entity<Model>().Property<DateTime>("LastUpdated");
            //builder.Entity<Object>().Property<Model>("Model");
            //builder.Entity<Property>().Property<Object>("Object");
            //builder.Entity<Script>().Property<Property>("Property");

            builder.Entity<Model>().HasMany(m => m.objects).WithOne().HasForeignKey(y=>y.modelId).OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Cascade);
            builder.Entity<Object>().HasMany(m => m.properties).WithOne().HasForeignKey(y => y.objectId).OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Cascade);
            builder.Entity<Property>().HasMany(m => m.scripts).WithOne().HasForeignKey(y => y.propertyId).OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Cascade);

            base.OnModelCreating(builder);
        }

        //public override int SaveChanges()
        //{
        //    ChangeTracker.DetectChanges();

        //    updateUpdatedProperty<Model>();
        //    updateUpdatedProperty<Dashboard>();
        //    updateUpdatedProperty<Object>();
        //    updateUpdatedProperty<Property>();
        //    updateUpdatedProperty<Script>();


        //    return base.SaveChanges();
        //}

        //private void updateUpdatedProperty<T>() where T : class
        //{
        //    var modifiedSourceInfo =
        //        ChangeTracker.Entries<T>()
        //            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        //    foreach (var entry in modifiedSourceInfo)
        //    {
        //        entry.Property("LastUpdated").CurrentValue = DateTime.UtcNow;
        //    }
        //}
    }
}
