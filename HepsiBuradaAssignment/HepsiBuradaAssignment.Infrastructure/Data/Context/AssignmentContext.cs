using System.IO;
using HepsiBuradaAssignment.Domain.Entities;
using HepsiBuradaAssignment.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace HepsiBuradaAssignment.Infrastructure.Data.Context
{
    public class AssignmentContext:DbContext
    {
        public AssignmentContext(DbContextOptions<AssignmentContext> options) : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new OrderConfiguration());
            builder.ApplyConfiguration(new CampaignConfiguration());
        }
    }

    public class AssignmentContextDesignFactory : IDesignTimeDbContextFactory<AssignmentContext>
    {
        public AssignmentContextDesignFactory()
        {

        }
        public AssignmentContext CreateDbContext(string [] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            var connectionString = configuration["ConnectionString"];
            var optionsBuilder = new DbContextOptionsBuilder<AssignmentContext>().UseNpgsql(connectionString);
            return new AssignmentContext(optionsBuilder.Options);
        }
    }
}
