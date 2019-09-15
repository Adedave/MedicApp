using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicApp.Models
{
    public class DiseaseModel
    {
        public int Id { get; set; }
        public string DiseaseName { get; set; }
        public DateTime DateDiagnosed { get; set; }
    }
}
