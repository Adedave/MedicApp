using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicApp.dummyinput.Models
{
    public class HospitalModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OfficePhoneNumber { get; set; }
        public string Address { get; set; }
        public string Lga { get; set; }
        public int LocationId { get; set; }
    }
}
