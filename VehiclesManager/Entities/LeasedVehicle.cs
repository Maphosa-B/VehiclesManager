using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VehiclesManager.Entities
{
    public class LeasedVehicle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public bool IsReturned { get; set; } = false;

        [Required]
        public DateTime AddDate { get; set; } = DateTime.Now;

        public DateTime? ReturnDate { get; set; } = null;

        public int? ConditionStatus { get; set; } = null;

        [Required]
        [ForeignKey("Vehicle")]
        public int VehicleId { get; set; }

        [Required]
        [ForeignKey("Driver")]
        public int DriverId { get; set; }

        [Required]
        [ForeignKey("Branch")]
        public int BranchId { get; set; }


        public Vehicle Vehicle { get; set; }
        public Driver Driver { get; set; }
        public Branch Branch { get; set; }
    }
}