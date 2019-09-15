using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicApp.Api.Domain.Models
{
    public class EnrolleeDisease
    {
        public int EnrolleeId { get; set; }
        public int DiseaseId { get; set; }
        public DateTime DateDiseaseDiagnosed { get; set; }
        

        public virtual Enrollee Enrollee { get; set; }
        public virtual Disease Disease { get; set; }
    }
}
