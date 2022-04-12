using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalMgt.Models
{
    public class Dispense
    {
        public Guid Id { get; set; }
        public Guid DispensedBy { get; set; }
        [ForeignKey("DispensedBy")]
        public Doctor Doctor { get; set; }
        public Guid DispensedTo { get; set; }
        [ForeignKey("DispensedTo")]
        public Patient Patient { get; set; }
    }
}
