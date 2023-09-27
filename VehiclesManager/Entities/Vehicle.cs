using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VehiclesManager.Entities
{
    public class Vehicle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public string Registration { get; set; }

        [Required]
        public DateTime AddDate { get; set; }

        [Required]
        public bool IsActive { get; set; }


        [Required]
        [ForeignKey("Supplier")]
        public  int SupplierId { get; set; }


        public  Supplier Supplier { get; set; }

    }

}