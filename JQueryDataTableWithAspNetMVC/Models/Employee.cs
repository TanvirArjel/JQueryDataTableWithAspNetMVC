using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JQueryDataTableWithAspNetMVC.Models
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int DepartmentId { get; set; }
        public string Designation { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public DateTime CreatedOn { get; set; }
        public Department Department { get; set; }

    }
}