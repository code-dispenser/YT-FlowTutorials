using Flow.Core.Areas.Returns;
using Flow.Core.Common.Models;
using ProtoBuf.Grpc;
using ProtoBuf.Grpc.Configuration;

namespace FlowTutorials.Contracts.Areas.Suppliers;

[Service]
public interface IGrpcSuppliersService
{
    [Operation]
    Task<Flow<SupplierViewResponse>> GetSupplierView(SupplierViewRequest instruction, CallContext context = default);

}