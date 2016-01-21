using Autofac;

namespace Affecto.PositiveFeedback.EmployeeSynchronization
{
    public class SynchronizationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<Controller>();
        }
    }
}
