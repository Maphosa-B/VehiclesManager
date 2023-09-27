using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VehiclesManager.Entities
{
    public class Driver
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(500)]
        public string EmailAddress { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        [Required]
        public bool IsAvailable { get; set; } = true;

        [Required]
        public DateTime AddDate { get; set; } = DateTime.Now;
    }
}