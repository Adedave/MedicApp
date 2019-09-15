using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicApp.Api.Domain.Models
{
    public class Enrollee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Lga { get; set; }

        public virtual int HospitalId { get; set; }
        public virtual Hospital Hospital { get; set; }
        public virtual IEnumerable<EnrolleeDisease> Diseases { get; set; }
    }
}
