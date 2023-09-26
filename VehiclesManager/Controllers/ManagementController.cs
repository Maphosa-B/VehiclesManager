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
    }
}