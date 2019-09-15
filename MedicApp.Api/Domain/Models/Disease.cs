using System;
using System.Collections.Generic;

namespace MedicApp.Api.Domain.Models
{
    public class Disease
    {
        public int Id { get; set; }
        public string DiseaseName { get; set; }

        public virtual List<EnrolleeDisease> Enrollees { get; set; }
    }
}
