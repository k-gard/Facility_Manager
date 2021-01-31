using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FacilityManager.Data;
using FacilityManager.Models;

namespace FacilityManager.Controllers
{
    public class WorkOrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WorkOrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: WorkOrders
        public async Task<IActionResult> Index()
        {
            return View(await _context.WorkOrder.Include(w => w.Building).ToListAsync());
        }

        // GET: WorkOrders/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }



            var workOrder = _context.WorkOrder.Include(w => w.Equipment)
                                              .Include(w => w.Employees)
                                              .Include(w => w.Building)
                                              .Include(w => w.Contractors)
                                              .Include(w => w.Tasks).SingleOrDefaultAsync(w => w.Id == id);
            if (workOrder == null)
            {
                return NotFound();
            }
            List<String> tasks = new List<String>();
            List<String> equipment = new List<String>();
            List<String> employees = new List<String>();
            List<String> contractors = new List<String>();

            if (workOrder.Result.Employees.Count != 0)
            {
                foreach (WorkOrderEmployee e in workOrder.Result.Employees)
                {
                    Employee emp = _context.Employee.Find(e.EmployeeId);
                    employees.Add(emp.FirstName + " " + emp.LastName);
                }
            }
            else { employees.Add("No Employees"); }

            if (workOrder.Result.Tasks.Count != 0)
            {
                foreach (WorkOrderTask t in workOrder.Result.Tasks)
                {
                    tasks.Add(_context.Task.Find(t.TaskId).Description);
                }
            }

            if (workOrder.Result.Tasks.Count != 0)
            {
                foreach (ContractorWorkOrder w in workOrder.Result.Contractors)
                {
                    contractors.Add(_context.Contractor.Find(w.Contractor.ContractorId).Company.Name);
                }

            }
            else { employees.Add("No Contractors"); }

            WorkOrderViewModel workOrderViewModel = new WorkOrderViewModel();
           // WorkOrderViewModel workOrderViewModel = new WorkOrderViewModel


            workOrderViewModel.BuildingName = workOrder.Result.Building.Name;
            workOrderViewModel.WorkStart = workOrder.Result.WorkStart;
            workOrderViewModel.WorkEnd = workOrder.Result.WorkEnd;
            workOrderViewModel.EquipmentName = workOrder.Result.Equipment.Description;
            workOrderViewModel.EmployeeNames = employees;
            workOrderViewModel.ContractorNames = contractors;
            workOrderViewModel.TasksName = tasks;

            






          //  var workOrder = await _context.WorkOrder
           //     .FirstOrDefaultAsync(m => m.Id == id);
            if (workOrder == null)
            {
                return NotFound();
            }

