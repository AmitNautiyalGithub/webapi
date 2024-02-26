using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;

namespace EmployeeService.Controllers
{
    public class Employee
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Gender { get; set; }
    }
    //[Authorize]
    //[RequireHttps]
    public class ValuesController : ApiController
    {
        private static List<Employee> employees = new List<Employee>()
        {
            new Employee() { Id = 1, UserName = "Amit", Gender = "Male"},
            new Employee() { Id = 2, UserName = "Sumit", Gender = "Male"},
            new Employee() { Id = 3, UserName = "Reema", Gender = "FeMale"},
            new Employee() { Id = 4, UserName = "Sujata", Gender = "FeMale"}
        };

        // GET api/values
        [BasicAuthentication]
        public HttpResponseMessage Get()
        {
            var userName = Thread.CurrentPrincipal.Identity.Name;
            if (string.IsNullOrWhiteSpace(userName))
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "User not authenticated");
            }

            Employee emp = employees.FirstOrDefault(e => String.Equals(e.UserName, userName, StringComparison.CurrentCultureIgnoreCase));
            if (emp == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Employee not found");
            }

            switch (emp.Gender.ToLower())
            {
                case "male":
                    var maleEmployee = employees.Where(e => string.Equals(e.Gender, "male", StringComparison.CurrentCultureIgnoreCase));
                    return Request.CreateResponse(HttpStatusCode.OK, maleEmployee);

                case "female":
                    var femaleEmployee = employees.Where(e => string.Equals(e.Gender, "female", StringComparison.CurrentCultureIgnoreCase));
                    return Request.CreateResponse(HttpStatusCode.OK, femaleEmployee);

                default:
                    return Request.CreateResponse(HttpStatusCode.OK, employees);
            }
        }

        // GET api/values/5
        public HttpResponseMessage Get(int id)
        {
            var emp = employees.Where(e => e.Id == id);
            if (emp == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Item not found");

            }
            return Request.CreateResponse(HttpStatusCode.OK, emp);
        }

        // POST api/values
        public HttpResponseMessage Post([FromBody]Employee employee)
        {
            employee.Id = employees.Count + 1;
            employees.Add(employee);

            var msg = Request.CreateResponse(HttpStatusCode.OK, employee);
            msg.Headers.Location = new Uri(Request.RequestUri + "/" + employee.Id);
            return msg;
        }

        // PUT api/values/5
        public HttpResponseMessage Put(int id, [FromBody]Employee employee)
        {
            Employee emp = employees.FirstOrDefault(e => e.Id == id);
            if (emp == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Item not found");
            }

            emp.UserName = employee.UserName;
            emp.Gender = employee.Gender;
            return Request.CreateResponse(HttpStatusCode.OK, "Item updated successfully.");

        }

        // DELETE api/values/5
        public HttpResponseMessage Delete(int id)
        {
            var emp = employees.FirstOrDefault(e => e.Id == id);
            if (emp == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Item not found");
            }
            else
            {
                employees.Remove(emp);
                return Request.CreateResponse(HttpStatusCode.OK, "Item deleted successfully.");
            }
        }
    }
}
