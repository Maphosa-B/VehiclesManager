using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VehiclesManager.Entities
{
    public class Supplier
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        [Required]
        public DateTime AddDate { get; set; } = DateTime.Now;

        public ICollection<Vehicle> Vehicles { get; set; }
    }
}