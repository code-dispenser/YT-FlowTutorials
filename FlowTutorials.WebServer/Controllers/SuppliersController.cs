using Flow.Core.Areas.Returns;
using FlowTutorials.Application.Areas.Suppliers;
using FlowTutorials.Contracts.Areas.Suppliers;
using Instructor.Core;
using Instructor.Core.Common.Seeds;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlowTutorials.WebServer.Controllers;

[Route("[controller]")]
[ApiController]
public class SuppliersController(IInstructionDispatcher dispatcher) : ControllerBase
{
    private readonly IInstructionDispatcher _dispatcher = dispatcher;


    [HttpGet()]
    [Route("{supplierID:int}/view")]
    public async Task<Flow<SupplierView>> SupplierView(int supplierID) // Or Task<ActionResult<Flow<SupplierView>>> if you want to add headers etc
        
        => await _dispatcher.SendInstruction(new GetSupplierViewQuery(supplierID));
    
    /*
        If you do not like the above just use the handlers directly

        [HttpGet()]
        [Route("{supplierID:int}/view")]
        public async Task<Flow<SupplierView>> SupplierViewTwo([FromServices] GetSupplierViewQueryHandler supplierViewHandler, int supplierID, CancellationToken cancellationToken = default)

            => await supplierViewHandler.Handle(new GetSupplierViewQuery(supplierID), cancellationToken);

    */

    [HttpGet()]
    public Task<bool> IsAlive() => Task.FromResult(true);
}
