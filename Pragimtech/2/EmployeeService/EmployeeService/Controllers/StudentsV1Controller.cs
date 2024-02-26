using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeService.Models;

namespace EmployeeService.Controllers
{
    public class StudentsV1Controller : ApiController
    {
        private static List<StudentV1> listOfStudentsv1 = new List<StudentV1>()
            {
                new StudentV1() {ID = 1, FirstName = "Student V1 - 1"},
                new StudentV1() {ID = 2, FirstName = "Student V1 - 2"},
                new StudentV1() {ID = 3, FirstName = "Student V1 - 3"},
                new StudentV1() {ID = 4, FirstName = "Student V1 - 4"}
            };


        //[Route("api/v1/students")]
        public IHttpActionResult Get()
        {
            return Ok(listOfStudentsv1);
        }

       // [Route("api/v1/students/{id}")]
        public StudentV1 Get(int id)
        {
            return listOfStudentsv1.FirstOrDefault(m => m.ID == id);
        }
    }
}
