using Flow.Core.Areas.Extensions;
using Flow.Core.Areas.Returns;
using FlowTutorials.Application.Areas.Suppliers;
using FlowTutorials.Contracts.Areas.Suppliers;
using Instructor.Core.Common.Seeds;
using ProtoBuf.Grpc;

namespace FlowTutorials.WebServer.GrpcServices
{
    public class GrpcSupplierService(IInstructionDispatcher instructionDispatcher) : IGrpcSuppliersService
    {
        private readonly IInstructionDispatcher _instructionDispatcher = instructionDispatcher;
        public async Task<Flow<SupplierViewResponse>> GetSupplierView(SupplierViewRequest instruction, CallContext context = default)

            => await _instructionDispatcher.SendInstruction(new GetSupplierViewQuery(instruction.SupplierID))
                                                .ReturnAs(success => new SupplierViewResponse(success)); // You can also change the failure if needed with other overloads of the ReturnAs method.
    }
}
