using Flow.Core.Areas.Returns;
using FlowTutorials.ConsoleClient.Common.Extensions;
using FlowTutorials.Contracts.Areas.Suppliers;
using Grpc.Core;
using ProtoBuf.Grpc;

namespace FlowTutorials.ConsoleClient.Common.Services;

internal class GrpcSuppliersService(IGrpcSuppliersService suppliersService)
{
    private readonly IGrpcSuppliersService _suppliersService = suppliersService;

    public async Task<Flow<SupplierViewResponse>> GetSupplierView(int supplierId)
     
        => await _suppliersService.GetSupplierView(new SupplierViewRequest(supplierId)).TryCatchGrpcResult();




}
