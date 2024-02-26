using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeService.Models;

namespace EmployeeService.Controllers
{
    //[Authorize]
    [RoutePrefix("api/employees")]
    public class EmployeesController : ApiController
    {
        private static List<Manager> listOfManagers = new List<EmployeeService.Models.Manager>()
            {
                new Manager() {ID = 1, FirstName = "Manager 1", Gender = "Male"},
                new Manager() {ID = 2, FirstName = "Manager 2", Gender = "Male"},
                new Manager() {ID = 3, FirstName = "Manager 3", Gender = "Female"},
                new Manager() {ID = 4, FirstName = "Manager 4", Gender = "Male"}
            };

        [Route("")]
        public HttpResponseMessage Get()
        {
            using (EmployeeDBEntities entities = new EmployeeDBEntities())
            {
                return Request.CreateResponse(HttpStatusCode.OK, entities.Employees.ToList());
            }
        }

        //[Route("{id:int:min(1):max(3)}")]
        [Route("{id:int:range(1,3)}")]
        public HttpResponseMessage Get(int id)
        {

            using (EmployeeDBEntities entities = new EmployeeDBEntities())
            {
                var employee = entities.Employees.FirstOrDefault(e => e.ID == id);
                if (employee == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Employee not found");
                }

                return Request.CreateResponse(HttpStatusCode.OK, employee);
            }
        }

        [Route("{name:alpha}")]
        public HttpResponseMessage Get(string name)
        {

            using (EmployeeDBEntities entities = new EmployeeDBEntities())
            {
                var employee = entities.Employees.FirstOrDefault(e => e.FirstName == name);
                if (employee == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Employee not found");
                }

                return Request.CreateResponse(HttpStatusCode.OK, employee);
            }
        }


        [Route("GetById/{id}")]
        public HttpResponseMessage GetById(int id)
        {
            using (EmployeeDBEntities entities = new EmployeeDBEntities())
            {
                var employee = entities.Employees.FirstOrDefault(e => e.ID == id);
                if (employee == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Employee not found");
                }

                return Request.CreateResponse(HttpStatusCode.OK, employee);
            }
        }

        [Route("~/api/manager")]
        public HttpResponseMessage GetManagers()
        {
            var managers = new List<EmployeeService.Models.Manager>()
            {
                new Manager() {ID = 1, FirstName = "Manager 1", Gender = "Male"},
                new Manager() {ID = 1, FirstName = "Manager 1", Gender = "Male"},
                new Manager() {ID = 1, FirstName = "Manager 1", Gender = "Male"},
                new Manager() {ID = 1, FirstName = "Manager 1", Gender = "Male"}
            };

            return Request.CreateResponse(HttpStatusCode.OK, managers);

        }
    }
}
