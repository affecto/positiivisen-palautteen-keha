using System;
using System.Collections.Generic;
using System.Linq;
using Affecto.PositiveFeedback.Store.MongoDb;
using Affecto.Testing.SpecFlow;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace Affecto.PositiveFeedback.Application.AcceptanceTests.Infrastructure
{
    [Binding]
    internal static class TestRun
    {
        [BeforeScenario]
        public static void SetupScenario()
        {
            var builder = new ContainerBuilder();
            RegisterProductionCodeModules(builder);
            SetupMockRepository(builder);
            BuildContainer(builder);
            CreateIdentifiersCollection();
        }

        [AfterScenario]
        public static void TearDownScenario()
        {
            List<Exception> exceptions = ScenarioContext.Current.Where(pair => pair.Value is Exception).Select(pair => (Exception)pair.Value).ToList();

            if (exceptions.Any())
            {
                string[] exceptionMessages = exceptions.Select(FormatExceptionMessage).ToArray();
                string exceptionMessage = string.Join(Environment.NewLine, exceptionMessages);
                string exceptionStackTrace = exceptions.First().StackTrace;
                Assert.Fail("Unhandled exception was thrown in scenario:{0}{1}{2}{3}", Environment.NewLine, exceptionMessage, Environment.NewLine, exceptionStackTrace);
            }
        }

        private static void CreateIdentifiersCollection()
        {
            var identifiers = new Identifiers();
            ScenarioContext.Current.Set(identifiers);
        }

        private static string FormatExceptionMessage(Exception e)
        {
            return string.Format("{0}: {1}", e.GetType().FullName, e.Message);
        }

        private static void SetupMockRepository(ContainerBuilder builder)
        {
            builder.RegisterType<MockEmployeeCollection>().As<Store.MongoDb.ICollection<Store.MongoDb.Employee>>();
        }

        private static void RegisterProductionCodeModules(ContainerBuilder builder)
        {
            builder.RegisterModule<StoreModule>();
        }

        private static void BuildContainer(ContainerBuilder builder)
        {
            IContainer container = builder.Build();
            ScenarioContext.Current.Set(container);
        }
    }
}