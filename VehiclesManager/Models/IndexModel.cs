using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VehiclesManager.Entities;

namespace VehiclesManager.Models
{
    public class IndexModel
    {
        public Client Client { get; set; }
        public int NumberOfBranches { get; set; }
        public int TotalLeasedVehicles { get; set; }
    }
}