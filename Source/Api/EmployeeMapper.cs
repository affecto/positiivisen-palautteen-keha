using Affecto.Mapping.AutoMapper;
using AutoMapper;

namespace Affecto.PositiveFeedback.Api
{
    public class EmployeeMapper : OneWayMapper<Application.Employee, Employee>
    {
        protected override void ConfigureMaps()
        {
            Mapper.CreateMap<Application.Employee, Employee>();
        }
    }
}