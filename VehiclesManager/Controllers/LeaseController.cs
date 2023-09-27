using System;
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
            var status = await _db.SaveChangesAsync();
            if(status > 0)
            {

            }else
            {

            }
            return RedirectToAction("BranchLeaseRecords", "Lease", new { branchId = branchId });
        }
    }
}