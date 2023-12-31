﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using VehiclesManager.Entities;
using VehiclesManager.Models;

namespace VehiclesManager.Controllers
{
    [Authorize(Roles = "Admin")]
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
            if (status > 0)
            {
                TempData["status"] = "Record has been saved";
            }
            else
            {
                TempData["error"] = "There wa some error, record is not saved";
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
        [ValidateAntiForgeryToken]
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
                IsAvailable = true
            });

            var status = await _db.SaveChangesAsync();

            if (status > 0)
            {
                TempData["status"] = "Record has been saved";
            }
            else
            {
                TempData["error"] = "There wa some error, record is not saved";
            }

            return RedirectToAction("SupplierVehicles", "Management", new { supplierId = supplierId });
        }


        public async Task<ActionResult> Drivers()
        {
            List<Driver> drivers = await _db.Drivers.Where(x => x.IsActive == true).OrderBy(x => x.FirstName).ToListAsync();
            return View(drivers);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddDriver(FormCollection fc)
        {
            string firstname = fc["firstname"];
            string lastname = fc["lastname"];
            string emailadress = fc["emailadress"];


            _db.Drivers.Add(new Driver()
            {
                AddDate = System.DateTime.Now,
                IsActive = true,
                FirstName = firstname,
                LastName = lastname,
                EmailAddress = emailadress,
                IsAvailable = true
            });

            var status = await _db.SaveChangesAsync();

            if (status > 0)
            {
                TempData["status"] = "Record has been saved";
            }
            else
            {
                TempData["error"] = "There wa some error, record is not saved";
            }

            return RedirectToAction("Drivers", "Management");
        }

        public async Task<ActionResult> Clients()
        {
            List<Client> clients = await _db.Clients.Where(x => x.IsActive == true).OrderBy(x => x.Name).ToListAsync();
            return View(clients);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddClient(FormCollection fc)
        {
            string name = fc["name"];

            _db.Clients.Add(new Client()
            {
                AddDate = System.DateTime.Now,
                IsActive = true,
                Name = name
            });


            var status = await _db.SaveChangesAsync();
            if (status > 0)
            {
                TempData["status"] = "Record has been saved";
            }
            else
            {
                TempData["error"] = "There wa some error, record is not saved";
            }
            return RedirectToAction("Clients", "Management");
        }

        public async Task<ActionResult> ClientBranches(int? clientId)
        {
            if (clientId == null)
            {
                return RedirectToAction("Clients", "Management");
            }

            ViewData["clientId"] = clientId;

            List<Branch> vehicles = await _db.Branches.Where(x => x.IsActve == true && x.ClientId == clientId).OrderBy(x => x.AddDate).ToListAsync();
            return View(vehicles);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddBranch(FormCollection fc)
        {

            string name = fc["name"];
            int clientId = Convert.ToInt32(fc["clientId"]);

            _db.Branches.Add(new Branch
            {
                AddDate = DateTime.Now,
                ClientId = clientId,
                IsActve = true,
                Name = name,
            });

            var status = await _db.SaveChangesAsync();
            if (status > 0)
            {
                TempData["status"] = "Record has been saved";
            }
            else
            {
                TempData["error"] = "There wa some error, record is not saved";
            }

            return RedirectToAction("ClientBranches", "Management",new { clientId = clientId});
        }
    }
}