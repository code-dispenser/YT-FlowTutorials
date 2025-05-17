using FlowTutorials.Application.Common.Models.EFCore;
using FlowTutorials.Application.Common.Seeds;
using FlowTutorials.Infrastructure.EfCore.Configurations;
using Microsoft.EntityFrameworkCore;

namespace FlowTutorials.Infrastructure.EfCore;

public partial class NorthwindDbWrite : DbContext, IDbContextWrite
{
    public virtual DbSet<Supplier> Suppliers { get; set; }
    
    public NorthwindDbWrite(DbContextOptions<NorthwindDbWrite> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)

        => modelBuilder.ApplyConfiguration(new SupplierConfiguration());

}
