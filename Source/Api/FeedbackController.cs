﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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

            Stream pictureStream = repository.GetEmployeePicture(id);
            return CreateBinaryStreamResult(pictureStream);
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

        private static HttpResponseMessage CreateBinaryStreamResult(Stream pictureStream)
        {
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StreamContent(pictureStream);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            return result;
        }
    }
}