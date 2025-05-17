using ProtoBuf;

namespace FlowTutorials.Contracts.Areas.Suppliers;

/*
    * Using newer protobuf-net allows c# records without attributes i.e [ProtoContract], [ProtoMember]
*/
public record GetSupplier(int SupplierID);

public record GetSupplierResponse(SupplierView SupplierView);

public record DeleteSupplier(int SupplierID);
