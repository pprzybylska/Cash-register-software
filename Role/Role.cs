using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Role
{
    public static class Role
    {
        public static string Admin = "Admin";
        public static string Kasjer = "Kasjer";

        public static List<SelectListItem> GetRoleForDropDown()
        {
            return new List<SelectListItem>
            {
                new SelectListItem{Value=Role.Admin,Text=Role.Admin},
                new SelectListItem{Value=Role.Kasjer,Text=Role.Kasjer}
            };
        }
    }
}
