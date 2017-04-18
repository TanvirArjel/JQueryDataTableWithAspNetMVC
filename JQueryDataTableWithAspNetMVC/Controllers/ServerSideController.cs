using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;
using JQueryDataTableWithAspNetMVC.Models;

namespace JQueryDataTableWithAspNetMVC.Controllers
{
    public class ServerSideController : Controller
    {
        private EmployeeDbContext db = new EmployeeDbContext();

        // GET: ServerSide
        public async Task<ActionResult> Index()
        {
            
            return View();
        }

        public ActionResult LoadData()
        {

            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            //Find Order Column
            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();

            var employeeName = Request.Form.GetValues("columns[0][search][value]").FirstOrDefault();
            var departmentName = Request.Form.GetValues("columns[1][search][value]").FirstOrDefault();
            var age = Request.Form.GetValues("columns[2][search][value]").FirstOrDefault();
            var gender = Request.Form.GetValues("columns[3][search][value]").FirstOrDefault();
            var createdOn = Request.Form.GetValues("columns[4][search][value]").FirstOrDefault();


            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;


            // dc.Configuration.LazyLoadingEnabled = false; // if your table is relational, contain foreign key

            var employees = db.Employees.Include(x => x.Department).Select(x => new
            {
                x.EmployeeId,
                x.EmployeeName,
                x.Department,
                x.Age,
                x.Gender,
                x.CreatedOn
            }).AsQueryable();

            //Searching By Column
            if (!String.IsNullOrEmpty(employeeName) || !String.IsNullOrEmpty(departmentName) || !String.IsNullOrEmpty(age) || !String.IsNullOrEmpty(gender) || !String.IsNullOrEmpty(createdOn))
            {
                employees = employees.Where(x => x.EmployeeName.Contains(employeeName) && x.Department.DepartmentName.Contains(departmentName) && x.Age.ToString().Contains(age)
                && x.Gender.Contains(gender) && x.CreatedOn.ToString().Contains(createdOn));
            }


            //SORT
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                if (sortColumnDir == "asc" && sortColumn == "DepartmentName")
                {
                    employees = employees.OrderBy(x => x.Department.DepartmentName);
                } else if (sortColumnDir == "desc" && sortColumn == "DepartmentName")
                {
                    employees = employees.OrderByDescending(x => x.Department.DepartmentName);
                }
                else
                {
                    employees = employees.OrderBy(sortColumn + " " + sortColumnDir);
                }
            }



            recordsTotal = employees.Count();
            var data = employees.Skip(skip).Take(pageSize).ToList();
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data }, JsonRequestBehavior.AllowGet);

        }

        // GET: ServerSide/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = await db.Employees.FindAsync(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: ServerSide/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName");
            return View();
        }

        // POST: ServerSide/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "EmployeeId,EmployeeName,DepartmentId,Age,Gender,CreatedOn")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName", employee.DepartmentId);
            return View(employee);
        }

        // GET: ServerSide/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = await db.Employees.FindAsync(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName", employee.DepartmentId);
            return View(employee);
        }

        // POST: ServerSide/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "EmployeeId,EmployeeName,DepartmentId,Age,Gender,CreatedOn")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName", employee.DepartmentId);
            return View(employee);
        }

        // GET: ServerSide/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = await db.Employees.FindAsync(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: ServerSide/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Employee employee = await db.Employees.FindAsync(id);
            db.Employees.Remove(employee);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
