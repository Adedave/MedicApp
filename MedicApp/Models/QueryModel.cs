using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicApp.Models
{
    public class QueryModel
    {
        public int Age { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public int LgaId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
