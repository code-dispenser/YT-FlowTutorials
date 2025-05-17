using FlowTutorials.Application.Common.Models.EFCore;
using FlowTutorials.Application.Common.Seeds;
using FlowTutorials.Infrastructure.EfCore.Configurations;
using Microsoft.EntityFrameworkCore;

namespace FlowTutorials.Infrastructure.EfCore;

public partial class NorthwindDbRead : DbContext, IDbContextReadOnly
{
    public virtual DbSet<Product>  Products  { get; set; }
    public virtual DbSet<Supplier> Suppliers { get; set; }

    public NorthwindDbRead(DbContextOptions<NorthwindDbRead> options) : base(options)
    {
        this.ChangeTracker.QueryTrackingBehavior    = QueryTrackingBehavior.NoTracking;
        this.ChangeTracker.LazyLoadingEnabled       = false;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new SupplierConfiguration());
    }

    public override int SaveChanges()

        => -1;
}