using System;

namespace Affecto.PositiveFeedback.Application
{
    public interface IFeedbackRepository
    {
        bool HasEmployee(Guid id);
        void AddEmployee(Guid id, string name);
        void UpdateEmployee(Guid id, string name);
    }
}
