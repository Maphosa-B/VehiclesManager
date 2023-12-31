﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VehiclesManager.Entities;
using VehiclesManager.Models;

namespace VehiclesManager.Controllers
{
    [Authorize(Roles = "User")]
    public class LeaseController : Controller
    {
        private readonly ApplicationDbContext _db;

        public LeaseController()
        {
            _db = new ApplicationDbContext();
        }

        public async Task<ActionResult> Clients()
        {
            List<Client> clients = await _db.Clients.Where(x => x.IsActive == true).OrderBy(x => x.Name).ToListAsync();
            return View(clients);
        }

        public async Task<ActionResult> ClientBranches(int? clientId)
        {
            if (clientId == null)
            {
                return RedirectToAction("Clients", "Lease");
            }

            ViewData["clientId"] = clientId;

            List<Branch> vehicles = await _db.Branches.Where(x => x.IsActve == true && x.ClientId == clientId).OrderBy(x => x.AddDate).ToListAsync();
            return View(vehicles);
        }

        public async Task<ActionResult> BranchLeaseRecords(int? branchId)
        {
            if (branchId == null)
            {
                return RedirectToAction("Clients", "Lease");
            }

            ViewData["branchId"] = branchId;

            LeaseRecordsModel model = new LeaseRecordsModel();
                
                
             model.Records =  await _db.LeasedVehicles.Where(x =>  x.BranchId == branchId)
                                                                   .Include(x => x.Vehicle)
                                                                   .Include(x => x.Vehicle.Supplier)
                                                                   .Include(x => x.Branch)
                                                                   .Include(x => x.Driver)
                                                                   .OrderBy(x => x.AddDate)
                                                                   .ToListAsync();

            model.AvailableDrivers = await _db.Drivers.Where(x => x.IsActive == true && x.IsAvailable == true).ToListAsync();
            model.AvailableVehicles = await _db.Vehicles.Where(x => x.IsAvailable == true && x.IsAvailable == true).Include(x => x.Supplier).ToListAsync();

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SaveLeaseRecord(FormCollection fc)
        {
            int branchId = Convert.ToInt32(fc["branchId"]);
            int driverId = Convert.ToInt32(fc["driverId"]);
            int vehicleId = Convert.ToInt32(fc["vehicleId"]);

             _db.LeasedVehicles.Add(new LeasedVehicle
             {
                AddDate = DateTime.Now,
                BranchId = branchId,
                ConditionStatus = 0,
                DriverId = driverId,
                ReturnDate = null,
                VehicleId = vehicleId,
                IsReturned = false,
            });

            //Lets mark a vehice and driver as not being available since they are allocated
            var dbVehicle = await _db.Vehicles.Where(x => x.Id == vehicleId).FirstOrDefaultAsync();
            dbVehicle.IsAvailable = false;

            var dbDriver = await _db.Drivers.Where(x => x.Id == driverId).FirstOrDefaultAsync();
            dbDriver.IsAvailable = false;

            var status = await _db.SaveChangesAsync();
            if(status > 0)
            {
                TempData["status"] = "Record has been saved";
            }else
            {
                TempData["error"] = "There wa some error, record is not saved";
            }

            return RedirectToAction("BranchLeaseRecords", "Lease", new { branchId = branchId });
        }

        public async Task<ActionResult> ViewRecordDetails(int? leasedVehicleId)
        {
            if (leasedVehicleId == null)
            {
                return RedirectToAction("Clients", "Lease");
            }

            ViewData["clientId"] = leasedVehicleId;

            LeasedVehicle vehicle = await _db.LeasedVehicles.Where(x => x.Id == leasedVehicleId)
                                                            .OrderBy(x => x.AddDate)
                                                            .Include(x => x.Vehicle)
                                                            .Include(x => x.Vehicle.Supplier)
                                                            .Include(x => x.Driver)
                                                            .Include(x => x.Branch)
                                                            .Include(x => x.Branch.Client)
                                                            .FirstOrDefaultAsync();
            return View(vehicle);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ReturnVehicle(FormCollection fc)
        {
            int damageStatus = Convert.ToInt32(fc["damageStatus"]);
            string comment = fc["comment"];
            int leasedVehicleId = Convert.ToInt32(fc["leasedVehicleId"]);

            var data = await _db.LeasedVehicles.Where(x => x.Id == leasedVehicleId)
                                                .Include(x => x.Driver)
                                                .Include(x => x.Vehicle)
                                                .FirstOrDefaultAsync();

            data.IsReturned = true;
            data.ReturnDate = DateTime.Now;
            data.ConditionStatus = damageStatus;
            data.Comment = comment;
            data.Driver.IsAvailable = true;
            data.Vehicle.IsAvailable = true;


            var status = await _db.SaveChangesAsync();
            if (status > 0)
            {
                TempData["status"] = "Record has been saved";
            }
            else
            {
                TempData["error"] = "There wa some error, record is not saved";
            }
            return RedirectToAction("ViewRecordDetails", "Lease", new { leasedVehicleId = leasedVehicleId });
        }
    }
}