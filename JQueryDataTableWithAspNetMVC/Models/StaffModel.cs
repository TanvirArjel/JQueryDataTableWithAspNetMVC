using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JQueryDataTableWithAspNetMVC.Models
{
    public class StaffModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StaffId { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string position { get; set; }
        public string email { get; set; }
        public string office { get; set; }
        public int extn { get; set; }
        public int age { get; set; }
        public int salary { get; set; }
        public string start_date { get; set; }
    }
}