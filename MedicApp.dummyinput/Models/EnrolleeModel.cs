using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicApp.dummyinput.Models
{
    public class EnrolleeModel
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Lga { get; set; }
        public int HospitalId { get; set; }
        public List<EnrolleeDiseaseModel> Diseases { get; set; }
    }
}
