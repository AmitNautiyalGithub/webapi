using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeService.Models;

namespace EmployeeService.Controllers
{
    [RoutePrefix("api/managers")]
    public class ManagersController : ApiController
    {
        private static List<Manager> listOfManagers = new List<EmployeeService.Models.Manager>()
            {
                new Manager() {ID = 1, FirstName = "Manager 1", Gender = "Male"},
                new Manager() {ID = 2, FirstName = "Manager 2", Gender = "Male"},
                new Manager() {ID = 3, FirstName = "Manager 3", Gender = "Female"},
                new Manager() {ID = 4, FirstName = "Manager 4", Gender = "Male"}
            };


        public IHttpActionResult Get()
        {
            return Ok(listOfManagers);
        }


        //[Route("{id:int}", Name = "GetManagerById")]
        public Manager Get(int id)
        {
            return listOfManagers.FirstOrDefault(m => m.ID == id);
        }

        public HttpResponseMessage Post(Manager manager)
        {
            manager.ID = listOfManagers.Count + 1;
            listOfManagers.Add(manager);

            var msg = Request.CreateResponse(HttpStatusCode.Created);
            //msg.Headers.Location = new Uri(Request.RequestUri + manager.ID.ToString());
            msg.Headers.Location = new Uri(Url.Link("DefaultApi", new {id = manager.ID}));
            return msg;
        }
    }
}
