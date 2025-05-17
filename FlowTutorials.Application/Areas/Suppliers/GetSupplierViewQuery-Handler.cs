using Flow.Core.Areas.Returns;
using Flow.Core.Areas.Utilities;
using FlowTutorials.Application.Common.Models.EFCore;
using FlowTutorials.Application.Common.Seeds;
using FlowTutorials.Contracts.Areas.Suppliers;
using Instructor.Core.Common.Seeds;
using Microsoft.EntityFrameworkCore;

namespace FlowTutorials.Application.Areas.Suppliers;


public record GetSupplierViewQuery(int SupplierID) : IInstruction<Flow<SupplierView>>;


public class GetSupplierViewQueryHandler(IDbContextReadOnly dbReadOnly, IDbExceptionHandler exceptionHandler) : IQueryHandler<GetSupplierViewQuery, Flow<SupplierView>>
{
    public Task<Flow<SupplierView>> Handle(GetSupplierViewQuery instruction, CancellationToken cancellationToken)

        => FlowHandler.TryToFlow(async () =>
        {
            var result =  await dbReadOnly.Suppliers.Where(s => s.SupplierID == instruction.SupplierID).Select(Supplier.ProjectToSupplierView).SingleOrDefaultAsync();

            return result is not null ? result : new Failure.ItemNotFoundFailure($"The supplier with id {instruction.SupplierID} does not exist, this record may have been deleted.");

        }, exceptionHandler.Handle<SupplierView>);// Or explicitly:  exception => exceptionHandler.Handle<SupplierView>(exception)) 
}


