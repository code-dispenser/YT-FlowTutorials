using FlowTutorials.Application.Common.Models.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlowTutorials.Infrastructure.EfCore.Configurations;

public partial class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> entity)
    {
        entity.Property(e => e.CompanyName).IsRequired();

    }
}
