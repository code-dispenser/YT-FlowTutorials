using Autofac;
using FlowTutorials.Application.Common.Seeds;
using FlowTutorials.Infrastructure.Common.ExceptionHandlers;
using FlowTutorials.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;

namespace FlowTutorials.Infrastructure.Configuration;

public class InfrastructureAutofacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.Register<NorthwindDbRead>(c =>
        {
            var options = new DbContextOptionsBuilder<NorthwindDbRead>().UseSqlite("Data Source=northwind.sqlite;Mode=ReadOnly;")
            //.LogTo(message => Debug.WriteLine(message))
            .Options;

            return new NorthwindDbRead(options);
        }).As<IDbContextReadOnly>().InstancePerLifetimeScope();

        builder.Register<NorthwindDbWrite>(c =>
        {
            var options = new DbContextOptionsBuilder<NorthwindDbWrite>().UseSqlite("Data Source=northwind.sqlite;").Options;
            return new NorthwindDbWrite(options);
        }).As<IDbContextWrite>().InstancePerLifetimeScope();

        builder.RegisterType<SqliteExceptionHandler>().As<IDbExceptionHandler>().SingleInstance();

        base.Load(builder);
    }
}
