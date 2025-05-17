using FlowTutorials.Application.Common.Models.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlowTutorials.Infrastructure.EfCore.Configurations;

public partial class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> entity)
    {
        entity.Property(e => e.Discontinued)
            .IsRequired()
            .HasDefaultValue("0");
        entity.Property(e => e.ProductName).IsRequired();
        entity.Property(e => e.ReorderLevel).HasDefaultValue(0);
        entity.Property(e => e.UnitPrice)
            .HasDefaultValue(0.0)
            .HasColumnType("NUMERIC");
        entity.Property(e => e.UnitsInStock).HasDefaultValue(0);
        entity.Property(e => e.UnitsOnOrder).HasDefaultValue(0);

        entity.HasOne(d => d.Supplier).WithMany(p => p.Products).HasForeignKey(d => d.SupplierID);
    }
}
