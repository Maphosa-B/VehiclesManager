using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VehiclesManager.Entities;

namespace VehiclesManager.Models
{
    public class LeaseRecordsModel
    {
        public List<LeasedVehicle> Records { get; set; } = new List<LeasedVehicle>();
        public List<Vehicle> AvailableVehicles { get; set; } = new List<Vehicle>();
        public List<Driver> AvailableDrivers { get; set; } = new List<Driver>();
    }
}