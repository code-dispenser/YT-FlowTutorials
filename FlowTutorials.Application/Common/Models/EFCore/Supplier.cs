using FlowTutorials.Contracts.Areas.Suppliers;
using System.Linq.Expressions;

namespace FlowTutorials.Application.Common.Models.EFCore;

public partial class Supplier
{
    public int     SupplierID   { get; set; }
    public string  CompanyName  { get; set; } = default!;
    public string? ContactName  { get; set; }
    public string? ContactTitle { get; set; }
    public string? Address      { get; set; }
    public string? City         { get; set; }
    public string? Region       { get; set; }
    public string? PostalCode   { get; set; }
    public string? Country      { get; set; }
    public string? Phone        { get; set; }
    public string? Fax          { get; set; }
    public string? HomePage     { get; set; }
    
    public ICollection<Product> Products { get; set; } = [];



    public static Expression<Func<Supplier, SupplierView>> ProjectToSupplierView

        => supplier => new SupplierView(supplier.SupplierID, supplier.CompanyName, supplier.Products.Count());

}