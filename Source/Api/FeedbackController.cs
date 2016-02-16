using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Affecto.Mapping;
using Affecto.PositiveFeedback.Application;
using WebApi.OutputCache.V2.TimeAttributes;

namespace Affecto.PositiveFeedback.Api
{
    [CacheOutputUntilToday(23, 55)]
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
        [Route("v1/employeefeedback")]
        public IHttpActionResult GetEmployeesWithFeedback()
        {
            IEnumerable<Application.Employee> employees = repository.GetActiveEmployeesWithFeedback();
            var mappedEmployees = MapEmployees(employees);
            return Ok(mappedEmployees);
        }

        [HttpGet]
        [Route("v1/employees")]
        public IHttpActionResult GetEmployees()
        {
            IEnumerable<Application.Employee> employees = repository.GetActiveEmployees();
            var mappedEmployees = MapEmployees(employees);
            return Ok(mappedEmployees);
        }

        [HttpGet]
        [Route("v1/employees/search/{searchCriteria}")]
        public IHttpActionResult SearchEmployees(string searchCriteria)
        {
            IEnumerable<Application.Employee> employees = repository.SearchActiveEmployees(searchCriteria);
            var mappedEmployees = MapEmployees(employees);
            return Ok(mappedEmployees);
        }

        [HttpGet]
        [Route("v1/employees/{id}")]
        public IHttpActionResult GetEmployee(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Id must be provided.", nameof(id));
            }

            Application.Employee employee = repository.GetEmployee(id);
            var mapper = mapperFactory.CreateEmployeeMapper();
            return Ok(mapper.Map(employee));
        }

        [HttpGet]
        [Route("v1/employees/{id}/picture")]
        public HttpResponseMessage GetEmployeePicture(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Id must be provided.", nameof(id));
            }

            using (MemoryStream pictureStream = repository.GetEmployeePicture(id))
            {
                return CreateByteArrayResult(pictureStream);
            }
        }

        [HttpPost]
        [Route("v1/employees/{id}/textfeedback")]
        public IHttpActionResult GiveEmployeeTextFeedback(Guid id, [FromBody] string feedback)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Employee id must be provided.", nameof(id));
            }

            repository.AddTextFeedback(id, feedback);
            return Ok();
        }

        private IEnumerable<Employee> MapEmployees(IEnumerable<Application.Employee> employees)
        {
            var mapper = mapperFactory.CreateEmployeeMapper();
            return mapper.Map(employees);
        }

        private static HttpResponseMessage CreateByteArrayResult(MemoryStream pictureStream)
        {
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);

            if (pictureStream != null)
            {
                result.Content = new ByteArrayContent(pictureStream.ToArray());
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpg");
            }

            return result;
        }
    }
}