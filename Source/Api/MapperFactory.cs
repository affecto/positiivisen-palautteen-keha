using Affecto.Mapping;

namespace Affecto.PositiveFeedback.Api
{
    public class MapperFactory
    {
        public IMapper<Application.Employee, Employee> CreateEmployeeMapper()
        {
            return new EmployeeMapper();
        }
    }
}