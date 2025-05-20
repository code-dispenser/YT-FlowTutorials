using ProtoBuf;

namespace FlowTutorials.Contracts.Areas.Suppliers;

public record class SupplierView(int SupplierID, string CompanyName, int ProductCount);



/*
 
    [ProtoContract]
    public record class SupplierView
    {
        [ProtoMember(1)] public int    SupplierID   { get; }
        [ProtoMember(2)] public string CompanyName  { get; } = string.Empty;
        [ProtoMember(3)] public int    ProductCount { get; }

        private SupplierView() { }

        public SupplierView(int supplierID, string companyName, int productCount)
        {
            SupplierID   = supplierID;
            CompanyName  = companyName;
            ProductCount = productCount;
        }
    }

*/