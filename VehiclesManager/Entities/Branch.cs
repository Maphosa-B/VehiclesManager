using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VehiclesManager.Entities
{
    public class Branch
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public DateTime AddDate { get; set; }

        [ForeignKey("Client")]
        public int ClientId { get; set; }

        public Client Client { get; set; }
    }
}