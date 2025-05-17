using Autofac;
using Autofac.Extensions.DependencyInjection;
using FlowTutorials.Application.Configuration;
using FlowTutorials.Infrastructure.Configuration;
using Instructor.Core;
using Instructor.Core.Common.Seeds;

namespace FlowTutorials.WebServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.Host.ConfigureContainer<ContainerBuilder>(Container => {


                Container.RegisterType<InstructionDispatcher>().SingleInstance();
                Container.RegisterModule<ApplicationAutofacModule>();
                Container.RegisterModule<InfrastructureAutofacModule>();

                Container.Register<InstructionDispatcher>(c =>
                {
                    var context = c.Resolve<IComponentContext>();
                    return new InstructionDispatcher(type => context.Resolve(type));

                }).As<IInstructionDispatcher>().InstancePerLifetimeScope();
            });

            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
