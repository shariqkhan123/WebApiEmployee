using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiEmployee.Models
{
    public class EmpModel
    {
        public int EmpID { get; set; }
        public string EmpName { get; set; }
        public string EmpDesignation { get; set; }
        public string EmpMobile { get; set; }
        public string EmpEmail { get; set; }
        public string EmpPassword { get; set; }
    }
}