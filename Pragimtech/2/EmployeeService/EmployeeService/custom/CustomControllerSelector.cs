using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using Microsoft.Owin.Security.OAuth;

namespace EmployeeService.custom
{
    public class CustomControllerSelector : DefaultHttpControllerSelector
    {
        private readonly HttpConfiguration _configuration;

        public CustomControllerSelector(HttpConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            var controllers = GetControllerMapping();
            var routeData = request.GetRouteData();

            var controllername = routeData.Values["controller"].ToString();
            var versionNumber = "1";

            // by QueryString
            //var versionQueryString = HttpUtility.ParseQueryString(request.RequestUri.Query);
            //if (versionQueryString["v"] != null)
            //{
            //    versionNumber = versionQueryString["v"];
            //}

            // by customer header
            //var customHeader = "X-StudentService-Version";
            //if (request.Headers.Contains(customHeader))
            //{
            //  versionNumber =  request.Headers.GetValues(customHeader).FirstOrDefault();
            //}
            //if (versionNumber.Contains(','))
            //{
            //    versionNumber = versionNumber.Split(',')[0];
            //}


            // by Accept header
            //var acceptHeader =
            //    request.Headers.Accept.Where(h => h.Parameters.Count(p => p.Name.ToLower() == "version") > 0);
            //if (acceptHeader.Any())
            //{
            //    versionNumber = acceptHeader.First().Parameters.First(p => p.Name.ToLower() == "version").Value;
            //}

            // by Custom media type

            //media type = application/amit.students.v1+json
            var regx = @"application\/amit\.([a-z]+)\.v(?<version>[0-9]+)\+([a-z]+)";

            var acceptHeader =
                request.Headers.Accept.Where(h => Regex.IsMatch(h.MediaType, regx, RegexOptions.IgnoreCase));
            
            if (acceptHeader.Any())
            {
                var match = Regex.Match(acceptHeader.First().MediaType, regx, RegexOptions.IgnoreCase);
                versionNumber = match.Groups["version"].Value;
            }


            #region Controller by version

            if (versionNumber == "1")
            {
                controllername = controllername + "v1";
            }
            else
            {
                controllername = controllername + "v2";
            }

            HttpControllerDescriptor controllerDescriptor;
            if (controllers.TryGetValue(controllername, out controllerDescriptor))
            {
                return controllerDescriptor;
            }

            #endregion

            return null;
        }
    }
}