using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicApp.Api.Domain.Models
{
    public class Hospital
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OfficePhoneNumber { get; set; }
        public string Address { get; set; }
        public string Lga { get; set; }
        public virtual int LocationId { get; set; }
        public virtual Location Location { get; set; }
        public virtual List<Enrollee> Enrollees { get; set; }
    }
}
