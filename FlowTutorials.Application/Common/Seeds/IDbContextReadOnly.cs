using FlowTutorials.Application.Common.Models.EFCore;
using Microsoft.EntityFrameworkCore;

namespace FlowTutorials.Application.Common.Seeds;

public interface IDbContextReadOnly
{
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Product>  Products  { get; set; }
}
