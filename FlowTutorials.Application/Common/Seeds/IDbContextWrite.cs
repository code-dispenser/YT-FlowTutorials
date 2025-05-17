using FlowTutorials.Application.Common.Models.EFCore;
using Microsoft.EntityFrameworkCore;

namespace FlowTutorials.Application.Common.Seeds;

public interface IDbContextWrite
{
    public DbSet<Supplier> Suppliers { get; set; }
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}