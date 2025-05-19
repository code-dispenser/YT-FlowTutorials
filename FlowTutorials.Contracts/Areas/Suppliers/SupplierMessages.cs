using ProtoBuf;

namespace FlowTutorials.Contracts.Areas.Suppliers;

/*
    * For simple types and with newer versions of protobuf-net you can use c# records without attributes see below.
*/
public record SupplierViewRequest(int SupplierID);

public record SupplierViewResponse(SupplierView SupplierView);


/*
  
    [ProtoContract]
    public record class SupplierViewResponse
    {
        [ProtoMember(1)] public SupplierView SupplierView { get; }

        public SupplierViewResponse(SupplierView supplierView) => SupplierView = supplierView;

        private SupplierViewResponse() { }

    }

*/
