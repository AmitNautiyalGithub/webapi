using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeService
{
    public class EmployeeSecurity
    {
        public static bool Login(string userName, string password)
        {
            if (userName.ToLower() == "amit" && password.ToLower() == "123")
            {
                return true;
            }

            return false;
        }
    }
}