using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VehiclesManager.Entities
{
    public class Client
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]

        public string Name { get; set; }

        [Required]
        public DateTime AddDate { get; set; }

        public ICollection<Branch> Branches { get; set; }
    }
}