using Affecto.PositiveFeedback.Application;
using Autofac;

namespace Affecto.PositiveFeedback.Store.MongoDb
{
    public class StoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<FeedbackRepository>().As<IFeedbackRepository>();
        }
    }
}
