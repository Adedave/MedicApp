using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicApp.Models
{
    public class EnrolleeModel
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public string Gender { get; set; }
        public string LGA { get; set; }
        public List<DiseaseModel> Diseases { get; set; }
    }
}
