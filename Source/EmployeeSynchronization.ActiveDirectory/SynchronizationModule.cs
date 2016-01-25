﻿using Autofac;

namespace Affecto.PositiveFeedback.EmployeeSynchronization.ActiveDirectory
{
    public class SynchronizationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<Controller>();
            builder.RegisterType<EmployeeRepository>().As<IEmployeeRepository>();
        }
    }
}