using Affecto.PositiveFeedback.EmployeeSynchronization;
using Affecto.PositiveFeedback.EmployeeSynchronization.ActiveDirectory;
using Affecto.PositiveFeedback.Store.MongoDb;
using Autofac;

namespace Affecto.PositiveFeedback.ConsoleEmployeeSynchronizer
{
    class Program
    {
        static void Main(string[] args)
        {
            Controller syncController = CreateSynchronizationController();
            syncController.Synchronize();
        }

        private static Controller CreateSynchronizationController()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<SynchronizationModule>();
            builder.RegisterModule<StoreModule>();
            IContainer container = builder.Build();
            return container.Resolve<Controller>();
        }
    }
}