namespace FlowTutorials.Application.Common.Models.EFCore;

public class Product
{
    public int     ProductID       { get; set; }
    public string  ProductName     { get; set; } = null!;
    public int     SupplierID      { get; set; }
    public int     CategoryID      { get; set; }
    public string  QuantityPerUnit { get; set; } = null!;
    public double  UnitPrice       { get; set; }
    public int     UnitsInStock    { get; set; }
    public int     UnitsOnOrder    { get; set; }
    public int     ReorderLevel    { get; set; }
    public string  Discontinued    { get; set; } = null!;
    
    public Supplier? Supplier      { get; set; }
}

