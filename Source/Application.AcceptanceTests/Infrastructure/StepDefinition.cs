using Affecto.Testing.SpecFlow;
using Autofac;
using TechTalk.SpecFlow;

namespace Affecto.PositiveFeedback.Application.AcceptanceTests.Infrastructure
{
    internal abstract class StepDefinition
    {
        private static IContainer Container => ScenarioContext.Current.Get<IContainer>();

        protected MockEmployeeCollection EmployeeCollection => Container.Resolve<MockEmployeeCollection>();
        protected IFeedbackRepository Repository => Container.Resolve<IFeedbackRepository>();
        protected Identifiers Identifiers => ScenarioContext.Current.Get<Identifiers>();
    }
}
