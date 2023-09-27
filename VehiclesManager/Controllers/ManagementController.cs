using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using VehiclesManager.Entities;
using VehiclesManager.Models;

namespace VehiclesManager.Controllers
{
    public class ManagementController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ManagementController()
        {
            _db = new ApplicationDbContext();
        }



        public async Task<ActionResult> Suppliers()
        {
            List<Supplier> suppliers = await _db.Suppliers.Where(x => x.IsActive == true).OrderBy(x => x.Name).ToListAsync();
            return View(suppliers);
        }

        [HttpPost]
        public async Task<ActionResult> AddSupplier(FormCollection fc)
        {
            string name = fc["name"];

            _db.Suppliers.Add(new Supplier()
            {
                AddDate = System.DateTime.Now,
                IsActive = true,
                Name = name,
            });
            var status = await _db.SaveChangesAsync();
            if(status > 0)
            {
                TempData["status"] = "Supplier has been added";
            }else
            {
                TempData["error"] = "Supplier is not added";
            }

            return RedirectToAction("Suppliers", "Management");
        }

        public async Task<ActionResult> SupplierVehicles(int? supplierId)
        {
            if(supplierId == null)
            {
                return RedirectToAction("Suppliers", "Management");
            }

            ViewData["supplierId"] = supplierId;

            List<Vehicle> vehicles = await _db.Vehicles.Where(x => x.IsActive == true && x.SupplierId == supplierId).OrderBy(x => x.AddDate).ToListAsync();
            return View(vehicles);
        }

        [HttpPost]
        public async Task<ActionResult> AddSupplierVehicle(FormCollection fc)
        {
            string registration = fc["registration"];
            string model = fc["model"];
            int supplierId = Convert.ToInt32(fc["supplierId"]);

            _db.Vehicles.Add(new Vehicle()
            {
                AddDate = System.DateTime.Now,
                IsActive = true,
                Registration = registration,
                SupplierId = supplierId,
                Model = model,
            });

            var status = await _db.SaveChangesAsync();
            if (status > 0)
            {
                TempData["status"] = "Vehicle has been added";
            }
            else
            {
                TempData["error"] = "Vehicle is not added";
            }

            return RedirectToAction("SupplierVehicles", "Management", new { supplierId = supplierId });
        }
    }
}