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
    public class EquipmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EquipmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Equipments
        [Authorize(Roles = "Admin,Manager")]
        [Route("Equipment")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Equipment.ToListAsync());
        }

        // GET: Equipments/Details/5
        [Authorize(Roles = "Admin,Manager")]
        [Route("Equipment/Details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipment = await _context.Equipment
                .FirstOrDefaultAsync(m => m.Id == id);
            if (equipment == null)
            {
                return NotFound();
            }

            return View(equipment);
        }

        // GET: Equipments/Create
        [Authorize(Roles = "Admin,Manager")]
        [Route("Equipment/Create")]
        public async Task<IActionResult> Create()
        {
            var buildings = await _context.Building.Include(b => b.Facility).Include(b => b.Facility.Company).ToListAsync();
            
            var BuildingsList = new List<SelectListItem>();
        
            
            foreach (Building b in buildings) {
                BuildingsList.Add(new SelectListItem() { Value = b.Id.ToString(), Text =b.Facility.Company.Name+" : "+b.Facility.FacilityName +" : "+ b.Name });
            }
            var FrequencyTypeList = new List<SelectListItem>();
            FrequencyTypeList.Add(new SelectListItem() { Value = "DAY", Text = "Day" });
            FrequencyTypeList.Add(new SelectListItem() { Value = "MONTH", Text = "Month" });
            FrequencyTypeList.Add(new SelectListItem() { Value = "YEAR", Text = "Year" });
            ViewBag.Buildings = BuildingsList;
            ViewBag.FrequencyTypes = FrequencyTypeList;
            return View();
        }

        // POST: Equipments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Equipment/Create")]
        public async Task<IActionResult> Create([Bind("Id,Category,Description,FrequencyType,MaintenanceFrequency,BuildingId")] EquipmentViewModel equipment)
        {
            var building = _context.Building.Find(equipment.BuildingId);
            if (ModelState.IsValid)
            {
                _context.Add(new Equipment { 
                Building = building,
                MaintenanceFrequency = equipment.MaintenanceFrequency,
                FrequencyType = equipment.FrequencyType,
                Category = equipment.Category,
                Description = equipment.Description

                });
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(equipment);
        }

        // GET: Equipments/Edit/5
        [Authorize(Roles = "Admin,Manager")]
        [Route("Equipment/Edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipment = await _context.Equipment.FindAsync(id);
            if (equipment == null)
            {
                return NotFound();
            }
            return View(equipment);
        }

        // POST: Equipments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Equipment/Edit/{id}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Category,Description,FrequencyType,MaintenanceFrequency")] Equipment equipment)
        {
            if (id != equipment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(equipment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquipmentExists(equipment.Id))
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
            return View(equipment);
        }

        // GET: Equipments/Delete/5
        [Authorize(Roles = "Admin,Manager")]
        [Route("Equipment/Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipment = await _context.Equipment
                .FirstOrDefaultAsync(m => m.Id == id);
            if (equipment == null)
            {
                return NotFound();
            }

            return View(equipment);
        }

        // POST: Equipments/Delete/5
        [Authorize(Roles = "Admin,Manager")]
        [Route("Equipment/Delete/{id}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var equipment = await _context.Equipment.FindAsync(id);
            _context.Equipment.Remove(equipment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EquipmentExists(int id)
        {
            return _context.Equipment.Any(e => e.Id == id);
        }
    }
}
