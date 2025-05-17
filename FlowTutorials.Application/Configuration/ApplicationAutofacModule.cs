using Autofac;
using Instructor.Core.Common.Seeds;

namespace FlowTutorials.Application.Configuration;

public class ApplicationAutofacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies()).AsClosedTypesOf(typeof(IInstructionHandler<,>));
               
        base.Load(builder);
    }
}
