using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VehiclesManager.Models;

namespace VehiclesManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public HomeController()
        {
            _db = new ApplicationDbContext();
        }

        public async Task<ActionResult> Index()
        {

            var a = await _db.Suppliers.Include(x => x.Vehicles).ToListAsync();


            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}