using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FacilityManager.Data;
using FacilityManager.Models;
using Microsoft.AspNetCore.Authorization;

namespace FacilityManager.Controllers
{
    public class ReportController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Report
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Report.ToListAsync());
        }

        // GET: Report/Details/5
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Report
                .FirstOrDefaultAsync(m => m.Id == id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        // GET: Report/Create
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> CreateAsync()
        {
            var companies = await _context.Company.ToListAsync();
            List<SelectListItem> companiesList = new List<SelectListItem>();

            foreach (Company c in companies)
            {

                companiesList.Add(new SelectListItem() { Value = c.Id.ToString(), Text = c.Name });

            }

            ViewBag.Companies = companiesList;

            return View();
        }

        // POST: Report/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Create([Bind("CompanyId,fromDate,toDate")] ReportsViewModel report)
        {
            var workorders = await _context.WorkOrder.FromSqlRaw(
                "SELECT * FROM WorkOrder WHERE WorkOrder.BuildingId IN" +
                "(SELECT BuildingId FROM Building AS B INNER JOIN Facility AS F ON  B.FacilityId = F.Id WHERE B.FacilityId IN " +
                "(SELECT Facility.Id FROM Facility INNER JOIN Company ON Facility.CompanyId = Company.Id WHERE CompanyId = {0}))", report.CompanyId).Include(w => w.Employees).ToListAsync();
            //      var test = CalculateTotalWorkHoursCost(workorders, report);
            Report _report = new Report
            {
                fromDate = report.fromDate,
                toDate = report.toDate,
                Company = _context.Company.Find(report.Id),
                TotalWorkCost = CalculateTotalWorkHoursCost(workorders, report),
                TotalWorkHours = CalculateTotalHours(workorders, report),
                TotalCost = CalculateTotalCost(workorders, report),
                TotalWorkOrders = ReportWorkOrdersNumber(workorders,report)
            };
            if (ModelState.IsValid)
            {
                _context.Add(_report);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(report);
        }

        private int ReportWorkOrdersNumber(List<WorkOrder> workorders ,ReportsViewModel report)
        {
            int count = 0;

            foreach (WorkOrder w in workorders)
            {
                 
                if ((DateTime.Compare(w.WorkStart, report.fromDate) >= 0) && (DateTime.Compare(w.WorkEnd, report.toDate) < 0))
                {
                    count++;
                }
                

            }
            return count;
        }

        private float CalculateTotalWorkHoursCost(List<WorkOrder> workorders, ReportsViewModel report)
        {
            float total = 0;

            foreach (WorkOrder w in workorders)
            {
                 var totaltime = w.WorkEnd - w.WorkStart;
                if ((DateTime.Compare(w.WorkStart, report.fromDate) >= 0) && (DateTime.Compare(w.WorkEnd, report.toDate) < 0) && w.Employees.Count > 0)
                {
                    

                    foreach (WorkOrderEmployee e in w.Employees)
                    {
                        Employee emp = _context.Employee.Find(e.EmployeeId);
                        total +=(float) _context.Employee.Find(e.EmployeeId).CostPerHour * totaltime.Minutes / 60 ;
                    }
                 
                }

            }
            return total;
        }

        private int CalculateTotalCost(List<WorkOrder> workorders, ReportsViewModel report)
        {

           
            long total = 0;
            foreach (WorkOrder w in workorders) {
                if ((DateTime.Compare(w.WorkStart, report.fromDate) >= 0) && (DateTime.Compare(w.WorkEnd, report.toDate) < 0))
                {
                    total += w.Cost;
                }
            }
            return (int) total;
        }

        private  float CalculateTotalHours(List<WorkOrder> workorders, ReportsViewModel report) 
        {
            
            TimeSpan totaltime = new TimeSpan();
            foreach (WorkOrder w in workorders)
            {
                if ((DateTime.Compare(w.WorkStart, report.fromDate) >= 0) && (DateTime.Compare(w.WorkEnd, report.toDate) < 0))
                {
                    totaltime += w.WorkEnd - w.WorkStart;
                }
                
            }
            double total = totaltime.TotalMinutes;
            return (float) total / 60;
        }


        // GET: Report/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Report.FindAsync(id);
            if (report == null)
            {
                return NotFound();
            }
            return View(report);
        }

        // POST: Report/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TotalWorkOrders,TotalWorkHours,TotalCost,fromDate,toDate")] Report report)
        {
            if (id != report.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(report);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReportExists(report.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(report);
        }

        // GET: Report/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Report
                .FirstOrDefaultAsync(m => m.Id == id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        // POST: Report/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var report = await _context.Report.FindAsync(id);
            _context.Report.Remove(report);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReportExists(int id)
        {
            return _context.Report.Any(e => e.Id == id);
        }
    }
}
