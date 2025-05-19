using Flow.Core.Areas.Extensions;
using FlowTutorials.ConsoleClient.Common.Seeds;
using FlowTutorials.ConsoleClient.Common.Services;
using FlowTutorials.ConsoleClient.Common.Utilities;

namespace FlowTutorials.ConsoleClient.Examples
{
    internal class Over_The_Wire_With_Grpc(GrpcSuppliersService grpcSupplierService) : IFlowExample
    {
        public Range  PrintLineRange => 18 .. 36;
        public string FileName       => "06_Over_The_Wire_With_Grpc.cs";
        public int    Order          => 6;
        public string Name           => "Over the wire with Grpc";
        public string Description    => "Using flow over the wire with Grpc";


        private readonly GrpcSuppliersService _supplierService = grpcSupplierService;

        public async Task RunExample()
        
            => await GetSuppliersView(_supplierService);

        private async Task GetSuppliersView(GrpcSuppliersService supplierService)
        {
            for (int index = 0; index < 2; index++)
            {

                var supplierID = index == 0 ? Random.Shared.Next(30, 40) : Random.Shared.Next(1, 29);

                var flowResult = await supplierService.GetSupplierView(supplierID)
                                                        .OnFailure(failure => GeneralUtils.WriteLine($"{failure.Reason}\r\n", ConsoleColor.Red))
                                                            .OnSuccess(success => GeneralUtils.WriteLine($"{success.SupplierView}\r\n", ConsoleColor.Green))
                                                                .Finally(failure => null, success => success.SupplierView);
            }

        }


    }
}
