using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicApp.Api.Domain.Models
{
    public class LocationDetailsModel
    {
        public int LgaId { get; set; }
        public string LgaName { get; set; }
        public int EnrolleesCount { get; set; }
        public int MaleEnrolleesCount { get; set; }
        public int FemaleEnrolleesCount { get; set; }
        public int HospitalCount { get; set; }
        public string TopThreeDiseases { get; set; }
    }
}
