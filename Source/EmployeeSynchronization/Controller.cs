using System;
using Affecto.PositiveFeedback.Application;

namespace Affecto.PositiveFeedback.EmployeeSynchronization
{
    public class Controller
    {
        private readonly Guid firstEmployeeId = Guid.Parse("780AE7D2-880C-42BE-BFB4-342678D06AB0");
        private readonly Guid secondEmployeeId = Guid.Parse("8166B820-5509-4169-AA89-1E1CD719ADFB");
        private const string FirstEmployeeName = "Antti Affectolainen";
        private const string SecondEmployeeName = "Katja Karttakeskuslainen";

        private readonly IFeedbackRepository repository;

        public Controller(IFeedbackRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }
            this.repository = repository;
        }

        public void Synchronize()
        {
            AddOrUpdateEmployee(firstEmployeeId, FirstEmployeeName);
            AddOrUpdateEmployee(secondEmployeeId, SecondEmployeeName);
        }

        private void AddOrUpdateEmployee(Guid id, string name)
        {
            if (repository.HasEmployee(id))
            {
                repository.UpdateEmployee(id, name);
            }
            else
            {
                repository.AddEmployee(id, name);
            }
        }
    }
}
