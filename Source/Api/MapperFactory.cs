using Affecto.Mapping;

namespace Affecto.PositiveFeedback.Api
{
    public class MapperFactory
    {
        public virtual IMapper<Application.Employee, Employee> CreateEmployeeMapper()
        {
            return new EmployeeMapper();
        }
    }
}