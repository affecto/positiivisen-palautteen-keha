using Affecto.PositiveFeedback.Application.AcceptanceTests.Infrastructure;
using TechTalk.SpecFlow;

namespace Affecto.PositiveFeedback.Application.AcceptanceTests
{
    [Binding]
    [Scope(Feature = "TextFeedback")]
    internal sealed class TextFeedbackSteps : StepDefinition
    {
        [Given(@"the following employees exist:")]
        public void GivenTheFollowingEmployeesExist(Table employees)
        {
            foreach (TableRow employeeRow in employees.Rows)
            {
                string employeeName = employeeRow["Name"];
                Identifiers.Generate(employeeName);
                Repository.AddEmployee(Identifiers.Get(employeeName), employeeName);
            }
        }

        [Given(@"'(.+)' is given free feedback '(.+)'")]
        [When(@"'(.+)' is given free feedback '(.+)'")]
        public void WhenIsGivenFreeFeedback(string employee, string feedback)
        {
            Repository.AddTextFeedback(Identifiers.Get(employee), feedback);
        }

        [Then(@"'(.+)' has the following free feedback:")]
        public void ThenHasTheFollowingFreeFeedback(string employee, Table feedback)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"'(.+)' has no feedback")]
        public void ThenHasNoFeedback(string employee)
        {
            ScenarioContext.Current.Pending();
        }
    }
}
