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
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public HomeController()
        {
            _db = new ApplicationDbContext();
        }

        public async Task<ActionResult> Index()
        {
            var suppliers = await _db.Suppliers.Where(x =>x.IsActive == true).Include(x => x.Vehicles).ToListAsync();


            //Lets clean a list of vehicles and take active ones only
            int systemVehices = 0;
            foreach(var i in suppliers)
            {
                foreach(var r in i.Vehicles.Where(x => x.IsActive == true).ToList())
                {
                    systemVehices++;
                }
            }

            ViewData["SuppliersCount"] = suppliers.Count();
            ViewData["VehiclesCount"] = systemVehices;


            //lets get all drivers in the system
            List<Driver> drivers = await _db.Drivers.Where(x => x.IsActive == true).ToListAsync();
            ViewData["DriverCount"] = drivers.Count();

            List<IndexModel> model = new List<IndexModel>();

            var clients = await _db.Clients.Where(x => x.IsActive == true).Include(x => x.Branches).ToListAsync();
            ViewData["ClientsTotal"] = clients.Count();

            int branchesTotal = 0;
            int totalLeasedVehicles = 0;
            foreach (var i in clients)
            {
                IndexModel tempModel = new IndexModel
                {
                    Client = i,
                    NumberOfBranches = i.Branches.Count(),
                };

                foreach(var r in i.Branches)
                {
                    var leaseVehicles = await _db.LeasedVehicles.Where(x => x.BranchId == r.Id && x.IsReturned == false).ToListAsync();
                    tempModel.TotalLeasedVehicles += leaseVehicles.Count();
                    totalLeasedVehicles += leaseVehicles.Count();
                    branchesTotal++;
                }
                model.Add(tempModel);
            }
            ViewData["BranchesCount"] = branchesTotal;
            ViewData["LeasedVehicles"] = totalLeasedVehicles;

            return View(model);
        }

    }
}