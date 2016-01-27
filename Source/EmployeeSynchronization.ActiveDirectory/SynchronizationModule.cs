using System.Configuration;
using Affecto.ActiveDirectoryService;
using Autofac;

namespace Affecto.PositiveFeedback.EmployeeSynchronization.ActiveDirectory
{
    public class SynchronizationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterInstance(Configuration.Settings).As<IConfiguration>();
            builder.RegisterType<Controller>();
            builder.RegisterType<EmployeeRepository>().As<IEmployeeRepository>();
            builder
                .Register(componentContext =>
                {
                    IConfiguration configuration = componentContext.Resolve<IConfiguration>();
                    return ActiveDirectoryServiceFactory.CreateActiveDirectoryService(configuration.DomainPath);
                })
                .As<IActiveDirectoryService>();
        }
    }
}