using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JQueryDataTableWithAspNetMVC.Models;
using DataTables;

namespace JQueryDataTableWithAspNetMVC.Controllers
{
    
    public class StaffController : Controller
    {
        private readonly EmployeeDbContext _dbContext = new EmployeeDbContext();
        public ActionResult Table()
        {
            var settings = Properties.Settings.Default;
            var formData = HttpContext.Request.Form;

            using (var db = new Database(settings.DbType, settings.DbConnection))
            {
                var response = new Editor(db, "staff")
                    .Model<StaffModel>()
                    .Field(new Field("start_date")
                        .Validator(Validation.DateFormat(
                            Format.DATE_ISO_8601,
                            new ValidationOpts { Message = "Please enter a date in the format yyyy-mm-dd" }
                        ))
                        .GetFormatter(Format.DateSqlToFormat(Format.DATE_ISO_8601))
                        .SetFormatter(Format.DateFormatToSql(Format.DATE_ISO_8601))
                    )
                    .Process(formData)
                    .Data();

                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoadTableData()
        {
            List<StaffModel> staffModels = _dbContext.StaffModels.ToList();
            return Json(new {data = staffModels}, JsonRequestBehavior.AllowGet);
        }
       
    }
}