using System;
using System.Collections.Generic;
using System.Web.Http;
using Affecto.Mapping;
using Affecto.PositiveFeedback.Application;

namespace Affecto.PositiveFeedback.Api
{
    public class FeedbackController : ApiController
    {
        private readonly IFeedbackRepository repository;
        private readonly MapperFactory mapperFactory;

        public FeedbackController(IFeedbackRepository repository, MapperFactory mapperFactory)
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }
            if (mapperFactory == null)
            {
                throw new ArgumentNullException(nameof(mapperFactory));
            }
            this.repository = repository;
            this.mapperFactory = mapperFactory;
        }

        [HttpGet]
        [Route("v1/employees")]
        public IHttpActionResult GetEmployees()
        {
            IEnumerable<Application.Employee> employees = repository.GetEmployees();
            var mapper = mapperFactory.CreateEmployeeMapper();
            IEnumerable<Employee> mappedEmployees = mapper.Map(employees);
            return Ok(mappedEmployees);
        }

        [HttpGet]
        [Route("v1/employee/{id}")]
        public IHttpActionResult GetEmployee(Guid id)
        {
            Application.Employee employee = repository.GetEmployee(id);
            var mapper = mapperFactory.CreateEmployeeMapper();
            return Ok(mapper.Map(employee));
        }
    }
}