            return View(workOrderViewModel);
        }

        // GET: WorkOrders/Create
        public async Task<IActionResult> CreateAsync()
        {
            var contractors = await _context.Company.Where(c => c.Contractor != null).ToListAsync();
            var employees = await _context.Employee.ToListAsync();
            var buildings = await _context.Building.ToListAsync();
            var tasks = await _context.Task.ToListAsync();
            var equipment = await _context.Equipment.ToListAsync();
            var BuildingsList = new List<SelectListItem>();
            var ContractorsList = new List<SelectListItem>();
            var EmployeesList = new List<SelectListItem>();
            var TaskList = new List<SelectListItem>();
            var EquipmentList = new List<SelectListItem>();

            foreach (Models.Task t in tasks) {
            TaskList.Add(new SelectListItem() { Value = t.Id.ToString(), Text = t.Category +":" + t.Description });
            }
            
            
            foreach (Company c in contractors)
            {
                ContractorsList.Add(new SelectListItem() { Value = c.Id.ToString(), Text = c.Name});
                
            }
            foreach (Employee e in employees)
            {
                EmployeesList.Add(new SelectListItem() { Value = e.Id.ToString(), Text = e.FirstName + e.LastName });
                
            }
            foreach (Building b in buildings) 
            {
                BuildingsList.Add(new SelectListItem() { Value = b.Id.ToString(), Text = b.Name });
              
            }
            foreach (Equipment e in equipment)
            {
                EquipmentList.Add(new SelectListItem() { Value = e.Id.ToString(), Text = e.Category + ":" + e.Description});

            }
            ViewBag.Employees = EmployeesList;
            ViewBag.Contractors = ContractorsList;
            ViewBag.Buildings = BuildingsList;
            ViewBag.Tasks = TaskList;
            ViewBag.Equipment = EquipmentList;
            return View();
        }

        // POST: WorkOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Cost,Contractor,Building,Employee,Tasks,WorkStart,WorkEnd,Equipment")] WorkOrderViewModel workOrder)
        {
            WorkOrder _workOrder = new WorkOrder();
            List<WorkOrderEmployee> employeesList = new List<WorkOrderEmployee>();
            List<ContractorWorkOrder> contractorsList = new List<ContractorWorkOrder>();
            List<WorkOrderTask> tasksList = new List<WorkOrderTask>();

            if (!workOrder.Equipment.Equals("0"))
            {
                
                _workOrder.Equipment=_context.Equipment.Find(workOrder.Equipment);
                

            }

            if (!workOrder.Employee[0].Equals("0"))
            {
                foreach (String s in workOrder.Employee)
                {
                   
                    employeesList.Add(new WorkOrderEmployee
                    {
                        Employee = _context.Employee.Find(s),
                        WorkOrder = _workOrder
                    });

                }
            }

            if (workOrder.Contractor[0] != 0 ){
                foreach (int i in workOrder.Contractor)
                {
                   
                    
                    contractorsList.Add(new ContractorWorkOrder { 
                    Contractor = _context.Contractor.Find(i),
                    WorkOrder = _workOrder

                });
                }

            }

            if (!workOrder.Tasks[0].Equals("0") ){
                foreach (int i in workOrder.Tasks)
                {
                    tasksList.Add(new WorkOrderTask
                    { Task = _context.Task.Find(i),
                      WorkOrder = _workOrder  
                    }) ;
                       
                }

            }

           

            Building building = _context.Building.Find(workOrder.Building);

            if (ModelState.IsValid)
            {

                _workOrder.Employees = employeesList;
                _workOrder.Contractors = contractorsList;
                _workOrder.Tasks = tasksList;
                _workOrder.Building = building;
                _workOrder.Cost = workOrder.Cost;
                _workOrder.WorkStart = workOrder.WorkStart;
                _workOrder.WorkEnd = workOrder.WorkEnd;
                
                _context.Add(_workOrder);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(workOrder);
        }

        // GET: WorkOrders/Edit/5
        public async Task<IActionResult> EditAsync(int? id)
        {


            var contractors = await _context.Company.Where(c => c.Contractor != null).ToListAsync();
            var employees = await _context.Employee.ToListAsync();
            var buildings = await _context.Building.ToListAsync();
            var tasks = await _context.Task.ToListAsync();
            var equipment = await _context.Equipment.ToListAsync();
            var BuildingsList = new List<SelectListItem>();
            var ContractorsList = new List<SelectListItem>();
            var EmployeesList = new List<SelectListItem>();
            var TaskList = new List<SelectListItem>();
            var EquipmentList = new List<SelectListItem>();

            foreach (Models.Task t in tasks)
            {
                TaskList.Add(new SelectListItem() { Value = t.Id.ToString(), Text = t.Category + ":" + t.Description });
            }


            foreach (Company c in contractors)
            {
                ContractorsList.Add(new SelectListItem() { Value = c.Id.ToString(), Text = c.Name });

            }
            foreach (Employee e in employees)
            {
                EmployeesList.Add(new SelectListItem() { Value = e.Id.ToString(), Text = e.FirstName + e.LastName });

            }
            foreach (Building b in buildings)
            {
                BuildingsList.Add(new SelectListItem() { Value = b.Id.ToString(), Text = b.Name });

            }
            foreach (Equipment e in equipment)
            {
                EquipmentList.Add(new SelectListItem() { Value = e.Id.ToString(), Text = e.Category + ":" + e.Description });

            }
            ViewBag.Employees = EmployeesList;
            ViewBag.Contractors = ContractorsList;
            ViewBag.Buildings = BuildingsList;
            ViewBag.Tasks = TaskList;
            ViewBag.Equipment = EquipmentList;















            if (id == null)
            {
                return NotFound();
            }

            var workOrder = _context.WorkOrder.Include(w => w.Equipment)
                                              .Include(w => w.Employees)
                                              .Include(w => w.Contractors)
                                              .Include(w => w.Tasks).SingleOrDefaultAsync(w => w.Id == id);

            List<String> tasksName = new List<String>();
            List<String> equipmentName = new List<String>();
            List<String> employeesNames = new List<String>();
            List<String> contractorsNames = new List<String>();

            if (workOrder.Result.Employees.Count != 0)
            {
                foreach (WorkOrderEmployee e in workOrder.Result.Employees)
                {
                    Employee emp = _context.Employee.Find(e.EmployeeId);
                    employeesNames.Add(emp.FirstName + " " + emp.LastName);
                }
            }
            else { employeesNames.Add("No Employees"); }

            if (workOrder.Result.Tasks.Count != 0)
            {
                foreach (WorkOrderTask t in workOrder.Result.Tasks)
                {
                    tasksName.Add(_context.Task.Find(t.TaskId).Description);
                }
            }

            if (workOrder.Result.Tasks.Count != 0)
            {
                foreach (ContractorWorkOrder w in workOrder.Result.Contractors)
                {
                    contractorsNames.Add(_context.Contractor.Find(w.Contractor.ContractorId).Company.Name);
                }

            }
            else { contractorsNames.Add("No Contractors"); }

            WorkOrderViewModel workOrderViewModel = new WorkOrderViewModel();
            // WorkOrderViewModel workOrderViewModel = new WorkOrderViewModel


            workOrderViewModel.BuildingName = workOrder.Result.Building.Name;
            workOrderViewModel.WorkStart = workOrder.Result.WorkStart;
            workOrderViewModel.WorkEnd = workOrder.Result.WorkEnd;
            workOrderViewModel.EquipmentName = workOrder.Result.Equipment.Description;
            workOrderViewModel.EmployeeNames = employeesNames;
            workOrderViewModel.ContractorNames = contractorsNames;
            workOrderViewModel.TasksName = tasksName;








            if (workOrder == null)
            {
                return NotFound();
            }
            
            return View(workOrderViewModel);
        }

        // POST: WorkOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Cost")] WorkOrder workOrder)
        {
            if (id != workOrder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkOrderExists(workOrder.Id))
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
            return View(workOrder);
        }

        // GET: WorkOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workOrder = await _context.WorkOrder
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workOrder == null)
            {
                return NotFound();
            }

            return View(workOrder);
        }

        // POST: WorkOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var workOrder = await _context.WorkOrder.FindAsync(id);
            _context.WorkOrder.Remove(workOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkOrderExists(int id)
        {
            return _context.WorkOrder.Any(e => e.Id == id);
        }
    }
}
