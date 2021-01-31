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
    public class FacilitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FacilitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Facilities
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Facility.ToListAsync());
        }

        // GET: Facilities/Details/5
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facility = await _context.Facility
                .FirstOrDefaultAsync(m => m.Id == id);
            if (facility == null)
            {
                return NotFound();
            }

            return View(facility);
        }

        // GET: Facilities/Create
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Create()
        {
            var companies = await _context.Company.ToListAsync();
            var CompaniesList = new List<SelectListItem>();
            var CompaniesDictionary = new SortedDictionary<String, int>();
            
            foreach (Company u in companies)
            {
                CompaniesDictionary.Add(u.Name, u.Id);
                
                
            }
            CompaniesDictionary.OrderBy(kvp => kvp.Key);
            foreach (KeyValuePair<String, int> kvp in CompaniesDictionary) {
                CompaniesList.Add(new SelectListItem() { Value = kvp.Value.ToString(), Text = kvp.Key });
            }
            // CompaniesList.Add(new SelectListItem() { Value = u.Id.ToString(), Text = u.Name });

            ViewBag.Companies = CompaniesList;
       //     ViewBag.CompaniesNames = CompaniesNamesList;
        //    ViewBag.CompaniesIdsList = CompaniesIdsList;
            return View();
        }

        // POST: Facilities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Create([Bind("Id,FacilityName,CompanyId")] FacilitiesViewModel facility)
        {
            if (ModelState.IsValid)
            {
                Company company = _context.Company.Find(facility.CompanyId);
                _context.Add(new Facility
                {
                    FacilityName = facility.FacilityName,
                    Company = company
                  

                }) ;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(facility);
        }

        // GET: Facilities/Edit/5
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facility = await _context.Facility.FindAsync(id);
            if (facility == null)
            {
                return NotFound();
            }
            return View(facility);
        }

        // POST: Facilities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FacilityName")] Facility facility)
        {
            if (id != facility.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(facility);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacilityExists(facility.Id))
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
            return View(facility);
        }

        // GET: Facilities/Delete/5
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facility = await _context.Facility
                .FirstOrDefaultAsync(m => m.Id == id);
            if (facility == null)
            {
                return NotFound();
            }

            return View(facility);
        }

        // POST: Facilities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var facility = await _context.Facility.FindAsync(id);
            _context.Facility.Remove(facility);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacilityExists(int id)
        {
            return _context.Facility.Any(e => e.Id == id);
        }
    }
}
