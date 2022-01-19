using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models.ViewModel
{
    public class BonVM
    {
        public Bon bon { get; set; }
        public IEnumerable<SelectListItem> TypeDropDown { get; set; }
    }
}
