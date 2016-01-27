using System.Configuration;
using Affecto.ActiveDirectoryService;
using Affecto.Mapping;
using Autofac;

namespace Affecto.PositiveFeedback.EmployeeSynchronization.ActiveDirectory
{
    public class SynchronizationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<Controller>();
            builder.RegisterType<EmployeeRepository>().As<IEmployeeRepository>();
            builder.Register(ctx => ActiveDirectoryServiceFactory.CreateActiveDirectoryService(Configuration.Settings.DomainPath)).As<IActiveDirectoryService>();
            builder.RegisterInstance(Configuration.Settings).As<IConfiguration>();
            builder.RegisterType<PrincipalMapper>().As<IMapper<IPrincipal, Employee>>();
        }
    }
}