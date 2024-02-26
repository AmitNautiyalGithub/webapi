using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeService.Models;

namespace EmployeeService.Controllers
{
    public class StudentsV2Controller : ApiController
    {
        private static List<StudentV2> listOfStudentsv2 = new List<StudentV2>()
            {
                new StudentV2() {ID = 1, FirstName = "Student V2 - 1",LastName="lstname 1"},
                new StudentV2() {ID = 2, FirstName = "Student V2 - 2",LastName="lstname 2"},
                new StudentV2() {ID = 3, FirstName = "Student V2 - 3",LastName="lstname 3"},
                new StudentV2() {ID = 4, FirstName = "Student V2 - 4",LastName="lstname 4"}
            };

      //  [Route("api/v2/students")]
        public IHttpActionResult Get()
        {
            return Ok(listOfStudentsv2);
        }
       // [Route("api/v2/students/{id}")]
        public StudentV2 Get(int id)
        {
            return listOfStudentsv2.FirstOrDefault(m => m.ID == id);
        }
    }
}
