using HepsiBuradaAssignment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HepsiBuradaAssignment.Infrastructure.Data.Configurations
{
    public class CampaignConfiguration : IEntityTypeConfiguration<Campaign>
    {
        public void Configure(EntityTypeBuilder<Campaign> builder)
        {
            builder.ToTable("Campaign");
            builder.HasKey(ci => ci.Id);
            builder.Property(ci => ci.Id).IsRequired();

        }
    }
}
